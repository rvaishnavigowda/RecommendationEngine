using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading.Tasks;
using RecommendationEngineServerSide.DAL.Context;

namespace RecommendationEngine
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await CreateHostBuilder(args).RunConsoleAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    // Add your services here
                    //services.AddSingleton<>(); // Example server service
                    //services.AddSingleton<YourClientService>(); // Example client service
                    services.AddDbContext<DBContext>(options =>
                        options.UseMySql("server=localhost;database=recommendationengine;user=root;password=root",
                                         new MySqlServerVersion(new Version(8, 0, 21))));
                    

                });
    }
}
