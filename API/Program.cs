using Core.Interfaces;
using Infrastructure.Data;
using Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

internal class Program
{
    private static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddDbContext<ApplicationContext>(_ =>
            _.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

        //Repos
        builder.Services.AddScoped<IProductRepository, ProductRepository>();

        var app = builder.Build();

        //For applying migrations and creating db
        using(var scope = app.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            var loggerFactory = services.GetRequiredService<ILoggerFactory>();
            try
            {
                var context = services.GetRequiredService<ApplicationContext>();
                await context.Database.MigrateAsync();
                await ApplicationContextSeed.SeedAsync(context, loggerFactory);
            }
            catch(Exception ex)
            {
                var logger = loggerFactory.CreateLogger<Program>();
                logger.LogError(ex, "Error caught during migration");
            }
        }

        //HTTP request pipeline config
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