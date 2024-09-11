using System.Text.RegularExpressions;
using Social_Media.Models;
using Microsoft.EntityFrameworkCore;

namespace Social_Media.Services;

public class PostService
{
    private readonly ApplicationDbContext _context;

    // list of banned words
    private readonly List<string> _bannedWords = new List<string>
        {
            "monolith", "spaghettiCode", "goto", "hack", "architrixs", "quickAndDirty", "cowboy", "yo", "globalVariable", "recursiveHell", "backdoor", "hotfix", "leakyAbstraction", "mockup", "singleton", "silverBullet", "technicalDebt"
        };

    public PostService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Post>> GetPostsAsync()
    {
        return await _context.Posts.ToListAsync();
    }

    public async Task<Post> GetPostByIdAsync(int id)
    {
        return await _context.Posts.FindAsync(id);
    }

    // check for banned words in the post 
    private bool ContainsBannedWords(string content)
    {
        foreach (var word in _bannedWords)
        {
            if (Regex.IsMatch(content, $@"\b{word}\b", RegexOptions.IgnoreCase))
            {
                return true;
            }
        }
        return false;
    }
    public async Task<Post> CreatePostAsync(Post post)
    {
        if (ContainsBannedWords(post.Content))
        {
            throw new Exception("Your post contains inappropriate content and cannot be posted.");
        }
        _context.Posts.Add(post);
        await _context.SaveChangesAsync();
        return post;
    }

    public async Task<Post> UpdatePostAsync(Post post)
    {
        _context.Entry(post).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!PostExists(post.PostID))
            {
                return null;
            }
            else
            {
                throw;
            }
        }

        return post;
    }

    public async Task<Post> DeletePostAsync(int id)
    {
        var post = await _context.Posts.FindAsync(id);
        if (post == null)
        {
            return null;
        }

        _context.Posts.Remove(post);
        await _context.SaveChangesAsync();

        return post;
    }

    private bool PostExists(int id)
    {
        return _context.Posts.Any(e => e.PostID == id);
    }

    public async Task<UserFeed> GetUserFeedAsync()
    {
        var posts = await _context.Posts.ToListAsync();
        var likes = await _context.Likes.ToListAsync();
        var comments = await _context.Comments.ToListAsync();

        return new UserFeed
        {
            Posts = posts,
            Likes = likes,
            Comments = comments
        };
    }
}