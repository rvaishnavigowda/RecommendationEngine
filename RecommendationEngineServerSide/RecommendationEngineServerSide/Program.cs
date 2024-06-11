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
            //Console.WriteLine("enter the new menu:");
            //MenuDTO menu = new MenuDTO();
            //Console.WriteLine("enter the menu Type:");
            //menu.MenuType=Console.ReadLine();
            //Console.WriteLine("enter menu name:");
            //menu.MenuName=Console.ReadLine();
            //Console.WriteLine("enter the item price");
            //menu.MenuPrice=Convert.ToInt32(Console.ReadLine());
            
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
                    services.AddScoped<IAdminService, AdminService>();
                    services.AddAutoMapper(typeof(Mapper));
                    services.AddScoped<IRegisterService,RegisterService>();
                    services.AddScoped<IEmplyoeeService, EmplyoeeService>();
                    services.AddScoped<INotificationService, NotificationService>();
                    services.AddScoped<AdminController>(); 
                });
    }
}

 