using ConsoleEF.Entities;
using Microsoft.EntityFrameworkCore;

namespace ConsoleEF
{
    internal class Program
    {
        static void Main(string[] args)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            // добавление данных
            using(ApplicationContext db = new ApplicationContext())
            {

                // создаем два объекта User
                User user2 = new User { Name = "Иван" };
                user2.ChatId = 113333343;
                user2.Wishes!.Add(new Wish("Телефон")
                {
                    Description = "Хочу красивый телефон",
                });

                // добавляем их в бд
                db.Users.AddRange( user2);
                db.SaveChanges();
            }
            //получение данных
            using(ApplicationContext db = new ApplicationContext())
            {
                // получаем объекты из бд и выводим на консоль
                var users = db.Users.Include(u => u.Wishes).ToList();
                Console.WriteLine("Users list:");
                foreach(var u in users)
                {
                    if(u.Wishes!.Count != 0)
                    {
                        Console.WriteLine($"{u.Id}.{u.Name} - {u.Wishes.FirstOrDefault()!.Name} {u.Wishes.FirstOrDefault()!.Description}");
                    }
                    else
                    {
                        Console.WriteLine($"{u.Id}.{u.Name} - null null");
                    }
                    
                }
                Console.ReadLine();
            }
        }
    }
}
/*
 * Message = "A command is already in progress: SELECT u.\"Id\", u.\"ChatId\", u.\"Name\", w.\"Id\", w.\"ChosenDate\", w.\"Description\", w.\"DoneDate\", w.\"ExecutorId\", w.\"Name\", w.\"Rating\", w.\"Status\", w.\"UserId\"\r\nFROM \"Users\" AS u\r\nLEFT JOIN \"Wishes\...
 */