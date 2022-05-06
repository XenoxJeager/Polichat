using System;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Polichat_Backend.Controllers;
using Polichat_Backend.Database;
using Polichat_Backend.Services;

namespace Polichat_Backend;

public class SingletonService : Attribute {}

public static class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        foreach (var obj in Assembly.GetExecutingAssembly().GetTypes()
                     .Where(predicate => predicate.IsDefined(typeof(SingletonService))))
            builder.Services.AddSingleton(obj);

        var secret = RandomNumberGenerator.GetBytes(2048);
        builder.Services.AddTransient(_ => new JwtService(secret));
        
        builder.Services.AddCors(options => options.AddDefaultPolicy(policyBuilder => policyBuilder.AllowAnyOrigin()));
        builder.Services.AddControllers();
        builder.Services
            .AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    
                    ValidateLifetime = true,

                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(secret)
                };
            });

        const string connectionString = "Server=127.0.0.1;Database=polichat;Uid=root;";
        builder.Services.AddDbContext<Context>(
            optionsBuilder => optionsBuilder.UseMySql(
                    connectionString, ServerVersion.AutoDetect(connectionString)
                )
            );

        var app = builder.Build();

        app.Use(async (context, next) =>
        {
            app.Services.GetService<AnalyticsService>()!.ApiAnalytics.TotalApiCalls += 1;
            await next();
        });    

        
        if (builder.Environment.IsDevelopment())
            app.UseDeveloperExceptionPage();
        
        app.MapControllers();
        app.UseCors();
        app.UseWebSockets();
        
        app.UseAuthentication();
        app.UseRouting();
        app.UseAuthorization();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
        
        app.Run();
    }
}