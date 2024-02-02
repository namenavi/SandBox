using ConsoleEF.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=EF;Username=postgres;Password=pass");
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
                .Property(u => u.Name) // Указание свойства имени
                .IsRequired() // Обязательное поле
                .HasMaxLength(50); // Ограничение длины
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Name) // Указание индекса по имени
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
/*docker run --hostname=a059d6062d5b
 * --mac-address=02:42:ac:11:00:02 
 * --env=POSTGRES_PASSWORD=pass 
 * --env=POSTGRES_USER=postgres 
 * --env=PATH=/usr/local/sbin:/usr/local/bin:/usr/sbin:/usr/bin:/sbin:/bin:/usr/lib/postgresql/16/bin 
 * --env=GOSU_VERSION=1.16 --env=LANG=en_US.utf8 
 * --env=PG_MAJOR=16 --env=PG_VERSION=16.1-1.pgdg120+1 
 * --env=PGDATA=/var/lib/postgresql/data --volume=/var/lib/postgresql/data 
 * -p 5432:5432 --runtime=runc -d postgres:16.1*/