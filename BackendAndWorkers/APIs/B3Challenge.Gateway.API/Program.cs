using NLog;
using NLog.Web;

namespace B3Challenge.Gateway.API
{
    public class Program
    {
        public static void Main(string[] args)
        {

            var logger = NLog.LogManager.GetLogger("UpperLevel");
            //var logger = NLog.LogManager.LoadConfiguration("nlog.config").Setup().GetCurrentClassLogger();
            logger.Info("init main");

            try
            {
                var builder = WebApplication.CreateBuilder(args);


                builder.Logging.ClearProviders();
                builder.Logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);                
                builder.Host.UseNLog();



                builder.Services.AddControllers();
                // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
                builder.Services.AddEndpointsApiExplorer();
                builder.Services.AddSwaggerGen();

                builder.Services.AddCors(o => o.AddPolicy("CustomPolicy", builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader();
                }));


                var app = builder.Build();

                // Configure the HTTP request pipeline.
                if (app.Environment.IsDevelopment())
                {
                    app.UseSwagger();
                    app.UseSwaggerUI();
                }

                app.UseHttpsRedirection();

                app.UseAuthorization();

                app.UseCors("CustomPolicy");

                app.MapControllers();

                app.Run();
            }
            catch (Exception exception)
            {
                // NLog: catch setup errors
                logger.Error(exception, "Stopped program because of exception");
                throw;
            }
            finally
            {
                // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
                NLog.LogManager.Shutdown();
            }

        }
    }
}