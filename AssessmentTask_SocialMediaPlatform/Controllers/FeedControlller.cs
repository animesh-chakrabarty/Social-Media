using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Social_Media.Models;
using Social_Media.Services;
using System.Text.RegularExpressions;

namespace Social_Media.Controllers;

// controller to fetch user feed
[ApiController]
[Route("[controller]")]
public class FeedController : ControllerBase
{
    private readonly FeedService _feedService;

    public FeedController(FeedService feedService)
    {
        _feedService = feedService;
    }

    // List of banned words
    private readonly List<string> _bannedWords = new List<string>
        {
            "monolith", "spaghettiCode", "goto", "hack", "architrixs", "quickAndDirty", "cowboy", "yo",
            "globalVariable", "recursiveHell", "backdoor", "hotfix", "leakyAbstraction", "mockup",
            "singleton", "silverBullet", "technicalDebt"
        };

    // Method to check for banned words
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

    // GET - /feed - to retrieve user feed
    [HttpGet]
    public async Task<ActionResult<UserFeed>> GetUserFeed()
    {
        var feed = await _feedService.GetUserFeedAsync();
        return Ok(feed);
    }

    // POST - /feed/{postId}/comment - to Add comment to a post
    [HttpPost("{postId}/comment")]
    public async Task<IActionResult> AddComment(int postId, [FromBody] Comment comment)
    {
        // Validation: Check if post exists
        if (!await _feedService.PostExistsAsync(postId))
            return NotFound(new { message = "Post not found" });

        // Validation: Check for banned words
        if (ContainsBannedWords(comment.Content))
            return BadRequest(new { message = "Comment contains inappropriate language" });

        // Set the PostID for the comment
        comment.PostID = postId;

        // Add comment to the database
        await _feedService.AddCommentAsync(comment);

        return Ok(new { message = "Comment added successfully" });
    }

    // POST - /feed/{postId}/like - to Add like to a post
    [HttpPost("{postId}/like")]
    public async Task<IActionResult> AddLike(int postId, [FromBody] Like like)
    {
        // Validation: Check if post exists
        if (!await _feedService.PostExistsAsync(postId))
            return NotFound(new { message = "Post not found" });

        // Validation: Check if user has already liked the post
        if (await _feedService.UserHasLikedAsync(postId, like.UserID))
            return BadRequest(new { message = "You have already liked this post" });

        // Ensure the PostID in the Like object matches the route parameter
        like.PostID = postId;

        // Add like to the database
        await _feedService.AddLikeAsync(like);

        return Ok(new { message = "Like added successfully" });
    }
}
