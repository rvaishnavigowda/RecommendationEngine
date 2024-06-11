using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using RecommendationEngineServerSide.DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pomelo.EntityFrameworkCore.MySql;

namespace RecommendationEngineServerSide.DAL.Context
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DBContext> options) : base(options)
        {
        }

        public DbSet<User> User { get; set; }
        public DbSet<UserType> UserType { get; set; }
        public DbSet<Menu> Menu { get; set; }
        public DbSet<MenuType> MenuType { get; set; }
        public DbSet<Feedback> Feedback { get; set; }
        public DbSet<DailyMenu> DailyMenu { get; set; }

        public DbSet<UserOrder> UserOrder { get; set; }

        public DbSet<Order> Order { get; set; }

        public DbSet<Notification> Notification { get; set; }

        public DbSet<NotificationType> notificationType { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    .UseLazyLoadingProxies()
                    .UseMySql("server=localhost;database=recommendationengine;user=root;password=root",
                              new MySqlServerVersion(new Version(8, 0, 21)));
            }
        }
    }
}
