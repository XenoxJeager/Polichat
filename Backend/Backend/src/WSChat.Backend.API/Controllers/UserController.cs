using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Polichat_Backend.Services;

namespace Polichat_Backend.Controllers;

[ApiController]
public class UserController : ControllerBase
{
    private readonly JwtService _jwtService;

    public UserController(JwtService jwtService)
    {
        _jwtService = jwtService;
    }
    
    [Route("/signIn")]
    public IActionResult SignIn(string username, string password)
    {
        var res = _jwtService.TryLogin(username, password);

        if (res == null)
            return Unauthorized();

        return Ok(res);
    }
}