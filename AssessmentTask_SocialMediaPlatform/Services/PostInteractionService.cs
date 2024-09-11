using Microsoft.EntityFrameworkCore;
using Social_Media.Models;

namespace Social_Media.Services
{
    public class PostInteractionService
    {
        private readonly ApplicationDbContext _context;

        public PostInteractionService(ApplicationDbContext context)
        {
            _context = context;
        }

        // Add comment to the database
        public async Task AddCommentAsync(Comment comment)
        {
            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();
        }

        // Add like to the database
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
}
