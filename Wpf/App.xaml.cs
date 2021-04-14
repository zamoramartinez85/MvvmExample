using AppContext.DAL;
using AppContext.Models;
using Microsoft.AspNet.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Debug;
using Repository.Repository;
using Serilog;
using Services.AuthenticationService;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Wpf
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        private IServiceProvider serviceProvider;
        protected override void OnStartup(StartupEventArgs e)
        {

            serviceProvider = CreateServiceProvider();
            TestServiceProvider();

            base.OnStartup(e);  
        }

        private void TestServiceProvider()
        {
            IAuthenticationService authenticationService = serviceProvider.GetRequiredService<IAuthenticationService>();
            if (authenticationService.Login("David", "1234") == null)
            {
                bool registrationTest = authenticationService.Register("David", "1234", "1234");
                User loginTest = authenticationService.Login("David", "1234");
            }
        }

        private IServiceProvider CreateServiceProvider()
        {
            IServiceCollection services = new ServiceCollection();

            services.AddSingleton<IPasswordHasher, PasswordHasher>();
            services.AddSingleton(x =>
            {
                ILoggerFactory loggerFactory = LoggerFactory.Create(builder =>
                {
                    LoggerConfiguration loggerConfiguration = new LoggerConfiguration().WriteTo.File("log.txt", rollingInterval: RollingInterval.Day);

                    builder.ClearProviders();
                    builder.AddDebug();
                    builder.AddSerilog(loggerConfiguration.CreateLogger());
                });
                return loggerFactory.CreateLogger("Logger");
            });

            services.AddSingleton<Context>();
            services.AddSingleton<IRepository, Repository.Repository.Repository>();
            services.AddSingleton<IAuthenticationService, AuthenticationService>();

            return services.BuildServiceProvider();
        }
    }
}
