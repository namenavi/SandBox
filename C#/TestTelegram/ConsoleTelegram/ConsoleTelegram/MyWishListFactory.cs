using Telegram.Bot.Types;
using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;
using System.Threading;

namespace ConsoleTelegram
{
    public class MyWishListFactory
    {
        public async Task LookMenuWishList(ITelegramBotClient client, Update update, CancellationToken ct)
        {
            InlineKeyboardMarkup inlineKeyboard = new(new[]
            {
                  new []
                  {
                      InlineKeyboardButton.WithCallbackData(text: "➕ Добавить желание", callbackData: "/addwish"),
                      InlineKeyboardButton.WithCallbackData(text: "➖ Удалить желание", callbackData: "/deletewish"),
                  },
                  new []
                  {
                      InlineKeyboardButton.WithCallbackData(text: "🔝 Изменить название списка", callbackData: "/refrashnamelist"),
                  },
                });
            await client.SendTextMessageAsync(
                        chatId: update.Message!.Chat.Id,
                        text: "К сожелению Ваш список пуст.",
                        replyMarkup: inlineKeyboard,
                        cancellationToken: ct);
        }
        public async Task AddWish(ITelegramBotClient client, Update update, CancellationToken ct)
        {
            await client.SendTextMessageAsync(
                        chatId: update.Message!.Chat.Id,
                        text: $"✅ Желание <{update.Message.Text}> добавлено!",
                        replyMarkup: new ReplyKeyboardRemove(),
                        cancellationToken: ct);
        }

        public async Task LookStartMenu(ITelegramBotClient bot, Update update, CancellationToken ct)
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
            await bot.SendTextMessageAsync(chatId: update.Message!.Chat.Id,
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
    }
}
