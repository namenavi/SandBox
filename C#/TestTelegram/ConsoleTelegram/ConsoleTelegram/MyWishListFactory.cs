using Telegram.Bot.Types;
using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;
using System.Threading;
using System.Text;

namespace ConsoleTelegram
{
    public class MyWishListFactory
    {
        public Dictionary<long, int> historyChat = new();
        public Dictionary<long, List<Wish>> wishList = new();
        public async Task LookMenuWishList(ITelegramBotClient client, Update update, CancellationToken ct)
        {
            if(!wishList.TryGetValue(update.Message!.Chat.Id, out var list))
            {
                wishList.Add(update.Message!.Chat.Id, new List<Wish>());
                list = new List<Wish>();
            }

            if(list!.Count == 0)
            {
                InlineKeyboardMarkup inlineKeyboard = new(new[]
                {
                    new []
                    {
                        InlineKeyboardButton.WithCallbackData(text: "➕ Добавить желание", callbackData: "/addwish"),
                    },
                    //new []
                    //{
                    //    InlineKeyboardButton.WithCallbackData(text: "🔝 Изменить название списка", callbackData: "/refrashnamelist"),
                    //},
                     new []
                    {
                        InlineKeyboardButton.WithCallbackData(text: "🏡 Перейти в главное меню", callbackData: "/lookMenu"),
                    }
                });
                await client.SendTextMessageAsync(
                            chatId: update.Message!.Chat.Id,
                            text: "К сожелению Ваш список пуст.",
                            replyMarkup: inlineKeyboard,
                            cancellationToken: ct);
            }
            else
            {
                var sb = new StringBuilder();
                sb.AppendLine("Вот ваш список желаний:");
                // Создать счетчик для нумерации желаний
                int counter = 1;

                foreach(Wish wish in list)
                {
                    sb.AppendLine(counter + ") " + wish.Name);
                    counter++;
                }

                InlineKeyboardMarkup inlineKeyboard = new(new[]
                {
                    new []
                    {
                        InlineKeyboardButton.WithCallbackData(text: "➕ Добавить желание", callbackData: "/addwish"),
                        InlineKeyboardButton.WithCallbackData(text: "➖ Удалить желание", callbackData: "/deletewish"),
                    },
                    new []
                    {
                        InlineKeyboardButton.WithCallbackData(text: "🏡 Перейти в главное меню", callbackData: "/lookMenu"),
                    }
                });
                await client.SendTextMessageAsync(
                            chatId: update.Message!.Chat.Id,
                            text: sb.ToString(),
                            replyMarkup: inlineKeyboard,
                            cancellationToken: ct);
            }
        }
        public async Task AddWish(ITelegramBotClient client, Update update, CancellationToken ct)
        {
            wishList[update.Message!.Chat.Id].Add(
                new Wish()
                {
                    Name = update.Message.Text!
                });
            await client.SendTextMessageAsync(
            chatId: update.Message!.Chat.Id,
            text: $"✅ Желание <{update.Message.Text}> добавлено!",
            replyMarkup: new ReplyKeyboardRemove(),
            cancellationToken: ct);
            await LookMenuWishList(client, update, ct);
        }

        public async Task LookStartMenu(ITelegramBotClient bot, Message message, CancellationToken ct)
        {
            ReplyKeyboardMarkup replyKeyboardMarkup = new(
                    new[]
                    {
                        ["✨ Посмотреть список друга"],
                        new KeyboardButton[] { "📜 Мой список", "👻 Профиль:" },
                    })
            {
                ResizeKeyboard = true
            };
            await bot.SendTextMessageAsync(chatId: message.Chat.Id,
                                                  text: "Этот бот поможет вам поделиться списком желаний с друзьями! ⚡️\r\n\r\n1. создавайте список желаний\r\n2. отправляйте ссылку друзьям\r\n" +
                                                  "3. выбранные друзьями желания автоматически удалятся из списка\r\n\r\nИспользование бота бесплатно!\r\n\r\nКак создать список желаний?\r\n\r\n" +
                                                  "Все просто: выбирайте Мой список в основном меню и добавляйте желания!\r\n\r\nМы добавили опцию неисчезающего желания: " +
                                                  "если одно и то же желания может выбрать несколько человек. Например, “донат на квартиру”. Для этого выберите опцию Многоразовый при создании желания." +
                                                  " Рядом с таким желанием в списке появится 🔒.\r\n\r\nПеред списком вы можете добавить приветствие для друзей: указать дату праздника, написать, " +
                                                  "что хотите получить больше всего, пожелать им классного дня.\r\n\r\nКак поделиться списком с друзьями?\r\n\r\nПерейдите в ваш Профиль из основного меню, " +
                                                  "скопируйте ссылку на список и отправьте его друзьям. Также посмотреть список можно введя номер списка в Посмотреть чужой список.\r\n\r\nКак выбрать желание из списка друга?\r\n\r\n" +
                                                  "Откройте список желаний друга (по ссылке или найдите его по номеру), изучите список — он может занимать несколько страниц. Нажимайте «Листать», чтобы увидеть следующую страницу. " +
                                                  "Нажмите \"Вычеркнуть желание\" и подтвердите выбор. Готово!\r\n\r\nЕсли рядом с названием желания стоит замочек🔒, то такое желание нельзя вычеркнуть — оно многоразовое. " +
                                                  "То есть такое желание могут выбрать неограниченное количество пользователей. Оно не пропадает из списка.\r\n\r\n💡 Совет!\r\n\r\nМаксимально подробно описывайте ваше желание: " +
                                                  "вы можете прикрепить ссылку и указать цену. Так вашим друзьям будет легче определиться с выбором!",
                                                  replyMarkup: replyKeyboardMarkup,
                                                  cancellationToken: ct);
        }

