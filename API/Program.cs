using API.Errors;
using API.Extensions;
using API.Helpers;
using API.Middleware;
using AutoMapper;
using Core.Interfaces;
using Infrastructure.Data;
using Infrastructure.Data.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

internal class Program
{
    private static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();

        builder.Services.AddEndpointsApiExplorer();

        builder.Services.AddAutoMapper(typeof(MappingProfiles));

        builder.Services.AddAppServices();

        builder.Services.AddSwagerServices();

        builder.Services.AddDbContext<ApplicationContext>(_ =>
            _.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

        var app = builder.Build();

        await app.UseDBMigrationsAndSeedData();

        app.UseMiddleware<ExceptionMiddleware>();

        app.UseStatusCodePagesWithReExecute("/errors/{0}");

        app.UseHttpsRedirection();

        app.UseStaticFiles();

        app.UseAuthorization();

        app.UseSwaggerDocumentation();

        app.MapControllers();

        app.Run();
    }
}