using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Functions;
using Data;
using Shared.Interface;
using Shared.Service;
[assembly: FunctionsStartup(typeof(Startup))]

namespace Functions
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            var appSettings = new AppSettings();
            builder.GetContext()
                .Configuration.GetSection("AppSettings")
                .Bind(appSettings);

            builder.Services.AddSingleton(appSettings);

            builder.Services.AddDbContext<VisitContext>(
                (serviceProvider, options) => { options.UseSqlServer(appSettings.ConnectionStrings.Orders); }, ServiceLifetime.Transient);

            builder.Services.AddTransient<IVisitService, VisitService>();
        }

        public override void ConfigureAppConfiguration(IFunctionsConfigurationBuilder builder)
        {
            var applicationRootPath = builder.GetContext().ApplicationRootPath;
            builder.ConfigurationBuilder.SetBasePath(applicationRootPath);
        }
    }
}