using ConsoleEF.Entities;

namespace ConsoleEF
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // добавление данных
            using(ApplicationContext db = new ApplicationContext())
            {
                // создаем два объекта User
                User user1 = new User { Name = "Tom" };
                User user2 = new User { Name = "Alice" };
                user2.ChatId = 11223343;
                user2.Wishes.Add(new Wish("Велосипед")
                {
                    Description = "Хочу красивый велосипед",
                });

                // добавляем их в бд
                db.Users.AddRange(user1, user2);
                db.SaveChanges();
            }
            //// получение данных
            //using(ApplicationContext db = new ApplicationContext())
            //{
            //    // получаем объекты из бд и выводим на консоль
            //    var users = db.Users.ToList();
            //    Console.WriteLine("Users list:");
            //    foreach(User u in users)
            //    {
            //        Console.WriteLine($"{u.Id}.{u.Name} - {u.Age}");
            //    }
            //}
        }
    }
}
