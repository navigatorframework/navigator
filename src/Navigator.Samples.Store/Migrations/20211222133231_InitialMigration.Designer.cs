﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Navigator.Extensions.Store;

#nullable disable

namespace Navigator.Samples.Store.Migrations
{
    [DbContext(typeof(NavigatorDbContext))]
    [Migration("20211222133231_InitialMigration")]
    partial class InitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.1");

            modelBuilder.Entity("Navigator.Extensions.Store.Entities.UniversalConversation", b =>
                {
                    b.Property<Guid>("ChatId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("UserId")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("FirstInteractionAt")
                        .HasColumnType("TEXT");

                    b.HasKey("ChatId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("Conversations");
                });

            modelBuilder.Entity("Navigator.Extensions.Store.Entities.UniversalProfile", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("Identification")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("LastUpdatedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("Provider")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Profiles");

                    b.HasDiscriminator<string>("Discriminator").HasValue("UniversalProfile");
                });

            modelBuilder.Entity("Navigator.Extensions.Store.Entities.UniversalUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("FirstInteractionAt")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("UniversalChat", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("FirstInteractionAt")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Chats");
                });

            modelBuilder.Entity("Navigator.Extensions.Store.Entities.ChatProfile", b =>
                {
                    b.HasBaseType("Navigator.Extensions.Store.Entities.UniversalProfile");

                    b.Property<Guid?>("UniversalChatId")
                        .HasColumnType("TEXT");

                    b.HasIndex("UniversalChatId");

                    b.HasDiscriminator().HasValue("ChatProfile");
                });

            modelBuilder.Entity("Navigator.Extensions.Store.Entities.ConversationProfile", b =>
                {
                    b.HasBaseType("Navigator.Extensions.Store.Entities.UniversalProfile");

                    b.Property<Guid?>("UniversalConversationChatId")
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("UniversalConversationUserId")
                        .HasColumnType("TEXT");

                    b.HasIndex("UniversalConversationChatId", "UniversalConversationUserId");

                    b.HasDiscriminator().HasValue("ConversationProfile");
                });

            modelBuilder.Entity("Navigator.Extensions.Store.Entities.UserProfile", b =>
                {
                    b.HasBaseType("Navigator.Extensions.Store.Entities.UniversalProfile");

                    b.Property<Guid?>("UniversalUserId")
                        .HasColumnType("TEXT");

                    b.HasIndex("UniversalUserId");

                    b.HasDiscriminator().HasValue("UserProfile");
                });

            modelBuilder.Entity("Navigator.Extensions.Store.Entities.UniversalConversation", b =>
                {
                    b.HasOne("UniversalChat", "Chat")
                        .WithMany("Conversations")
                        .HasForeignKey("ChatId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Navigator.Extensions.Store.Entities.UniversalUser", "User")
                        .WithMany("Conversations")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Chat");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Navigator.Extensions.Store.Entities.ChatProfile", b =>
                {
                    b.HasOne("UniversalChat", null)
                        .WithMany("Profiles")
                        .HasForeignKey("UniversalChatId");
                });

            modelBuilder.Entity("Navigator.Extensions.Store.Entities.ConversationProfile", b =>
                {
                    b.HasOne("Navigator.Extensions.Store.Entities.UniversalConversation", null)
                        .WithMany("Profiles")
                        .HasForeignKey("UniversalConversationChatId", "UniversalConversationUserId");
                });

            modelBuilder.Entity("Navigator.Extensions.Store.Entities.UserProfile", b =>
                {
                    b.HasOne("Navigator.Extensions.Store.Entities.UniversalUser", null)
                        .WithMany("Profiles")
                        .HasForeignKey("UniversalUserId");
                });

            modelBuilder.Entity("Navigator.Extensions.Store.Entities.UniversalConversation", b =>
                {
                    b.Navigation("Profiles");
                });

            modelBuilder.Entity("Navigator.Extensions.Store.Entities.UniversalUser", b =>
                {
                    b.Navigation("Conversations");

                    b.Navigation("Profiles");
                });

            modelBuilder.Entity("UniversalChat", b =>
                {
                    b.Navigation("Conversations");

                    b.Navigation("Profiles");
                });
#pragma warning restore 612, 618
        }
    }
}