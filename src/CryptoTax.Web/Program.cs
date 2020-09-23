using CryptoTax.Data.Context;
using CryptoTax.Data.Seeder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;

namespace CryptoTax.Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            // Setup DB Context
            using var scope = host.Services.CreateScope();

            var serviceProvider = scope.ServiceProvider;
            var dbContext = serviceProvider.GetRequiredService<CryptoTaxContext>();

            // Seed Data
            await CryptoTaxContextSeeder.SeedAsync(serviceProvider);

            // Run Application
            await host.RunAsync();        
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
