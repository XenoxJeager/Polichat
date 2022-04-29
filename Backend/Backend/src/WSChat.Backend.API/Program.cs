using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Polichat_Backend.Database;
using Polichat_Backend.LIB;

namespace Polichat_Backend;

public static class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddCors(options => options.AddDefaultPolicy(policyBuilder => policyBuilder.AllowAnyOrigin()));
        builder.Services.AddControllers();
        builder.Services.AddWebSocketManager();
        AddWebSockets(builder.Services);

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
        app.UseWebSockets();
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