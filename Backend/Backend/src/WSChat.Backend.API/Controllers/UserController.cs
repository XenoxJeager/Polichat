using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Polichat_Backend.Services;

namespace Polichat_Backend.Controllers;

public class UserLoginData
{
    public string Username { get; set; }
    public string Password { get; set; }
}

[ApiController]
public class UserController : ControllerBase
{
    private readonly JwtService _jwtService;

    public UserController(JwtService jwtService)
    {
        _jwtService = jwtService;
    }

    [HttpPost("/signIn")]
    public IActionResult SignIn([FromBody] UserLoginData loginData)
    {
        var res = _jwtService.TryLogin(loginData.Username, loginData.Password);

        if (res == null)
            return Unauthorized();
        return Ok(res);
    }
}