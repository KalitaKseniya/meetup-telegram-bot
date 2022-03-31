﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using meetup_telegram_bot.Infrastructure;

#nullable disable

namespace meetup_telegram_bot.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20220331125740_AddFlagToShowPresentation")]
    partial class AddFlagToShowPresentation
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("meetup_telegram_bot.Data.DbEntities.FeedbackDbEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AuthorName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("date");

                    b.Property<string>("FutureProposal")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("GeneralFeedback")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<TimeSpan>("Time")
                        .HasColumnType("time");

                    b.HasKey("Id");

                    b.ToTable("Feedbacks", (string)null);
                });

            modelBuilder.Entity("meetup_telegram_bot.Data.DbEntities.PresentationDbEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDisplayed")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<string>("SpeackerName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Presentations", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("f3c868d7-3153-468f-be7c-11bff5ee6692"),
                            Description = "Описание",
                            IsDisplayed = false,
                            SpeackerName = "Hanna",
                            Title = "Путь разработчика"
                        },
                        new
                        {
                            Id = new Guid("794db43d-d7a9-44bb-99aa-57e4de7ee59b"),
                            Description = "Описание",
                            IsDisplayed = false,
                            SpeackerName = "Kseniya",
                            Title = "In-live разработка"
                        },
                        new
                        {
                            Id = new Guid("8f4f245d-bbfc-4ace-bcf4-51d2cb70ed6d"),
                            Description = "Описание",
                            IsDisplayed = false,
                            SpeackerName = "Сюрприз",
                            Title = "Третий доклад"
                        },
                        new
                        {
                            Id = new Guid("fadabc27-40e4-47f3-bc1b-f0916b4772cd"),
                            Description = "Описание",
                            IsDisplayed = true,
                            SpeackerName = "Hanna",
                            Title = "Базы данных. То, что вы РЕАЛЬНО будете использовать на проекте"
                        },
                        new
                        {
                            Id = new Guid("0c03ba0b-3b46-42ba-ba39-6b635c9a4bc0"),
                            Description = "Описание",
                            IsDisplayed = true,
                            SpeackerName = "Kseniya",
                            Title = "REST-архитектура или как усидеть на 6 стулья"
                        },
                        new
                        {
                            Id = new Guid("99d09f48-0fec-4ef4-8292-2bab81de8d37"),
                            Description = "Описание",
                            IsDisplayed = true,
                            SpeackerName = "Илья",
                            Title = "Реалии фуллстека"
                        });
                });

            modelBuilder.Entity("meetup_telegram_bot.Data.DbEntities.QuestionDbEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AuthorName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("date");

                    b.Property<Guid>("PresentationId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<TimeSpan>("Time")
                        .HasColumnType("time");

                    b.HasKey("Id");

                    b.HasIndex("PresentationId");

                    b.ToTable("Questions", (string)null);
                });

            modelBuilder.Entity("meetup_telegram_bot.Data.DbEntities.QuestionDbEntity", b =>
                {
                    b.HasOne("meetup_telegram_bot.Data.DbEntities.PresentationDbEntity", null)
                        .WithMany()
                        .HasForeignKey("PresentationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
