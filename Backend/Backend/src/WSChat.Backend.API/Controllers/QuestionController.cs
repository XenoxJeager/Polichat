using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Polichat_Backend.Database;

namespace Polichat_Backend.Controllers;

public class Question
{
    public string Text { get; set; }
    public Weight[] Weights { get; set; }
}

public class Weight
{
    public double X { get; set; }
    public double Y { get; set; }
}

[ApiController]
public class QuestionController : ControllerBase
{
    private readonly Context _context;
    private readonly Random _random = new ();
    
    public QuestionController(Context context)
    {
        _context = context;
    }

    [Route("/questions")]
    [HttpGet]
    public async Task<Question[]> GetQuestions()
    {
        var dbQuestions = await _context.Questions.ToArrayAsync();
        var shuffled = dbQuestions.OrderBy(_ => _random.Next());
        var questions = shuffled
            .Select(question => new Question
            {
                Text = question.Text,
                Weights = new[]
                {
                    new Weight { X = question.StronglyAgreeX, Y = question.StronglyAgreeY },
                    new Weight { X = question.AgreeX, Y = question.AgreeY },
                    new Weight { X = question.DisagreeX, Y = question.DisagreeY },
                    new Weight { X = question.StronglyDisagreeX, Y = question.StronglyDisagreeY },
                }
            });
        
        Response.Headers.Add("Access-Control-Allow-Headers", "*");
        return questions.Take(20).ToArray();
    }
}
