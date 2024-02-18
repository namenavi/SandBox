using Telegram.Bot.Types;
using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;
using System.Threading;
using System.Text;
using Telegram.Bot.Types.InlineQueryResults;
using System.Collections.Generic;
using System.Reflection.Emit;

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
                        InlineKeyboardButton.WithCallbackData(text: "➖ Удалить желание", callbackData: "/deletewishs"),
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
            wishList[update.Message!.Chat.Id]
                .Add(new Wish(update.Message.Text!));
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
                        new KeyboardButton[] { "📜 Мой список", "👻 Профиль" },
                    })
            {
                ResizeKeyboard = true
            };
            await bot.SendTextMessageAsync(chatId: message.Chat.Id,
                                                  text: "Этот бот поможет вам поделиться списком желаний с друзьями! ⚡️\r\n\r\n1. создавайте список желаний\r\n2. отправляйте ссылку друзьям\r\n" +
                                                  "3. выбранные друзьями желания автоматически удалятся из списка\r\n\r\nИспользование бота бесплатно!\r\n\r\nКак создать список желаний?\r\n\r\n" +
                                                  "Все просто: выбирайте Мой список в основном меню и добавляйте желания!\r\n\r\nКак поделиться списком с друзьями?\r\n\r\nПерейдите в ваш Профиль из основного меню, " +
                                                  "скопируйте ссылку на список и отправьте его друзьям. Также посмотреть список можно введя номер списка в Посмотреть чужой список.\r\n\r\nКак выбрать желание из списка друга?\r\n\r\n" +
                                                  "Откройте список желаний друга (по ссылке или найдите его по номеру), изучите список — он может занимать несколько страниц. Нажимайте «Листать», чтобы увидеть следующую страницу. " +
                                                  "Нажмите \"Вычеркнуть желание\" и подтвердите выбор. Готово!\r\n\r\n💡 Совет!\r\n\r\n" +
                                                  "Вы можете прикрепить ссылку и указать цену. Так вашим друзьям будет легче определиться с выбором!",
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
                    InlineKeyboardButton.WithCallbackData(text:$"{counter}) {wish.Name}" , callbackData: $"/deletewishs,{wish.Id}")
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
            var wish = wishList[callbackQuery.Message!.Chat.Id]
                        .Where(t => t.Id.ToString() == subs[1]).FirstOrDefault();
            InlineKeyboardMarkup inlineKeyboard = new(new[]
               {
                    new []
                    {
                        InlineKeyboardButton.WithCallbackData(text: "☑️ Удалить", callbackData: $"/deletewish,{wish!.Id}"),
                        InlineKeyboardButton.WithCallbackData(text: "↩️ Назад", callbackData: "/lookMenu"),
                    }
                });

            await client.EditMessageTextAsync(
                         chatId: callbackQuery.Message!.Chat.Id,
                         messageId: callbackQuery.Message.MessageId,
                         text: $"➖ Удаление желания:\r\n\r\nВы уверены," +
                         $" что хотите удалить желание " +
                         $"`{wish.Name}`?",
                         replyMarkup: inlineKeyboard,
                         cancellationToken: ct);
        }

        public async Task DeleteWish(ITelegramBotClient client, CallbackQuery callbackQuery, CancellationToken ct, string[] subs)
        {
            var wish = wishList[callbackQuery.Message!.Chat.Id]
                        .Where(t => t.Id.ToString() == subs[1]).FirstOrDefault();
            var res = wishList[callbackQuery.Message!.Chat.Id].Remove(wish!);
            if(!res)
            {
                return;
            }
            await client.SendTextMessageAsync(
            chatId: callbackQuery.Message!.Chat.Id,
            text: $"✅ Желание <{wish!.Name}> удалено!",
            replyMarkup: new ReplyKeyboardRemove(),
            cancellationToken: ct);
            await LookDeleteListWish(client, callbackQuery, ct);
        }

        public async Task LookProfile(ITelegramBotClient client, Update update, CancellationToken ct)
        {
            ReplyKeyboardMarkup replyKeyboardMarkup = new(
                    new[]
                    {
                        ["✨ Посмотреть список друга"],
                        new KeyboardButton[] { "📜 Мой список", "👻 Профиль" },
                    })
            {
                ResizeKeyboard = true
            };
            await client.SendTextMessageAsync(chatId: update.Message!.Chat.Id,
                                                  text: $"👻 Профиль:\r\n\r\nTelegram ID - {update.Message.From!.Id}\r\nTelegram логин - @{update.Message.From.Username}\r\n\r\n🔗 Ссылка на список желаний - https://t.me/WishListMasterBot?start={update.Message.From.Id}\r\n\r\nОтправьте друзьям ссылку, чтобы они могли посмотреть ваш список.",
                                                  replyMarkup: replyKeyboardMarkup,
                                                  cancellationToken: ct);
        }

        public async Task LookWishListMenuAnother(ITelegramBotClient client, Update update, CancellationToken ct)
        {
            InlineKeyboardMarkup inlineKeyboard = new(new[]
               {
                    new []
                    {
                        InlineKeyboardButton.WithCallbackData(text: "@ Логин", callbackData: "/seatch_L"),
                    }, new []
                    {
                        InlineKeyboardButton.WithCallbackData(text: "📞 Телефон", callbackData: "/seatch_T"),
                    },
                     new []
                    {
                        InlineKeyboardButton.WithCallbackData(text: "🏡 Перейти в главное меню", callbackData: "/lookMenu"),
                    }
                });
            await client.SendTextMessageAsync(
                        chatId: update.Message!.Chat.Id,
                        text: "Здесь можно поискать списки желаний твоих знакомых! 🔍\r\n\r\n" +
                        "Выбери способ поиска с помощью кнопок ниже ⤵️\r\n\r\n" +
                        "или прикрепи контакт пользователя из телефонной книги с помощью скрепочки прямо сейчас 📎",
                        replyMarkup: inlineKeyboard,
                        cancellationToken: ct);
        }

        public async Task LookWishListAnother(ITelegramBotClient client, Update update, CancellationToken ct)
        {
            var contact = update.Message!.Contact;
            if(contact!.UserId != null)
            {
                var listWish = wishList[contact.UserId.Value];
                if(listWish.Count != 0)
                {
                    var sb = new StringBuilder();
                    sb.AppendLine($"Вот список желаний пользователя {contact.FirstName}:");
                    // Создать счетчик для нумерации желаний
                    int counter = 1;

                    foreach(Wish wish in listWish)
                    {
                        sb.AppendLine(counter + ") " + wish.Name);
                        counter++;
                    }

                    InlineKeyboardMarkup inlineKeyboard = new(new[]
                    {
                        new []
                        {
                            InlineKeyboardButton.WithCallbackData(text: "🎁 Выбрать одно из желаний", callbackData: $"/looklistwishs,{contact.UserId.Value}"),
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
                    return;

                }
            }

            await client.SendTextMessageAsync(
                        chatId: update.Message!.Chat.Id,
                        text: "Не нашел ни одного желания этого пользователя 😔\r\n\r\n" +
                        "Попробуй поискать другим способом или расскажи пользователю о боте!" +
                        "\r\n\r\nПусть скорее заполняет свой список желаний!",
                        cancellationToken: ct);
        }
        public async Task LookWishListAnotherButton(ITelegramBotClient client, Update update, CancellationToken ct)
        {
            var contact = update.Message!.Contact;
            if(contact!.UserId != null)
            {
                var listWish = wishList[contact.UserId.Value];
                if(listWish.Count != 0)
                {
                    var buttonRows = new List<InlineKeyboardButton[]>();
                    var counter = 1;

                    foreach(var wish in listWish)
                    {
                        buttonRows.Add(new[]
                        {
                            InlineKeyboardButton.WithCallbackData(text:$"{counter}) {wish.Name}" , callbackData: $"/looklistwishs,{contact.UserId.Value}")
                        });
                        counter++;
                    }

                    buttonRows.Add(new[]
                    {
                        InlineKeyboardButton.WithCallbackData(text: "↩️ Назад", callbackData: "/lookMenu")
                    });
                }

            }

            await client.SendTextMessageAsync(
                        chatId: update.Message!.Chat.Id,
                        text: "Не нашел ни одного желания этого пользователя 😔\r\n\r\n" +
                        "Попробуй поискать другим способом или расскажи пользователю о боте!" +
                        "\r\n\r\nПусть скорее заполняет свой список желаний!",
                        cancellationToken: ct);
        }
    }
}
