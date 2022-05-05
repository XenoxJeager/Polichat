using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Polichat_Backend.Services;

namespace Polichat_Backend.Controllers;

[ApiController]
[Authorize]
public class StatisticsController
{
    [HttpGet("/statistics")]
    public async Task<string> GetStatistics()
    {
        StatisticsService.ApiCalls += 1;
        return "hello xenia";
    }
}
