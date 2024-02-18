using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot;

namespace ConsoleTelegram
{
    public class MapGenerator
    {
        public Dictionary<long, MapGenState> ChatDict { get; set; } = new();

        public async Task Process(ITelegramBotClient client, Update update, CancellationToken ct)
        {
            if(!ChatDict.TryGetValue(update.Message!.Chat.Id, out var state))
            {
                ChatDict.Add(update.Message!.Chat.Id, new MapGenState());
            }

            state = ChatDict[update.Message!.Chat.Id];

            switch(state.Mode)
            {
                case Mode.Initial:
                    await SendInitial(client, update, state, ct);
                    break;
                case Mode.SetLat:
                    await SendSetLat(client, update, state, ct);
                    break;

                case Mode.SetLon:
                    await SendSetLong(client, update, state, ct);
                    break;
            }

        }

        private static async Task SendSetLong(ITelegramBotClient client, Update update, MapGenState state, CancellationToken ct)
        {
            var lonText = update.Message!.Text;
            if(lonText == null || !double.TryParse(lonText, out var lon))
            {
                await client.SendTextMessageAsync(
                   chatId: update.Message!.Chat.Id,
                   text: "Введите долгота повторно",
                   cancellationToken: ct);
            }
            else
            {
                state.Long = lon;
                try
                {
                    await client.SendLocationAsync(
                         chatId: update.Message!.Chat.Id,
                         longitude: state.Long,
                         latitude: state.Lat);
                    await client.SendTextMessageAsync(
                        chatId: update.Message!.Chat.Id,
                        text: "Вот ваша точка",
                        cancellationToken: ct);

                    await SendInitial(client, update, state, ct);
                }
                catch(Exception ex)
                {
                    await client.SendTextMessageAsync(
               chatId: update.Message!.Chat.Id,
               text: "Координаты некорректные",
               cancellationToken: ct);
                    await SendInitial(client, update, state, ct);
                }


            }
        }

        private static async Task SendSetLat(ITelegramBotClient client, Update update, MapGenState state, CancellationToken ct)
        {
            var latText = update.Message.Text;
            if(latText == null || !double.TryParse(latText, out var lat))
            {
                await client.SendTextMessageAsync(
                   chatId: update.Message!.Chat.Id,
                   text: "Введите широту повторно",
                   cancellationToken: ct);
            }
            else
            {
                state.Lat = lat;
                await client.SendTextMessageAsync(
                      chatId: update.Message!.Chat.Id,
                      text: "Введите Долготу",
                      cancellationToken: ct);
                state.Mode = Mode.SetLon;
            }
        }

        private static async Task SendInitial(ITelegramBotClient client, Update update, MapGenState? state, CancellationToken ct)
        {
            await client.SendTextMessageAsync(
                chatId: update.Message!.Chat.Id,
                text: "Введите широту",
                cancellationToken: ct);
            state.Mode = Mode.SetLat;
        }

    }
}
