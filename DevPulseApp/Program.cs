using Application.DependencyInjection;
using DevPulseApp.ExtensionMethods;
using DevPulseApp.Middlewares;
using Infrastructure.DependencyInjection;
using Serilog;

namespace DevPulseApp
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            //var key = Convert.ToBase64String(
            //    RandomNumberGenerator.GetBytes(64)
            //);
            //Console.WriteLine(key);

            var builder = WebApplication.CreateBuilder(args);

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(builder.Configuration)
                .CreateLogger();

            builder.Host.UseSerilog();

            builder.Services.AddInfrastructure(builder.Configuration);

            // Add services to the container.
            builder.Services.AddControllers();

            builder.Services.AddApplication();

            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                {
                    builder
                        .WithOrigins("http://localhost:4200")
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials()
                        .SetIsOriginAllowed((host) => true);
                });
            });

            var app = builder.Build();

            app.UseMiddleware<GlobalExceptionMiddleware>();

            app.UseStaticFiles();

            app.UseCors("CorsPolicy");

            await app.SeedDataAsync();
            await app.ApplyPendingMigrationsAsync();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}