using System.Reflection;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Polichat_Backend.Database;
using Polichat_Backend.LIB;

namespace Polichat_Backend;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        
        builder.Services.AddControllers();
        builder.Services.AddWebSocketManager();
        AddWebSockets(builder.Services);
        
        builder.Services.AddDbContext<Context>(
            optionsBuilder => optionsBuilder.UseSqlServer(
                "Server=localhost;Port=3306;Database=polichat;Uid=root;"
                )
            );

        var app = builder.Build();
        
        if (builder.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        
        app.MapControllers();
        
        app.Map(
            "/ws", 
            x => x.UseMiddleware<SocketMiddleware>(app.Services.GetService<SocketMessageHandler>())
            );

        app.Run();
    }

    public static void AddWebSockets(IServiceCollection services)
    {
        services.AddTransient<ConnectionService>();
        
        foreach (var type in Assembly.GetEntryAssembly()!.ExportedTypes)
            if (type.GetTypeInfo().BaseType == typeof(SocketHandler))
                services.AddSingleton(type);
    }
}