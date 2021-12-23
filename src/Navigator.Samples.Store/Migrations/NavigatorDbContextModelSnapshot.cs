﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Navigator.Extensions.Store.Context;

#nullable disable

namespace Navigator.Samples.Store.Migrations
{
    [DbContext(typeof(NavigatorDbContext))]
    partial class NavigatorDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.1");

            modelBuilder.Entity("Navigator.Entities.Chat", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Chat");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Chat");
                });

            modelBuilder.Entity("Navigator.Entities.Conversation", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Conversation");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Conversation");
                });

            modelBuilder.Entity("Navigator.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("User");

                    b.HasDiscriminator<string>("Discriminator").HasValue("User");
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

            modelBuilder.Entity("Navigator.Extensions.Store.Entities.ChatProfile", b =>
                {
                    b.HasBaseType("Navigator.Extensions.Store.Entities.UniversalProfile");

                    b.Property<Guid?>("DataId")
                        .HasColumnType("TEXT")
                        .HasColumnName("ChatProfile_DataId");

                    b.Property<Guid?>("UniversalChatId")
                        .HasColumnType("TEXT");

                    b.HasIndex("DataId")
                        .IsUnique();

                    b.HasIndex("UniversalChatId");

                    b.HasDiscriminator().HasValue("ChatProfile");
                });

            modelBuilder.Entity("Navigator.Extensions.Store.Entities.ConversationProfile", b =>
                {
                    b.HasBaseType("Navigator.Extensions.Store.Entities.UniversalProfile");

                    b.Property<Guid?>("DataId")
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("UniversalConversationId")
                        .HasColumnType("TEXT");

                    b.HasIndex("DataId")
                        .IsUnique();

                    b.HasIndex("UniversalConversationId");

                    b.HasDiscriminator().HasValue("ConversationProfile");
                });

            modelBuilder.Entity("Navigator.Extensions.Store.Entities.UniversalChat", b =>
                {
                    b.HasBaseType("Navigator.Entities.Chat");

                    b.Property<DateTime>("FirstInteractionAt")
                        .HasColumnType("TEXT");

                    b.HasDiscriminator().HasValue("UniversalChat");
                });

            modelBuilder.Entity("Navigator.Extensions.Store.Entities.UniversalConversation", b =>
                {
                    b.HasBaseType("Navigator.Entities.Conversation");

                    b.Property<Guid>("ChatId")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("FirstInteractionAt")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("UserId")
                        .HasColumnType("TEXT");

                    b.HasIndex("ChatId");

                    b.HasIndex("UserId");

                    b.HasDiscriminator().HasValue("UniversalConversation");
                });

            modelBuilder.Entity("Navigator.Extensions.Store.Entities.UniversalUser", b =>
                {
                    b.HasBaseType("Navigator.Entities.User");

                    b.Property<DateTime>("FirstInteractionAt")
                        .HasColumnType("TEXT");

                    b.HasDiscriminator().HasValue("UniversalUser");
                });

            modelBuilder.Entity("Navigator.Extensions.Store.Entities.UserProfile", b =>
                {
                    b.HasBaseType("Navigator.Extensions.Store.Entities.UniversalProfile");

                    b.Property<Guid?>("DataId")
                        .HasColumnType("TEXT")
                        .HasColumnName("UserProfile_DataId");

                    b.Property<Guid?>("UniversalUserId")
                        .HasColumnType("TEXT");

                    b.HasIndex("DataId")
                        .IsUnique();

                    b.HasIndex("UniversalUserId");

                    b.HasDiscriminator().HasValue("UserProfile");
                });

            modelBuilder.Entity("Navigator.Providers.Telegram.Entities.TelegramChat", b =>
                {
                    b.HasBaseType("Navigator.Entities.Chat");

                    b.Property<long>("ExternalIdentifier")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Title")
                        .HasColumnType("TEXT");

                    b.HasDiscriminator().HasValue("TelegramChat");
                });

            modelBuilder.Entity("Navigator.Providers.Telegram.Entities.TelegramConversation", b =>
                {
                    b.HasBaseType("Navigator.Entities.Conversation");

                    b.Property<Guid>("ChatId")
                        .HasColumnType("TEXT")
                        .HasColumnName("TelegramConversation_ChatId");

                    b.Property<Guid>("UserId")
                        .HasColumnType("TEXT")
                        .HasColumnName("TelegramConversation_UserId");

                    b.HasIndex("ChatId");

                    b.HasIndex("UserId");

                    b.HasDiscriminator().HasValue("TelegramConversation");
                });

            modelBuilder.Entity("Navigator.Providers.Telegram.Entities.TelegramUser", b =>
                {
                    b.HasBaseType("Navigator.Entities.User");

                    b.Property<long>("ExternalIdentifier")
                        .HasColumnType("INTEGER");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("LanguageCode")
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .HasColumnType("TEXT");

                    b.Property<string>("Username")
                        .HasColumnType("TEXT");

                    b.HasDiscriminator().HasValue("TelegramUser");
                });

            modelBuilder.Entity("Navigator.Extensions.Store.Telegram.TelegramChatProfile", b =>
                {
                    b.HasBaseType("Navigator.Extensions.Store.Entities.ChatProfile");

                    b.HasDiscriminator().HasValue("TelegramChatProfile");
                });

            modelBuilder.Entity("Navigator.Extensions.Store.Telegram.TelegramConversationProfile", b =>
                {
                    b.HasBaseType("Navigator.Extensions.Store.Entities.ConversationProfile");

                    b.HasDiscriminator().HasValue("TelegramConversationProfile");
                });

            modelBuilder.Entity("Navigator.Extensions.Store.Telegram.TelegramUserProfile", b =>
                {
                    b.HasBaseType("Navigator.Extensions.Store.Entities.UserProfile");

                    b.HasDiscriminator().HasValue("TelegramUserProfile");
                });

            modelBuilder.Entity("Navigator.Extensions.Store.Entities.ChatProfile", b =>
                {
                    b.HasOne("Navigator.Entities.Chat", "Data")
                        .WithOne()
                        .HasForeignKey("Navigator.Extensions.Store.Entities.ChatProfile", "DataId");

                    b.HasOne("Navigator.Extensions.Store.Entities.UniversalChat", null)
                        .WithMany("Profiles")
                        .HasForeignKey("UniversalChatId");

                    b.Navigation("Data");
                });

            modelBuilder.Entity("Navigator.Extensions.Store.Entities.ConversationProfile", b =>
                {
                    b.HasOne("Navigator.Entities.Conversation", "Data")
                        .WithOne()
                        .HasForeignKey("Navigator.Extensions.Store.Entities.ConversationProfile", "DataId");

                    b.HasOne("Navigator.Extensions.Store.Entities.UniversalConversation", null)
                        .WithMany("Profiles")
                        .HasForeignKey("UniversalConversationId");

                    b.Navigation("Data");
                });

            modelBuilder.Entity("Navigator.Extensions.Store.Entities.UniversalConversation", b =>
                {
                    b.HasOne("Navigator.Extensions.Store.Entities.UniversalChat", "Chat")
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

            modelBuilder.Entity("Navigator.Extensions.Store.Entities.UserProfile", b =>
                {
                    b.HasOne("Navigator.Entities.User", "Data")
                        .WithOne()
                        .HasForeignKey("Navigator.Extensions.Store.Entities.UserProfile", "DataId");

                    b.HasOne("Navigator.Extensions.Store.Entities.UniversalUser", null)
                        .WithMany("Profiles")
                        .HasForeignKey("UniversalUserId");

                    b.Navigation("Data");
                });

            modelBuilder.Entity("Navigator.Providers.Telegram.Entities.TelegramConversation", b =>
                {
                    b.HasOne("Navigator.Providers.Telegram.Entities.TelegramChat", "Chat")
                        .WithMany()
                        .HasForeignKey("ChatId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Navigator.Providers.Telegram.Entities.TelegramUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Chat");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Navigator.Extensions.Store.Entities.UniversalChat", b =>
                {
                    b.Navigation("Conversations");

                    b.Navigation("Profiles");
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
#pragma warning restore 612, 618
        }
    }
}
