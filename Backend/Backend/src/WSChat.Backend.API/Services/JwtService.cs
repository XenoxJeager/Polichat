using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace Polichat_Backend.Services;

public class JwtService
{
    private readonly byte[] _secret;

    public JwtService(byte[] secret)
    {
        _secret = secret;
    }
    
    public SecurityToken TryLogin(string username, string password)
    {
        if (!CheckCredentials(username, password))
            return null;

        var handler = new JwtSecurityTokenHandler();
        var descriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new []
            {
                new Claim(ClaimTypes.Name, "admin"),
                new Claim(ClaimTypes.Role, "admin")
            }),
            Expires = DateTime.UtcNow.AddMinutes(5),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(_secret),
                SecurityAlgorithms.HmacSha256Signature)
        };
        var token = handler.CreateToken(descriptor);
        Console.WriteLine(token.ToString());
        
        return token;
    }

    private bool CheckCredentials(string username, string password)
    {
        return username == "admin" && password == "admin";
    }
}
