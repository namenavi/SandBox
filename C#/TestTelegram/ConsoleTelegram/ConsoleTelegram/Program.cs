using System;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace ConsoleTelegram
{
    public class Program
    {
        Dictionary<long, ChatMode> dict = new Dictionary<long, ChatMode>();
        MyWishListFactory myListWish = new MyWishListFactory();

        static async Task Main(string[] args)
        {
            Program program = new Program();
            var botClient = new TelegramBotClient("");
            await botClient.DeleteWebhookAsync();
            var ro = new ReceiverOptions
            {
                AllowedUpdates = Array.Empty<UpdateType>(),
            };

            botClient.StartReceiving(updateHandler: program.Handler, pollingErrorHandler: program.HandleErrorAsync, receiverOptions: ro);
            Console.WriteLine("Стартовали");
            Console.ReadLine();
        }
        async Task Handler(ITelegramBotClient client, Update update, CancellationToken ct)
        {
            var handler = update.Type switch
            {
                // UpdateType.Unknown:
                // UpdateType.ChannelPost:
                // UpdateType.EditedChannelPost:
                // UpdateType.ShippingQuery:
                // UpdateType.PreCheckoutQuery:
                // UpdateType.Poll:
                UpdateType.Message => BotOnMessageReceived(client, update, ct),
                UpdateType.EditedMessage => BotOnMessageReceived(client, update, ct),
                UpdateType.CallbackQuery => BotOnCallbackQueryReceived(client, update.CallbackQuery!, ct),
                UpdateType.InlineQuery => BotOnInlineQueryReceived(update.InlineQuery!),
                UpdateType.ChosenInlineResult => BotOnChosenInlineResultReceived(update.ChosenInlineResult!),
                _ => UnknownUpdateHandlerAsync(update)
            };

            try
            {
                await handler;
            }
            catch(Exception exception)
            {
                await HandleErrorAsync(client, exception, ct);
            }
        }

        private async Task BotOnMessageReceived(ITelegramBotClient client, Update update, CancellationToken ct)
        {

            if(!dict.TryGetValue(update.Message!.Chat.Id, out var state))
            {
                dict.Add(update.Message!.Chat.Id, ChatMode.Initial);
            }
           

            state = dict[update.Message!.Chat.Id];
            switch(state)
            {
                case ChatMode.Initial:
                    switch(update.Message.Text)
                    {
                        case "✨ Посмотреть список друга":
                            //await myListWish.Process(client, update, ct);
                            //dict[update.Message!.Chat.Id] = ChatMode.WisnListOther;
                            break;
                        case "📜 Мой список":
                            await myListWish.LookMenuWishList(client, update, ct);
                            break;
                        default:
                            await myListWish.LookStartMenu(client, update.Message, ct);
                            break;
                    }
                    break;
                case ChatMode.AddWisn:
                    if(update.Message.Text == "❌ Отмена")
                    {
                        await myListWish.RemoveKeyboard(client, update.Message, ct);
                        await client.DeleteMessageAsync(
                                chatId: update.Message!.Chat.Id,
                                messageId: update.Message!.MessageId,
                                cancellationToken: ct);
                        await client.DeleteMessageAsync(
                                  chatId: update.Message!.Chat.Id,
                                  messageId: myListWish.historyChat[update.Message!.Chat.Id],
                                  cancellationToken: ct);
                        await myListWish.LookMenuWishList(client, update, ct);
                    }
                    else
                    {
                        await myListWish.AddWish(client, update, ct);
                    }
                    dict[update.Message!.Chat.Id] = ChatMode.Initial;
                    break;
                case ChatMode.GetWishOther:
                    break;
                case ChatMode.Profile:
                    break;
                default:
                    break;
            }
        }



        /// <param name="client"></param>
        /// <param name="callbackQuery"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        private async Task BotOnCallbackQueryReceived(ITelegramBotClient client, CallbackQuery callbackQuery, CancellationToken ct)
        {
            if(callbackQuery != null)
            {
                //subs[0] - command
                //subs[n] - переменные
                string[] subs = callbackQuery.Data!.Split(",");
                switch(subs[0])
                {
                    case "/addwish":
                        await myListWish.InputAddWish(client, callbackQuery, ct);
                        dict[callbackQuery.Message!.Chat.Id] = ChatMode.AddWisn;
                        break;
                    case "/deletewish":
                        if(subs.Count() == 1)
                        {
                            await myListWish.LookDeleteListWish(client, callbackQuery, ct);
                        }
                        else
                        {
                            await myListWish.LookDeleteWish(client, callbackQuery, ct, subs);
                        }
                       
                       // dict[callbackQuery.Message!.Chat.Id] = ChatMode.DeleteWish;
                        break;
                    case "/lookMenu":
                        await myListWish.LookStartMenu(client, callbackQuery.Message!, ct);
                        dict[callbackQuery.Message!.Chat.Id] = ChatMode.Initial;
                        break;
                    default:
                        await myListWish.LookStartMenu(client, callbackQuery.Message!, ct);
                        break;
                }
            }
        }

       

















        private Task Process(ITelegramBotClient client, Update update, CancellationToken ct)
        {

            throw new NotImplementedException();
        }

        async Task HandleErrorAsync(ITelegramBotClient client, Exception exception, CancellationToken ct)
        {
            Console.WriteLine(exception.Message);
        }

        private Task UnknownUpdateHandlerAsync(Update update)
        {
            throw new NotImplementedException();
        }

        private Task BotOnChosenInlineResultReceived(ChosenInlineResult chosenInlineResult)
        {
            throw new NotImplementedException();
        }

        private Task BotOnInlineQueryReceived(InlineQuery inlineQuery)
        {
            throw new NotImplementedException();
        }
    }
}




/*
  private async Task EditMessage(ITelegramBotClient client, CallbackQuery callbackQuery, CancellationToken ct)
        {
                //if(callbackQuery.Data == "/addwish")
                //{
                //}
                //else
                //{
                //    await client.AnswerCallbackQueryAsync(
                //        callbackQueryId: callbackQuery.Id,
                //        text: $"Received {callbackQuery.Data}");

                //    await client.SendTextMessageAsync(
                //        chatId: callbackQuery.Message!.Chat.Id,
                //        text: $"Received {callbackQuery.Data}");
                //}
        }
 */
