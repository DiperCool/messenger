﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Web.Models.Db;

namespace Web.Models.Migrations
{
    [DbContext(typeof(Context))]
    [Migration("20201211120824_init2")]
    partial class init2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityByDefaultColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("Web.Models.Entity.Chat", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .UseIdentityByDefaultColumn();

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int?>("CreatorId")
                        .HasColumnType("integer");

                    b.Property<int?>("LastMessageId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("LastUpdate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("guid")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CreatorId");

                    b.HasIndex("LastMessageId");

                    b.ToTable("Chats");
                });

            modelBuilder.Entity("Web.Models.Entity.ChatUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .UseIdentityByDefaultColumn();

                    b.Property<int?>("ChatId")
                        .HasColumnType("integer");

                    b.Property<int?>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ChatId");

                    b.HasIndex("UserId");

                    b.ToTable("ChatsUsers");
                });

            modelBuilder.Entity("Web.Models.Entity.Media", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .UseIdentityByDefaultColumn();

                    b.Property<int?>("ChatId")
                        .HasColumnType("integer");

                    b.Property<int?>("MessageId")
                        .HasColumnType("integer");

                    b.Property<string>("PathPhysic")
                        .HasColumnType("text");

                    b.Property<string>("PathWeb")
                        .HasColumnType("text");

                    b.Property<int?>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ChatId");

                    b.HasIndex("MessageId");

                    b.HasIndex("UserId");

                    b.ToTable("Medias");
                });

            modelBuilder.Entity("Web.Models.Entity.Message", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .UseIdentityByDefaultColumn();

                    b.Property<int>("ChatGuid")
                        .HasColumnType("integer");

                    b.Property<int?>("ChatId")
                        .HasColumnType("integer");

                    b.Property<string>("Content")
                        .HasColumnType("text");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int?>("CreatorId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ChatId");

                    b.HasIndex("CreatorId");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("Web.Models.Entity.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .UseIdentityByDefaultColumn();

                    b.Property<DateTime>("CreatedTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int?>("CurrentAvaId")
                        .HasColumnType("integer");

                    b.Property<string>("Login")
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .HasColumnType("text");

                    b.Property<string>("RefreshToken")
                        .HasColumnType("text");

                    b.Property<bool>("isOnline")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.HasIndex("CurrentAvaId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Web.Models.Entity.Chat", b =>
                {
                    b.HasOne("Web.Models.Entity.User", "Creator")
                        .WithMany()
                        .HasForeignKey("CreatorId");

                    b.HasOne("Web.Models.Entity.Message", "LastMessage")
                        .WithMany()
                        .HasForeignKey("LastMessageId");

                    b.Navigation("Creator");

                    b.Navigation("LastMessage");
                });

            modelBuilder.Entity("Web.Models.Entity.ChatUser", b =>
                {
                    b.HasOne("Web.Models.Entity.Chat", "Chat")
                        .WithMany()
                        .HasForeignKey("ChatId");

                    b.HasOne("Web.Models.Entity.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("Chat");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Web.Models.Entity.Media", b =>
                {
                    b.HasOne("Web.Models.Entity.Chat", null)
                        .WithMany("Avas")
                        .HasForeignKey("ChatId");

                    b.HasOne("Web.Models.Entity.Message", null)
                        .WithMany("Medias")
                        .HasForeignKey("MessageId");

                    b.HasOne("Web.Models.Entity.User", null)
                        .WithMany("Avas")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Web.Models.Entity.Message", b =>
                {
                    b.HasOne("Web.Models.Entity.Chat", null)
                        .WithMany("Messages")
                        .HasForeignKey("ChatId");

                    b.HasOne("Web.Models.Entity.User", "Creator")
                        .WithMany()
                        .HasForeignKey("CreatorId");

                    b.Navigation("Creator");
                });

            modelBuilder.Entity("Web.Models.Entity.User", b =>
                {
                    b.HasOne("Web.Models.Entity.Media", "CurrentAva")
                        .WithMany()
                        .HasForeignKey("CurrentAvaId");

                    b.Navigation("CurrentAva");
                });

            modelBuilder.Entity("Web.Models.Entity.Chat", b =>
                {
                    b.Navigation("Avas");

                    b.Navigation("Messages");
                });

            modelBuilder.Entity("Web.Models.Entity.Message", b =>
                {
                    b.Navigation("Medias");
                });

            modelBuilder.Entity("Web.Models.Entity.User", b =>
                {
                    b.Navigation("Avas");
                });
#pragma warning restore 612, 618
        }
    }
}
