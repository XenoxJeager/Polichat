using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Polichat_Backend.Database;

namespace Polichat_Backend.Endpoints;

[ApiController]
public class Questions
{
    private Context _context;
    
    public Questions(Context context)
    {
        _context = context;
    }

    [Route("/questions")]
    [HttpGet]
    public async Task<Question[]> GetQuestions()
    {
        return _context.Questions.Take(20).ToArray();
    }
}
