using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Polichat_Backend.LIB;
using Polichat_Backend.Resources;
using static System.Int32;

namespace Polichat_Backend.Controllers;

[ApiController]
public class WebSocketController : ControllerBase
{
    private readonly UserSocketService _service;

    public WebSocketController(UserSocketService service)
    {
        _service = service;

    }

    [HttpGet("/ws")]
    public async Task AcceptChatConnection([FromQuery(Name = "room_id")] int roomId)
    {
        if (HttpContext.WebSockets.IsWebSocketRequest)
        {
            using var webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();
            await _service.RegisterSocket((Room)roomId, webSocket);
        }
        else
            HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
    }
}
