using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;

namespace Polichat_Backend.Services;

public class JwtService
{
    private readonly byte[] _secret;
    
    public JwtService(byte[] secret)
    {
        _secret = secret;
    }
    
    public string TryLogin(string username, string password)
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
        
        return handler.WriteToken(token);
    }
    
    private bool CheckCredentials(string username, string password)
    {
        // Environment.SetEnvironmentVariable("passwd","admin");
        var pass = Environment.GetEnvironmentVariable("passwd") ?? throw new ArgumentNullException("Die Variable passwd ist nicht gesetzt.");
        return username == "admin" && password == pass;
    }
}
