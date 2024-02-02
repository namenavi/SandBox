﻿// <auto-generated />
using System;
using ConsoleEF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ConsoleEF.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20240202193803_Initial7")]
    partial class Initial7
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("ConsoleEF.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("ChatId")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("Id");

                    b.HasIndex("ChatId")
                        .IsUnique();

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ConsoleEF.Entities.Wish", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("ChosenDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)");

                    b.Property<DateTime?>("DoneDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid?>("ExecutorId")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<int?>("Rating")
                        .HasColumnType("integer");

                    b.Property<int>("Status")
                        .HasMaxLength(20)
                        .HasColumnType("integer");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("ExecutorId");

                    b.HasIndex("UserId");

                    b.ToTable("Wishes");
                });

            modelBuilder.Entity("ConsoleEF.Entities.Wish", b =>
                {
                    b.HasOne("ConsoleEF.Entities.User", "Executor")
                        .WithMany("ExecutableWishes")
                        .HasForeignKey("ExecutorId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("ConsoleEF.Entities.User", "User")
                        .WithMany("Wishes")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Executor");

                    b.Navigation("User");
                });

            modelBuilder.Entity("ConsoleEF.Entities.User", b =>
                {
                    b.Navigation("ExecutableWishes");

                    b.Navigation("Wishes");
                });
#pragma warning restore 612, 618
        }
    }
}
