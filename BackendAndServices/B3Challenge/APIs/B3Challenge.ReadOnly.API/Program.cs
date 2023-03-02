using B3Challenge.Business;
using B3Challenge.Repository;
using Microsoft.EntityFrameworkCore;

namespace B3Challenge.ReadOnly.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var configuration = GetConfiguration();

            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddSingleton<IConfiguration>(configuration);
            builder.Services.AddDbContext<MainDbContext>(options => options.UseSqlServer());
            builder.Services.AddAutoMapper(typeof(Program));


            DependencyInjectionConfig.RegisterRepositories(builder.Services);
            DependencyInjectionConfig.RegisterBusiness(builder.Services);

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
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

    }
}