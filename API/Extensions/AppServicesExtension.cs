using Core.Interfaces;
using Infrastructure.Data.Context;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using API.Errors;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions
{
    public static class AppServicesExtension
    {
        public static IServiceCollection AddAppServices(this IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = actionContext =>
                {
                    var errors = actionContext.ModelState.Where(e => e.Value.Errors.Count > 0)
                                    .SelectMany(x => x.Value.Errors).Select(x => x.ErrorMessage).ToArray();

                    var errorResponse = new ApiValidationErrorResponse { Errors = errors };

                    return new BadRequestObjectResult(errorResponse);
                };
            });

            //Repos
            services.AddScoped<IProductRepository, ProductRepository>();

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            return services;
        }

        public async static Task<IApplicationBuilder> UseDBMigrationsAndSeedData(this IApplicationBuilder app)
        {

            using (var scope = app.ApplicationServices.CreateScope())
            {
                var services = scope.ServiceProvider;
                var loggerFactory = services.GetRequiredService<ILoggerFactory>();
                try
                {
                    var context = services.GetRequiredService<ApplicationContext>();
                    await context.Database.MigrateAsync();
                    await ApplicationContextSeed.SeedAsync(context, loggerFactory);
                }
                catch (Exception ex)
                {
                    var logger = loggerFactory.CreateLogger<Program>();
                    logger.LogError(ex, "Error caught during migration");
                }
            }

            return app;
        }
    }
}
