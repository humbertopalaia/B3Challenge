using B3Challenge.Business;
using B3Challenge.Business.Interfaces;
using B3Challenge.Rabbit;
using B3Challenge.Repository;
using B3Challenge.Repository.Task;
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
using static System.Net.Mime.MediaTypeNames;

namespace B3Challenge.Task.Worker
{
    internal class Program
    {

        public static void Main(string[] args)
        {


            var host = CreateHostBuilder(args).Build();
            var app = host.Services.GetRequiredService<Worker>();

            app.Run();

        }

        static IHostBuilder CreateHostBuilder(string[] args)
        {

            var builder = Host.CreateDefaultBuilder(args);

            return Host.CreateDefaultBuilder(args)
                .ConfigureServices(
                    (_, services) => ConfigureServices(services));
        }


        static void ConfigureServices(IServiceCollection services)
        {
            var configuration = GetConfiguration();

            services.AddAutoMapper(typeof(Program));
            services.AddLogging(loggingBuilder =>
             {
                 // configure Logging with NLog
                 loggingBuilder.ClearProviders();
                 loggingBuilder.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
                 loggingBuilder.AddNLog(configuration);
                 loggingBuilder.AddConsole();
             });

            services.AddScoped<MainDbContext>(s => new MainDbContext(configuration.GetConnectionString("Default")));
            services.AddSingleton<IConfiguration>(c => configuration);
            services.AddSingleton<Worker, Worker>();


            RegisterRepositories(services);
            RegisterBusiness(services);
            RegisterRabbit(services, configuration);
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


        static IServiceCollection RegisterRepositories(IServiceCollection services)
        {
            #region Repositories
            services.AddSingleton<TaskRepository, TaskRepository>();
            #endregion


            return services;
        }


        static IServiceCollection RegisterBusiness( IServiceCollection services)
        {
            #region Business
            services.AddSingleton<ITaskBusiness, TaskBusiness>();
            #endregion


            return services;

        }

        static IServiceCollection RegisterRabbit( IServiceCollection services, IConfiguration configuration)
        {
            #region Others
            services.AddSingleton<IRabbitConsumer, RabbitConsumer>(opt =>
            {
                var hostName = configuration["Rabbit:Host"];
                var userName = configuration["Rabbit:User"];
                var password = configuration["Rabbit:Password"];
                return new RabbitConsumer(hostName, userName, password);
            });

            services.AddSingleton<IRabbitSender, RabbitSender>(opt =>
            {
                var hostName = configuration["Rabbit:Host"];
                var userName = configuration["Rabbit:User"];
                var password = configuration["Rabbit:Password"];
                return new RabbitSender(hostName, userName, password);
            });

            #endregion

            return services;

        }


        //    public static IHostBuilder CreateHostBuilder(string[] args) =>
        //        Host.CreateDefaultBuilder(args)                
        //            .ConfigureServices((hostContext, services) =>
        //            {
        //                var configuration = GetConfiguration();
        //                services.AddSingleton<IConfiguration>(configuration);
        //                services.AddHostedService<Worker>();
        //                services.AddDbContext<MainDbContext>(options => options.UseSqlServer());
        //                services.AddAutoMapper(typeof(Program));
        //                services.AddLogging(loggingBuilder =>
        //                 {
        //                     // configure Logging with NLog
        //                     loggingBuilder.ClearProviders();
        //                     loggingBuilder.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
        //                     loggingBuilder.AddNLog(configuration);
        //                     loggingBuilder.AddConsole();
        //                 });

        //                DependencyInjectionConfig.RegisterAll(services, GetConfiguration());
        //            });
    }




}