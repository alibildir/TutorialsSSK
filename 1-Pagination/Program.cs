using _1_Pagination.ActionFilters;
using _1_Pagination.AutoMappers;
using _1_Pagination.Contexts;
using _1_Pagination.Loggers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using NLog;

namespace _1_Pagination
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));
            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //Logger Service
            builder.Services.AddSingleton<ILoggerService, LoggerService>();

            var conn = builder.Configuration.GetConnectionString("DefaultConn");
            builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(conn));
            builder.Services.AddAutoMapper(typeof(Mapping));

            builder.Services.AddScoped<ValidationFilterAttribute>();

            //H�zS�n�rland�mas�
            builder.Services.Configure<IpRateLimitOptions>

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
    }
}