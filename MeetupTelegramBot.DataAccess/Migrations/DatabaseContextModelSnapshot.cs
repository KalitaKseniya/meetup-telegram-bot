﻿// <auto-generated />
using System;
using MeetupTelegramBot.DataAccess.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MeetupTelegramBot.DataAccess.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    partial class DatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
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

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("FutureProposal")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("GeneralFeedback")
                        .HasColumnType("nvarchar(max)");

                    b.Property<TimeSpan>("Time")
                        .HasColumnType("time");

                    b.HasKey("Id");

                    b.ToTable("Feedbacks");
                });

            modelBuilder.Entity("MeetupTelegramBot.DataAccess.Entities.PresentationEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDisplayed")
                        .HasColumnType("bit");

                    b.Property<string>("SpeackerName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Presentations");
                });

            modelBuilder.Entity("MeetupTelegramBot.DataAccess.Entities.QuestionEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AuthorName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("PresentationId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Text")
                        .HasColumnType("nvarchar(max)");

                    b.Property<TimeSpan>("Time")
                        .HasColumnType("time");

                    b.HasKey("Id");

                    b.ToTable("Questions");
                });
#pragma warning restore 612, 618
        }
    }
}
