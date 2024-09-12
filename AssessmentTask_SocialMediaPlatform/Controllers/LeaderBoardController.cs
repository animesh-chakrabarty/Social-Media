using Microsoft.AspNetCore.Mvc;
using Social_Media.Models;
using Social_Media.Services;

namespace Social_Media.Controllers;

[ApiController]
[Route("[controller]")]
public class LeaderBoardController : ControllerBase
{

    private readonly LeaderBoardService _leaderBoardService;

    public LeaderBoardController(LeaderBoardService leaderBoardService)
    {
        _leaderBoardService = leaderBoardService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserEngagement>>> GetUserEngagementScores()
    {
        var engagementScores = await _leaderBoardService.GetUserEngagementScoresAsync();
        return Ok(engagementScores);
    }
}


