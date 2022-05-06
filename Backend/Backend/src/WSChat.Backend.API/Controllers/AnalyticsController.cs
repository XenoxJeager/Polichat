using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Polichat_Backend.Services;

namespace Polichat_Backend.Controllers;

[ApiController]
[Authorize]
public class AnalyticsController : ControllerBase
{
    private readonly AnalyticsService _analyticsService;

    public AnalyticsController(AnalyticsService analyticsService)
    {
        _analyticsService = analyticsService;
    }
    
    [HttpGet("/analytics")]
    public IActionResult GetStatistics()
    {
        return Ok(_analyticsService);
    }
}