        public async Task RemoveKeyboard(ITelegramBotClient bot, Message message, CancellationToken cancellationToken)
        {
            var mes = await bot.SendTextMessageAsync(
                chatId: message.Chat.Id,
                text: "Removing keyboard",
                replyMarkup: new ReplyKeyboardRemove(),
                cancellationToken: cancellationToken);
            await bot.DeleteMessageAsync(
                chatId: message.Chat.Id,
                messageId: mes.MessageId,
                cancellationToken: cancellationToken);
        }
        public async Task InputAddWish(ITelegramBotClient client, CallbackQuery callbackQuery, CancellationToken ct)
        {
            await client.DeleteMessageAsync(
                chatId: callbackQuery.Message!.Chat.Id,
                messageId: callbackQuery.Message!.MessageId,
                cancellationToken: ct
                );

            var replyKeyboardMarkup = new ReplyKeyboardMarkup(
               new KeyboardButton[]
               {
                    "❌ Отмена"
               })
            {
                ResizeKeyboard = true,
            };

            var message = await client.SendTextMessageAsync(
                        chatId: callbackQuery.Message!.Chat.Id,
                        text: "Введите желание:\r\n\r\n(Укажите краткое название, позже можно будеть добавить описание)",
                        replyMarkup: replyKeyboardMarkup,
                        cancellationToken: ct);
            historyChat[callbackQuery.Message!.Chat.Id] = message.MessageId;
        }

        public async Task LookDeleteListWish(ITelegramBotClient client, CallbackQuery callbackQuery, CancellationToken ct)
        {
            var list = wishList[callbackQuery.Message!.Chat.Id];
            var buttonRows = new List<InlineKeyboardButton[]>();
            var counter = 1;

            foreach(var wish in list)
            {
                buttonRows.Add(new[]
                {
                    InlineKeyboardButton.WithCallbackData(text:$"{counter}) {wish.Name}" , callbackData: $"/deletewish,{wish.Id}")
                });
                counter++;
            }

            buttonRows.Add(new[]
            {
                InlineKeyboardButton.WithCallbackData(text: "↩️ Назад", callbackData: "/lookMenu")
            });

            InlineKeyboardMarkup inlineKeyboard = new InlineKeyboardMarkup(buttonRows);
            await client.EditMessageTextAsync(
                         chatId: callbackQuery.Message!.Chat.Id,
                         messageId: callbackQuery.Message.MessageId,
                         text: "➖ Удаление желания:\r\n\r\nНажмите на желание для удаления:",
                         replyMarkup: inlineKeyboard,
                         cancellationToken: ct);
        }

        public async Task LookDeleteWish(ITelegramBotClient client, CallbackQuery callbackQuery, CancellationToken ct, string[] subs)
        {
            InlineKeyboardMarkup inlineKeyboard = new(new[]
               {
                    new []
                    {
                        InlineKeyboardButton.WithCallbackData(text: "☑️ Удалить", callbackData: "/addwish"),
                        InlineKeyboardButton.WithCallbackData(text: "↩️ Назад", callbackData: "/lookMenu"),
                    }
                });
            await client.EditMessageTextAsync(
                         chatId: callbackQuery.Message!.Chat.Id,
                         messageId: callbackQuery.Message.MessageId,
                         text: $"➖ Удаление желания:\r\n\r\nВы уверены," +
                         $" что хотите удалить желание " +
                         $"`{wishList[callbackQuery.Message!.Chat.Id]
                         .Where(t => t.Id.ToString() == subs[1])}`?",
                         replyMarkup: inlineKeyboard,
                         cancellationToken: ct);
        }
    }
}
