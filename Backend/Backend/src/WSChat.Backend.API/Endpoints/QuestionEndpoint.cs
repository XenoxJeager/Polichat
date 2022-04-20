using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Polichat_Backend.Database;

namespace Polichat_Backend.Endpoints;

[ApiController]
public class QuestionEndpoint
{
    private Context _context;
    
    public QuestionEndpoint(Context context)
    {
        _context = context;
    }

    [Route("/questions")]
    [HttpGet]
    public async Task<Question[]> GetQuestions()
    {
        var questions = _context.Questions.OrderBy(question => question.Text);
        var res = questions.Take(20);
        return res.ToArray();
    }
}
