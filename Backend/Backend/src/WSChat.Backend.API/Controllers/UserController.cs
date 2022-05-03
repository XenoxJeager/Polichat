using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Polichat_Backend.LIB;

namespace Polichat_Backend.Controllers;

[ApiController]
[Authorize]
public class UserController
{
    private StatisticsService _statistics;

    public UserController(StatisticsService statistics)
    {
        _statistics = statistics;
    }
    
    [HttpGet("/statistics")]
    public async Task GetStatistics()
    {
        
    }
}
