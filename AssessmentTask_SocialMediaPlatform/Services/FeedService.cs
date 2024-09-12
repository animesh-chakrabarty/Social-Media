using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
using Social_Media.Models;

namespace Social_Media.Services;

public class FeedService
{
    private readonly ApplicationDbContext _context;

    public FeedService(ApplicationDbContext context)
    {
        _context = context;
    }

    // Interact with DB and fetch user feed
    public async Task<UserFeed> GetUserFeedAsync()
    {
        var posts = await _context.Posts.ToListAsync();
        var likes = await _context.Likes.ToListAsync();
        var comments = await _context.Comments.ToListAsync();

        return new UserFeed
        {
            Posts = posts,
            Likes = likes,
            Comments = comments,
        };
    }

    // Add comment to a post 
    public async Task AddCommentAsync(Comment comment)
    {
        _context.Comments.Add(comment);
        await _context.SaveChangesAsync();
    }

    // Add like to a post
    public async Task AddLikeAsync(Like like)
    {
        _context.Likes.Add(like);
        await _context.SaveChangesAsync();
    }

    // Check if the post exists
    public async Task<bool> PostExistsAsync(int postId)
    {
        return await _context.Posts.AnyAsync(e => e.PostID == postId);
    }

    // Check if the user already liked the post
    public async Task<bool> UserHasLikedAsync(int postId, int userId)
    {
        return await _context.Likes.AnyAsync(l => l.PostID == postId && l.UserID == userId);
    }
}
