using Microsoft.EntityFrameworkCore;

namespace ConsoleTelegram
{

    namespace ConsoleEF
    {
        public class ApplicationContext : DbContext
        {
            /// <summary>
            /// Свойство, которое хранит набор сущностей желаний
            /// </summary>
            public DbSet<Wish> Wishes { get; set; }

            /// <summary>
            /// Свойство, которое хранит набор сущностей пользователей
            /// </summary>
            public DbSet<User> Users { get; set; }

            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                optionsBuilder.UseNpgsql(
                    Environment.GetEnvironmentVariable("DefaultConnectionNpgsql"
                    , EnvironmentVariableTarget.Machine)!);
            }
            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                // Настройка сущности желания
                modelBuilder.Entity<Wish>()
                    .Property(w => w.Id)
                    .ValueGeneratedOnAdd();// Указываем, что свойство Id генерируется базой данных
                modelBuilder.Entity<Wish>()
                    .HasKey(w => w.Id); // Указание первичного ключа
                modelBuilder.Entity<Wish>()
                    .Property(w => w.Name) // Указание свойства имени
                    .IsRequired() // Обязательное поле
                    .HasMaxLength(100); // Ограничение длины
                modelBuilder.Entity<Wish>()
                    .Property(w => w.Description) // Указание свойства описания
                    .HasMaxLength(500); // Ограничение длины
                modelBuilder.Entity<Wish>()
                    .Property(w => w.Status) // Указание свойства статуса
                    .IsRequired() // Обязательное поле
                    .HasMaxLength(20); // Ограничение длины
                modelBuilder.Entity<Wish>()
                    .HasOne(w => w.User) // Указание связи с пользователем
                    .WithMany(u => u.Wishes) // Указание связи с коллекцией желаний
                    .HasForeignKey(w => w.UserId) // Указание внешнего ключа
                    .OnDelete(DeleteBehavior.Cascade); // Указание поведения при удалении
                modelBuilder.Entity<Wish>()
                    .HasOne(w => w.Executor) // Указание связи с исполнителем
                    .WithMany(u => u.ExecutableWishes) // Указание связи с коллекцией исполненных желаний
                    .HasForeignKey(w => w.ExecutorId) // Указание внешнего ключа
                    .OnDelete(DeleteBehavior.SetNull); // Указание поведения при удалении

                // Настройка сущности пользователя
                modelBuilder.Entity<User>()
                    .Property(u => u.Id)
                    .ValueGeneratedOnAdd();// Указываем, что свойство Id генерируется базой данных
                modelBuilder.Entity<User>()
                    .HasKey(u => u.Id); // Указание первичного ключа
                modelBuilder.Entity<User>()
                    .Property(u => u.UserName) // Указание свойства имени
                    .HasMaxLength(50); // Ограничение длины
                modelBuilder.Entity<User>()
                    .HasIndex(u => u.UserName) // Указание индекса по имени
                    .IsUnique(); // Уникальное значение
                modelBuilder.Entity<User>()
                   .Property(u => u.ChatId) // Указание свойства идентификатора чата
                   .IsRequired(); // Обязательное поле
                modelBuilder.Entity<User>()
                   .HasIndex(u => u.ChatId) // Указание индекса по идентификатору чата
                   .IsUnique(); // Уникальное значение
            }
        }
    }


}
