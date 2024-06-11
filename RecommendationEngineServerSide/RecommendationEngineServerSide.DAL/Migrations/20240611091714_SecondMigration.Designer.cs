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
    [Migration("20240611091714_SecondMigration")]
    partial class SecondMigration
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

                    b.Property<int>("DailyMenuName")
                        .HasColumnType("int");

                    b.Property<int>("ISDeleted")
                        .HasColumnType("int");

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

                    b.Property<int>("ISDeleted")
                        .HasColumnType("int");

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

            modelBuilder.Entity("RecommendationEngineServerSide.DAL.Model.Menu", b =>
                {
                    b.Property<int>("MenuId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("MenuId"));

                    b.Property<int>("ISDeleted")
                        .HasColumnType("int");

                    b.Property<DateTime>("MenuCreationDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("MenuName")
                        .HasColumnType("longtext");

                    b.Property<int>("MenuTypeId")
                        .HasColumnType("int");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(65,30)");

                    b.HasKey("MenuId");

                    b.HasIndex("MenuTypeId");

                    b.ToTable("Menu");
                });

            modelBuilder.Entity("RecommendationEngineServerSide.DAL.Model.MenuType", b =>
                {
                    b.Property<int>("MenuTypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("MenuTypeId"));

                    b.Property<int>("ISDeleted")
                        .HasColumnType("int");

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

                    b.Property<int>("IsDeleted")
                        .HasColumnType("int");

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

                    b.Property<int>("IsDeleted")
                        .HasColumnType("int");

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

                    b.Property<int>("IsDeleted")
                        .HasColumnType("int");

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

            modelBuilder.Entity("RecommendationEngineServerSide.DAL.Model.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("UserId"));

                    b.Property<int>("IsDeleted")
                        .HasColumnType("int");

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

            modelBuilder.Entity("RecommendationEngineServerSide.DAL.Model.UserType", b =>
                {
                    b.Property<int>("UserTypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("UserTypeId"));

                    b.Property<int>("IsDeleted")
                        .HasColumnType("int");

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

            modelBuilder.Entity("RecommendationEngineServerSide.DAL.Model.User", b =>
                {
                    b.HasOne("RecommendationEngineServerSide.DAL.Model.UserType", "UserType")
                        .WithMany()
                        .HasForeignKey("UserTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserType");
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
#pragma warning restore 612, 618
        }
    }
}
