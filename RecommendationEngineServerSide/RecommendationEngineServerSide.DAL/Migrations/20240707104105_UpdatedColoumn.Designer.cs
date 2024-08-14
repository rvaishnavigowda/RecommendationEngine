﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RecommendationEngineServerSide.DAL.Context;

#nullable disable

namespace RecommendationEngineServerSide.DAL.Migrations
{
    [DbContext(typeof(DBContext))]
    [Migration("20240707104105_UpdatedColoumn")]
    partial class UpdatedColoumn
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true)
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("RecommendationEngineServerSide.DAL.Model.DailyMenu", b =>
                {
                    b.Property<int>("DailyMenuId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("DailyMenuId"));

                    b.Property<DateTime>("DailyMenuDate")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("ISDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("MenuId")
                        .HasColumnType("int");

                    b.HasKey("DailyMenuId");

                    b.HasIndex("MenuId");

                    b.ToTable("DailyMenu");
                });

            modelBuilder.Entity("RecommendationEngineServerSide.DAL.Model.Feedback", b =>
                {
                    b.Property<int>("FeedbackId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("FeedbackId"));

                    b.Property<string>("Comment")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("FeedbackDate")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("ISDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("MenuId")
                        .HasColumnType("int");

                    b.Property<int>("Rating")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("FeedbackId");

                    b.HasIndex("MenuId");

                    b.HasIndex("UserId");

                    b.ToTable("Feedback");
                });

            modelBuilder.Entity("RecommendationEngineServerSide.DAL.Model.Log", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Callsite")
                        .HasColumnType("longtext");

                    b.Property<string>("Exception")
                        .HasColumnType("longtext");

                    b.Property<string>("Level")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("Logged")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Logger")
                        .HasColumnType("longtext");

                    b.Property<string>("Message")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Logs");
                });

            modelBuilder.Entity("RecommendationEngineServerSide.DAL.Model.Menu", b =>
                {
                    b.Property<int>("MenuId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("MenuId"));

                    b.Property<DateTime>("MenuCreationDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("MenuName")
                        .HasColumnType("longtext");

                    b.Property<int>("MenuStatus")
                        .HasColumnType("int");

                    b.Property<int>("MenuTypeId")
                        .HasColumnType("int");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(65,30)");

                    b.HasKey("MenuId");

                    b.HasIndex("MenuTypeId");

                    b.ToTable("Menu");
                });

            modelBuilder.Entity("RecommendationEngineServerSide.DAL.Model.MenuFeedback", b =>
                {
                    b.Property<int>("MenuFeedbackId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("MenuFeedbackId"));

                    b.Property<int?>("MenuFeedbackQuestionId")
                        .HasColumnType("int");

                    b.Property<int>("MenuId")
                        .HasColumnType("int");

                    b.Property<int>("QuestionId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("MenuFeedbackId");

                    b.HasIndex("MenuFeedbackQuestionId");

                    b.HasIndex("MenuId");

                    b.HasIndex("UserId");

                    b.ToTable("MenuFeedback");
                });

            modelBuilder.Entity("RecommendationEngineServerSide.DAL.Model.MenuFeedbackQuestion", b =>
                {
                    b.Property<int>("MenuFeedbackQuestionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("MenuFeedbackQuestionId"));

                    b.Property<string>("MenuFeedbackQuestionTitle")
                        .HasColumnType("longtext");

                    b.HasKey("MenuFeedbackQuestionId");

                    b.ToTable("MenuFeedbackQuestion");
                });

            modelBuilder.Entity("RecommendationEngineServerSide.DAL.Model.MenuType", b =>
                {
                    b.Property<int>("MenuTypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("MenuTypeId"));

                    b.Property<bool>("ISDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("MenuTypeName")
                        .HasColumnType("longtext");

                    b.HasKey("MenuTypeId");

                    b.ToTable("MenuType");
                });

            modelBuilder.Entity("RecommendationEngineServerSide.DAL.Model.Notification", b =>
                {
                    b.Property<int>("NotificationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("NotificationId"));

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime>("NotificationDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("NotificationMessage")
                        .HasColumnType("longtext");

                    b.Property<int>("NotificationTypeId")
                        .HasColumnType("int");

                    b.HasKey("NotificationId");

                    b.HasIndex("NotificationTypeId");

                    b.ToTable("Notification");
                });

            modelBuilder.Entity("RecommendationEngineServerSide.DAL.Model.NotificationType", b =>
                {
                    b.Property<int>("NotificationTypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("NotificationTypeId"));

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("NotificationTypeName")
                        .HasColumnType("longtext");

                    b.HasKey("NotificationTypeId");

                    b.ToTable("notificationType");
                });

            modelBuilder.Entity("RecommendationEngineServerSide.DAL.Model.Order", b =>
                {
                    b.Property<int>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("OrderId"));

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("MenuTypeId")
                        .HasColumnType("int");

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("OrderId");

                    b.HasIndex("MenuTypeId");

                    b.HasIndex("UserId");

                    b.ToTable("Order");
                });

            modelBuilder.Entity("RecommendationEngineServerSide.DAL.Model.ProfileAnswer", b =>
                {
                    b.Property<int>("PAId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("PAId"));

                    b.Property<string>("ProfileAnswerSolution")
                        .HasColumnType("longtext");

                    b.Property<int>("ProfileQuestionId")
                        .HasColumnType("int");

                    b.HasKey("PAId");

                    b.HasIndex("ProfileQuestionId");

                    b.ToTable("ProfileAnswer");
                });

            modelBuilder.Entity("RecommendationEngineServerSide.DAL.Model.ProfileQuestion", b =>
                {
                    b.Property<int>("PQId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("PQId"));

                    b.Property<string>("Question")
                        .HasColumnType("longtext");

                    b.HasKey("PQId");

                    b.ToTable("ProfileQuestion");
                });

            modelBuilder.Entity("RecommendationEngineServerSide.DAL.Model.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("UserId"));

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Password")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("UserCreationDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("UserName")
                        .HasColumnType("longtext");

                    b.Property<int>("UserTypeId")
                        .HasColumnType("int");

                    b.HasKey("UserId");

                    b.HasIndex("UserTypeId");

                    b.ToTable("User");
                });

            modelBuilder.Entity("RecommendationEngineServerSide.DAL.Model.UserNotification", b =>
                {
                    b.Property<int>("UserNotificationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("UserNotificationId"));

                    b.Property<int>("NotificationId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("UserNotificationId");

                    b.HasIndex("NotificationId");

                    b.HasIndex("UserId");

                    b.ToTable("UserNotification");
                });

            modelBuilder.Entity("RecommendationEngineServerSide.DAL.Model.UserOrder", b =>
                {
                    b.Property<int>("UserOrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("UserOrderId"));

                    b.Property<int>("DailyMenuId")
                        .HasColumnType("int");

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.HasKey("UserOrderId");

                    b.HasIndex("DailyMenuId");

                    b.HasIndex("OrderId");

                    b.ToTable("UserOrder");
                });

            modelBuilder.Entity("RecommendationEngineServerSide.DAL.Model.UserProfile", b =>
                {
                    b.Property<int>("UserProfileId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("UserProfileId"));

                    b.Property<int>("ProfileAnswerId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("UserProfileId");

                    b.HasIndex("ProfileAnswerId");

                    b.HasIndex("UserId");

                    b.ToTable("UserProfile");
                });

            modelBuilder.Entity("RecommendationEngineServerSide.DAL.Model.UserType", b =>
                {
                    b.Property<int>("UserTypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("UserTypeId"));

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("UserTypeName")
                        .HasColumnType("longtext");

                    b.HasKey("UserTypeId");

                    b.ToTable("UserType");
                });

            modelBuilder.Entity("RecommendationEngineServerSide.DAL.Model.DailyMenu", b =>
                {
                    b.HasOne("RecommendationEngineServerSide.DAL.Model.Menu", "Menu")
                        .WithMany()
                        .HasForeignKey("MenuId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Menu");
                });

            modelBuilder.Entity("RecommendationEngineServerSide.DAL.Model.Feedback", b =>
                {
                    b.HasOne("RecommendationEngineServerSide.DAL.Model.Menu", "Menu")
                        .WithMany()
                        .HasForeignKey("MenuId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RecommendationEngineServerSide.DAL.Model.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Menu");

                    b.Navigation("User");
                });

            modelBuilder.Entity("RecommendationEngineServerSide.DAL.Model.Menu", b =>
                {
                    b.HasOne("RecommendationEngineServerSide.DAL.Model.MenuType", "MenuType")
                        .WithMany()
                        .HasForeignKey("MenuTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MenuType");
                });

            modelBuilder.Entity("RecommendationEngineServerSide.DAL.Model.MenuFeedback", b =>
                {
                    b.HasOne("RecommendationEngineServerSide.DAL.Model.MenuFeedbackQuestion", "MenuFeedbackQuestion")
                        .WithMany()
                        .HasForeignKey("MenuFeedbackQuestionId");

                    b.HasOne("RecommendationEngineServerSide.DAL.Model.Menu", "Menu")
                        .WithMany()
                        .HasForeignKey("MenuId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RecommendationEngineServerSide.DAL.Model.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Menu");

                    b.Navigation("MenuFeedbackQuestion");

                    b.Navigation("User");
                });

            modelBuilder.Entity("RecommendationEngineServerSide.DAL.Model.Notification", b =>
                {
                    b.HasOne("RecommendationEngineServerSide.DAL.Model.NotificationType", "NotificationType")
                        .WithMany()
                        .HasForeignKey("NotificationTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("NotificationType");
                });

            modelBuilder.Entity("RecommendationEngineServerSide.DAL.Model.Order", b =>
                {
                    b.HasOne("RecommendationEngineServerSide.DAL.Model.MenuType", "MenuType")
                        .WithMany()
                        .HasForeignKey("MenuTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RecommendationEngineServerSide.DAL.Model.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MenuType");

                    b.Navigation("User");
                });

            modelBuilder.Entity("RecommendationEngineServerSide.DAL.Model.ProfileAnswer", b =>
                {
                    b.HasOne("RecommendationEngineServerSide.DAL.Model.ProfileQuestion", "ProfileQuestion")
                        .WithMany()
                        .HasForeignKey("ProfileQuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ProfileQuestion");
                });

            modelBuilder.Entity("RecommendationEngineServerSide.DAL.Model.User", b =>
                {
                    b.HasOne("RecommendationEngineServerSide.DAL.Model.UserType", "UserType")
                        .WithMany()
                        .HasForeignKey("UserTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserType");
                });

            modelBuilder.Entity("RecommendationEngineServerSide.DAL.Model.UserNotification", b =>
                {
                    b.HasOne("RecommendationEngineServerSide.DAL.Model.Notification", "Notification")
                        .WithMany()
                        .HasForeignKey("NotificationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RecommendationEngineServerSide.DAL.Model.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Notification");

                    b.Navigation("User");
                });

            modelBuilder.Entity("RecommendationEngineServerSide.DAL.Model.UserOrder", b =>
                {
                    b.HasOne("RecommendationEngineServerSide.DAL.Model.DailyMenu", "DailyMenu")
                        .WithMany()
                        .HasForeignKey("DailyMenuId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RecommendationEngineServerSide.DAL.Model.Order", "Order")
                        .WithMany()
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DailyMenu");

                    b.Navigation("Order");
                });

            modelBuilder.Entity("RecommendationEngineServerSide.DAL.Model.UserProfile", b =>
                {
                    b.HasOne("RecommendationEngineServerSide.DAL.Model.ProfileAnswer", "ProfileAnswer")
                        .WithMany()
                        .HasForeignKey("ProfileAnswerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RecommendationEngineServerSide.DAL.Model.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ProfileAnswer");

                    b.Navigation("User");
                });
#pragma warning restore 612, 618
        }
    }
}
