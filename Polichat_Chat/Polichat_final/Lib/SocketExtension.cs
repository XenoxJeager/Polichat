using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Polichat_final
{
    public static  class SocketExtension
    {
        public static IServiceCollection AddWebSocketManager(this IServiceCollection services)
        {
            services.AddTransient<ConnectionService>();
            foreach (var type in Assembly.GetEntryAssembly().ExportedTypes)
            {
                if (type.GetTypeInfo().BaseType == typeof(WebSocketHandler))
                {
                    services.AddSingleton(type);
                }
            }
            return services;
        }
        public static IApplicationBuilder MapSockets(this IApplicationBuilder app, PathString path, WebSocketHandler socket)
        {
            return app.Map(path, (x) => x.UseMiddleware<WebSocketMiddleware>(socket));
        } 
    }
}