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
    public PostService(ApplicationDbContext context)
    {
        _context = context;
    }

    // get all the posts
    public async Task<IEnumerable<Post>> GetPostsAsync()
    {
        return await _context.Posts.ToListAsync();
    }

    // get posts by id
    public async Task<Post> GetPostByIdAsync(int id)
    {
        return await _context.Posts.FindAsync(id);
    }

    // create a new post
    public async Task<Post> CreatePostAsync(Post post)
    {
        // check if content contains banned words
        if (ContainsBannedWords(post.Content))
        {
            throw new Exception("Your post contains inappropriate content and cannot be posted.");
        }
        _context.Posts.Add(post);
        await _context.SaveChangesAsync();
        return post;
    }

    // update a post
    public async Task<Post> UpdatePostAsync(Post post)
    {
        _context.Entry(post).State = EntityState.Modified;

        // check if updated content contains banned words
        if (ContainsBannedWords(post.Content))
        {
            throw new Exception("Your post contains inappropriate content and cannot be posted.");
        }

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

    // delete post
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

    // check if post exists or not
    private bool PostExists(int id)
    {
        return _context.Posts.Any(e => e.PostID == id);
    }

}