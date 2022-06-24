﻿// <auto-generated />
using System;
using MeetupTelegramBot.DataAccess.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MeetupTelegramBot.DataAccess.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20220624125327_AddTitleToSeeding")]
    partial class AddTitleToSeeding
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("MeetupTelegramBot.DataAccess.Entities.FeedbackEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AuthorName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FutureProposal")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("GeneralFeedback")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("MeetupId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<TimeSpan>("Time")
                        .HasColumnType("time");

                    b.HasKey("Id");

                    b.HasIndex("MeetupId");

                    b.ToTable("Feedbacks");

                    b.HasData(
                        new
                        {
                            Id = new Guid("1ef9eade-92a2-4277-94df-45b802157ef3"),
                            AuthorName = "Author Name",
                            FutureProposal = "Future proposal",
                            GeneralFeedback = "General Feedback",
                            MeetupId = new Guid("7ef9eade-92a2-4277-94df-45b802157ef3"),
                            Time = new TimeSpan(0, 18, 40, 0, 0)
                        });
                });

            modelBuilder.Entity("MeetupTelegramBot.DataAccess.Entities.MeetupEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Place")
                        .HasColumnType("nvarchar(max)");

                    b.Property<TimeSpan>("Time")
                        .HasColumnType("time");

                    b.HasKey("Id");

                    b.ToTable("Meetups");

                    b.HasData(
                        new
                        {
                            Id = new Guid("7ef9eade-92a2-4277-94df-45b802157ef3"),
                            Date = new DateTime(2022, 6, 23, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Place = "Polotsk",
                            Time = new TimeSpan(0, 18, 30, 0, 0)
                        });
                });

            modelBuilder.Entity("MeetupTelegramBot.DataAccess.Entities.MeetupPresentationEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("MeetupId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("PresentationId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasAlternateKey("MeetupId", "PresentationId");

                    b.HasIndex("PresentationId");

                    b.ToTable("MeetupPresentations");

                    b.HasData(
                        new
                        {
                            Id = new Guid("6ef9eade-92a2-4277-94df-45b802157ef3"),
                            MeetupId = new Guid("7ef9eade-92a2-4277-94df-45b802157ef3"),
                            PresentationId = new Guid("9ef9eade-92a2-4277-94df-45b802157ef3")
                        });
                });

            modelBuilder.Entity("MeetupTelegramBot.DataAccess.Entities.PresentationEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("SpeackerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("SpeackerId");

                    b.ToTable("Presentations");

                    b.HasData(
                        new
                        {
                            Id = new Guid("9ef9eade-92a2-4277-94df-45b802157ef3"),
                            Description = "Description",
                            SpeackerId = new Guid("2ef9eade-92a2-4277-94df-45b802157ef3"),
                            Title = "Title"
                        });
                });

            modelBuilder.Entity("MeetupTelegramBot.DataAccess.Entities.QuestionEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AuthorName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("MeetupPresentationId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Text")
                        .HasColumnType("nvarchar(max)");

                    b.Property<TimeSpan>("Time")
                        .HasColumnType("time");

                    b.HasKey("Id");

                    b.HasIndex("MeetupPresentationId");

                    b.ToTable("Questions");

                    b.HasData(
                        new
                        {
                            Id = new Guid("8ef9eade-92a2-4277-94df-45b802157ef3"),
                            AuthorName = "Author",
                            MeetupPresentationId = new Guid("6ef9eade-92a2-4277-94df-45b802157ef3"),
                            Text = "Sample text",
                            Time = new TimeSpan(0, 18, 40, 0, 0)
                        });
                });

            modelBuilder.Entity("MeetupTelegramBot.DataAccess.Entities.SpeackerEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Speackers");

                    b.HasData(
                        new
                        {
                            Id = new Guid("2ef9eade-92a2-4277-94df-45b802157ef3"),
                            FirstName = "Ivan",
                            LastName = "Ivanov"
                        });
                });

            modelBuilder.Entity("MeetupTelegramBot.DataAccess.Entities.FeedbackEntity", b =>
                {
                    b.HasOne("MeetupTelegramBot.DataAccess.Entities.MeetupEntity", "Meetup")
                        .WithMany("Feedbacks")
                        .HasForeignKey("MeetupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Meetup");
                });

            modelBuilder.Entity("MeetupTelegramBot.DataAccess.Entities.MeetupPresentationEntity", b =>
                {
                    b.HasOne("MeetupTelegramBot.DataAccess.Entities.MeetupEntity", "Meetup")
                        .WithMany("MeetupPresentations")
                        .HasForeignKey("MeetupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MeetupTelegramBot.DataAccess.Entities.PresentationEntity", "Presentation")
                        .WithMany("MeetupPresentations")
                        .HasForeignKey("PresentationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Meetup");

                    b.Navigation("Presentation");
                });

            modelBuilder.Entity("MeetupTelegramBot.DataAccess.Entities.PresentationEntity", b =>
                {
                    b.HasOne("MeetupTelegramBot.DataAccess.Entities.SpeackerEntity", "Speacker")
                        .WithMany("Presentations")
                        .HasForeignKey("SpeackerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Speacker");
                });

            modelBuilder.Entity("MeetupTelegramBot.DataAccess.Entities.QuestionEntity", b =>
                {
                    b.HasOne("MeetupTelegramBot.DataAccess.Entities.MeetupPresentationEntity", "MeetupPresentation")
                        .WithMany("Questions")
                        .HasForeignKey("MeetupPresentationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MeetupPresentation");
                });

            modelBuilder.Entity("MeetupTelegramBot.DataAccess.Entities.MeetupEntity", b =>
                {
                    b.Navigation("Feedbacks");

                    b.Navigation("MeetupPresentations");
                });

            modelBuilder.Entity("MeetupTelegramBot.DataAccess.Entities.MeetupPresentationEntity", b =>
                {
                    b.Navigation("Questions");
                });

            modelBuilder.Entity("MeetupTelegramBot.DataAccess.Entities.PresentationEntity", b =>
                {
                    b.Navigation("MeetupPresentations");
                });

            modelBuilder.Entity("MeetupTelegramBot.DataAccess.Entities.SpeackerEntity", b =>
                {
                    b.Navigation("Presentations");
                });
#pragma warning restore 612, 618
        }
    }
}
