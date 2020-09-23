using CryptoTax.Data.Context;
using CryptoTax.Web.Features.Authorization.Abstractions;
using CryptoTax.Web.Features.Authorization.Services;
using CryptoTax.Web.Features.Billing.Abstractions;
using CryptoTax.Web.Features.Billing.Services;
using CryptoTax.Web.Features.Reporting.Abstractions;
using CryptoTax.Web.Features.Reporting.Services;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CryptoTax.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            // Infrastructure Services
            services.AddValidatorsFromAssemblyContaining<Startup>();
            services.AddDbContext<CryptoTaxContext>(options =>
            {
                options.UseSqlite($"Data Source={nameof(CryptoTaxContext)}.db");
                options.UseLazyLoadingProxies();
                options.EnableSensitiveDataLogging();
            });

            // Web Services
            services.AddRazorPages();
            services.AddServerSideBlazor();

            // Business Logic Services
            services.AddScoped<ICurrentUserAccessor, CurrentUserAccessor>();
            services.AddScoped<IReportViewService, ReportViewService>();
            services.AddScoped<IBillingService, BillingService>();
            services.AddScoped<IPaymentService, PaymentService>();
            services.AddScoped<IPurchaseService, PurchaseService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
