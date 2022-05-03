using System;
using System.Reflection;
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
using Polichat_Backend.LIB;

namespace Polichat_Backend;

public static class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        
        builder.Services.AddSingleton<UserSocketService>();
        builder.Services.AddSingleton<WebSocketController>();
        builder.Services.AddSingleton<JwtService>();
        builder.Services.AddSingleton<StatisticsService>();
        
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
                    ValidateIssuer = true,
                    ValidIssuer = "localhost:3001",
                    
                    ValidateAudience = true,
                    ValidAudience = "localhost:3001",
                    
                    ValidateLifetime = true,

                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("hello"))
                };
            });

        const string connectionString = "Server=127.0.0.1;Database=polichat;Uid=root;";
        builder.Services.AddDbContext<Context>(
            optionsBuilder => optionsBuilder.UseMySql(
                    connectionString, ServerVersion.AutoDetect(connectionString)
                )
            );

        var app = builder.Build();
        
        if (builder.Environment.IsDevelopment())
            app.UseDeveloperExceptionPage();
        
        app.MapControllers();
        app.UseCors();
        app.UseAuthentication();
        app.UseWebSockets();
        app.Run();
    }
}