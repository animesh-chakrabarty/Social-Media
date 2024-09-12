using Social_Media.Models;
using Microsoft.EntityFrameworkCore;
// using System.Linq;
// using System.Threading.Tasks;

namespace Social_Media.Services;

public class UserService
{
    private readonly ApplicationDbContext _context;

    public UserService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<User>> GetUsersAsync()
    {
        return await _context.Users.ToListAsync();
    }

    public async Task<User> GetUserByIdAsync(int id)
    {
        return await _context.Users.FindAsync(id);
    }

    public async Task<User> CreateUserAsync(User user)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task<User> UpdateUserAsync(User user)
    {
        _context.Entry(user).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!UserExists(user.UserID))
            {
                return null;
            }
            else
            {
                throw;
            }
        }

        return user;
    }

    public async Task<User> DeleteUserAsync(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null)
        {
            return null;
        }

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();

        return user;
    }

    private bool UserExists(int id)
    {
        return _context.Users.Any(e => e.UserID == id);
    }

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