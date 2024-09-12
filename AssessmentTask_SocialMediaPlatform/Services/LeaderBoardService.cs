using Social_Media.Models;
using Microsoft.EntityFrameworkCore;

namespace Social_Media.Services;

public class LeaderBoardService
{
    private readonly ApplicationDbContext _context;

    public LeaderBoardService(ApplicationDbContext context)
    {
        _context = context;
    }

    // calculate leaderboard
    public async Task<IEnumerable<UserEngagement>> GetUserEngagementScoresAsync()
    {
        var engagementScores = await _context.Users
            .Select(user => new UserEngagement
            {
                UserID = user.UserID,
                UserName = user.UserName,
                NumberOfPosts = _context.Posts.Count(p => p.UserID == user.UserID),
                NumberOfLikes = _context.Likes.Count(l => l.UserID == user.UserID),
                NumberOfComments = _context.Comments.Count(c => c.UserID == user.UserID),
                EngagementScore = (_context.Posts.Count(p => p.UserID == user.UserID) * 5) +
                                (_context.Likes.Count(l => l.UserID == user.UserID) * 2) +
                                (_context.Comments.Count(c => c.UserID == user.UserID) * 3)
            })
            .OrderByDescending(ue => ue.EngagementScore) // Sort by score

            .ToListAsync();
        // Assign rank based on the sorted engagement scores
        int rank = 1;
        foreach (var engagementScore in engagementScores)
        {
            engagementScore.Rank = rank++;
        }

        return engagementScores;
    }
}
