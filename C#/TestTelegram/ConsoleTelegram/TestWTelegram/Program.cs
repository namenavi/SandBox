﻿// Импортировать библиотеку TLSharp
using TL;
using WTelegram;

namespace TestWTelegram
{
    internal class Program
    {
        static string Config(string what)
        {
            if(what == "api_id")
                return Environment.GetEnvironmentVariable("api_id", EnvironmentVariableTarget.Machine)!;
            if(what == "api_hash")
                return Environment.GetEnvironmentVariable("api_hash", EnvironmentVariableTarget.Machine)!;
            if(what == "phone_number")
                return Environment.GetEnvironmentVariable("phone_number", EnvironmentVariableTarget.Machine)!;
            if(what == "verification_code")
                return Console.ReadLine()!; // let WTelegramClient ask the user with a console prompt 
            if(what == "first_name")
                return "John";      // if sign-up is required
            if(what == "last_name")
                return "Doe";        // if sign-up is required
            if(what == "password")
                return "secret!";     // if user has enabled 2FA
            return null;
        }

        static WTelegram.Client Client;
        static User My;
        static readonly Dictionary<long, User> Users = new();
        static readonly Dictionary<long, ChatBase> Chats = new();

        // go to Project Properties > Debug > Environment variables and add at least these: api_id, api_hash, phone_number
        static async Task Main(string[] _)
        {
            Console.WriteLine("The program will display updates received for the logged-in user. Press any key to terminate");
            WTelegram.Helpers.Log = (l, s) => System.Diagnostics.Debug.WriteLine(s);
            Client = new WTelegram.Client(Config);
            using(Client)
            {
                Client.OnUpdate += Client_OnUpdate;
                My = await Client.LoginUserIfNeeded();
                Users[My.id] = My;
                // Note: on login, Telegram may sends a bunch of updates/messages that happened in the past and were not acknowledged
                Console.WriteLine($"We are logged-in as {My.username ?? My.first_name + " " + My.last_name} (id {My.id})");
                // We collect all infos about the users/chats so that updates can be printed with their names
                var dialogs = await Client.Messages_GetAllDialogs(); // dialogs = groups/channels/users
                dialogs.CollectUsersChats(Users, Chats);
                Console.ReadKey();
            }
        }
        
        // if not using async/await, we could just return Task.CompletedTask
        private static async Task Client_OnUpdate(UpdatesBase updates)
        {
            updates.CollectUsersChats(Users, Chats);
            if(updates is UpdateShortMessage usm && !Users.ContainsKey(usm.user_id))
                (await Client.Updates_GetDifference(usm.pts - usm.pts_count, usm.date, 0)).CollectUsersChats(Users, Chats);
            else if(updates is UpdateShortChatMessage uscm && (!Users.ContainsKey(uscm.from_id) || !Chats.ContainsKey(uscm.chat_id)))
                (await Client.Updates_GetDifference(uscm.pts - uscm.pts_count, uscm.date, 0)).CollectUsersChats(Users, Chats);
            foreach(var update in updates.UpdateList)
                switch(update)
                {
                    case UpdateNewMessage unm:
                        await HandleMessage(unm.message);
                        break;
                    case UpdateEditMessage uem:
                        await HandleMessage(uem.message, true);
                        break;
                    // Note: UpdateNewChannelMessage and UpdateEditChannelMessage are also handled by above cases
                    case UpdateDeleteChannelMessages udcm:
                        Console.WriteLine($"{udcm.messages.Length} message(s) deleted in {Chat(udcm.channel_id)}");
                        break;
                    case UpdateDeleteMessages udm:
                        Console.WriteLine($"{udm.messages.Length} message(s) deleted");
                        break;
                    case UpdateUserTyping uut:
                        Console.WriteLine($"{User(uut.user_id)} is {uut.action}");
                        break;
                    case UpdateChatUserTyping ucut:
                        Console.WriteLine($"{Peer(ucut.from_id)} is {ucut.action} in {Chat(ucut.chat_id)}");
                        break;
                    case UpdateChannelUserTyping ucut2:
                        Console.WriteLine($"{Peer(ucut2.from_id)} is {ucut2.action} in {Chat(ucut2.channel_id)}");
                        break;
                    case UpdateChatParticipants { participants: ChatParticipants cp }:
                        Console.WriteLine($"{cp.participants.Length} participants in {Chat(cp.chat_id)}");
                        break;
                    case UpdateUserStatus uus:
                        Console.WriteLine($"{User(uus.user_id)} is now {uus.status.GetType().Name[10..]}");
                        break;
                    case UpdateUserName uun:
                        Console.WriteLine($"{User(uun.user_id)} has changed profile name: {uun.first_name} {uun.last_name}");
                        break;
                    case UpdateUser uu:
                        Console.WriteLine($"{User(uu.user_id)} has changed infos/photo");
                        break;
                    default:
                        Console.WriteLine(update.GetType().Name);
                        break; // there are much more update types than the above example cases
                }
        }

        // in this example method, we're not using async/await, so we just return Task.CompletedTask
        private static Task HandleMessage(MessageBase messageBase, bool edit = false)
        {
            if(edit)
                Console.Write("(Edit): ");
            switch(messageBase)
            {
                case Message m:
                    //Client.Messages_SendMedia();
                    Console.WriteLine($"{Peer(m.from_id) ?? m.post_author} in {Peer(m.peer_id)}> {m.message}");
                    break;
                case MessageService ms:
                    Console.WriteLine($"{Peer(ms.from_id)} in {Peer(ms.peer_id)} [{ms.action.GetType().Name[13..]}]");
                    break;
            }
            return Task.CompletedTask;
        }

        private static string User(long id) => Users.TryGetValue(id, out var user) ? user.ToString() : $"User {id}";
        private static string Chat(long id) => Chats.TryGetValue(id, out var chat) ? chat.ToString()! : $"Chat {id}";
        private static string Peer(Peer peer) => peer is null ? null : peer is PeerUser user ? User(user.user_id)
            : peer is PeerChat or PeerChannel ? Chat(peer.ID) : $"Peer {peer.ID}";

    }

}



