using B3Challenge.Business.Interfaces;
using B3Challenge.Rabbit;
using B3Challenge.Repository;
using B3Challenge.Repository.Task;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B3Challenge.Business
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            #region Repositories
            services.AddScoped<TaskRepository, TaskRepository>();
            #endregion

            #region Business
            services.AddScoped<ITaskBusiness, TaskBusiness>();

            #endregion

            #region Others
            services.AddScoped<IRabbitConsumer, RabbitConsumer>(opt => {
                    var hostName = configuration["Rabbit:Host"];
                    var userName = configuration["Rabbit:User"];
                    var password = configuration["Rabbit:Password"];
                    return new RabbitConsumer(hostName, userName, password);
            });

            services.AddScoped<IRabbitSender, RabbitSender>(opt => {
                var hostName = configuration["Rabbit:Host"];
                var userName = configuration["Rabbit:User"];
                var password = configuration["Rabbit:Password"];
                return new RabbitSender(hostName, userName, password);
            });



            #endregion

            return services;

        }
    }
}
