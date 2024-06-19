using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RecommendationEngineServerSide.Common.AutoMapper;
using RecommendationEngineServerSide.Controller;
using RecommendationEngineServerSide.Controller.LoginControllers;
using RecommendationEngineServerSide.DAL.Context;
using RecommendationEngineServerSide.DAL.UnitfWork;
using RecommendationEngineServerSide.Service.AdminService;
using RecommendationEngineServerSide.Service.RegisterService;
using RecommendationEngineServerSide.Service.AdminService;
using System;
using System.Threading.Tasks;
using RecommendationEngineServerSide.Common.DTO;
using RecommendationEngineServerSide.Controller.AdminControllers;
using RecommendationEngineServerSide.Service.EmplyoeeService;
using RecommendationEngineServerSide.Service.NotificationService;
using RecommendationEngineServerSide.Service.ChefService;
using RecommendationEngineServerSide.Controller.ChefControllers;
using RecommendationEngineServerSide.Controller.EmployeeControllers;
using Microsoft.Extensions.Logging;
using RecommendationEngineServerSide.Service.RecommendationService;

namespace RecommendationEngineServerSide
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            // Start the socket server
            var socketServer = host.Services.GetRequiredService<SocketServer>();
            _ = socketServer.StartAsync();

            await host.RunAsync();
            
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddDbContext<DBContext>(options =>
                        options.UseMySql("server=localhost;database=recommendationengine;user=root;password=root",
                                         new MySqlServerVersion(new Version(8, 0, 21)))
                                .UseLazyLoadingProxies());
                    services.AddScoped<SocketServer>();
                    services.AddScoped<IUnitOfWork, UnitOfWork>();
                    services.AddScoped<ControllerRouter>();
                    services.AddScoped<LoginController>();
                    services.AddScoped<EmployeeController>();
                    services.AddScoped<ChefController>();
                    services.AddScoped<IAdminService, AdminService>();
                    services.AddAutoMapper(typeof(Mapper));
                    services.AddScoped<IRegisterService,RegisterService>();
                    services.AddScoped<IEmplyoeeService, EmplyoeeService>();
                    services.AddScoped<IRecommendationService,  RecommendationService>();
                    services.AddScoped<IChefService, ChefService>();
                    services.AddScoped<INotificationService, NotificationService>();
                    services.AddScoped<AdminController>();
                    services.AddLogging(config =>
                    {
                        config.ClearProviders();
                        config.AddConsole();
                        config.SetMinimumLevel(LogLevel.Warning);
                        config.AddFilter("Microsoft.EntityFrameworkCore.Database.Command", LogLevel.Warning);
                    });
                });
    }
}

