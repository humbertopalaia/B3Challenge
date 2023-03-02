using B3Challenge.Business;
using B3Challenge.Repository;
using B3Challenge.Task.Worker;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace B3ChallengeTaskService
{
    internal class Program
    {
        private const string UserName = "guest";
        private const string Password = "guest";
        private const string HostName = "localhost";

        public static void Main(string[] args)
        {
            var logger = NLog.LogManager.GetLogger("UpperLevel");
            logger.Info("init main");

            CreateHostBuilder(args).Build().Run();
        }

        static IConfiguration GetConfiguration()
        {
            var path = Directory.GetCurrentDirectory();
            var configuration = new ConfigurationBuilder()
              .SetBasePath(path)
              .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
              .Build();

            return configuration;
        }


        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)                
                .ConfigureServices((hostContext, services) =>
                {
                    var configuration = GetConfiguration();
                    services.AddSingleton<IConfiguration>(configuration);
                    services.AddHostedService<Worker>();
                    services.AddDbContext<MainDbContext>(options => options.UseSqlServer());
                    services.AddAutoMapper(typeof(Program));
                    services.AddLogging(loggingBuilder =>
                     {
                         // configure Logging with NLog
                         loggingBuilder.ClearProviders();
                         loggingBuilder.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
                         loggingBuilder.AddNLog(configuration);
                         loggingBuilder.AddConsole();
                     });

                    DependencyInjectionConfig.RegisterAll(services, GetConfiguration());
                });
    }




}