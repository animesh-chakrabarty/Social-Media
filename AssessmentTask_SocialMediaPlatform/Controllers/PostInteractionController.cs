using Microsoft.AspNetCore.Mvc;
using Social_Media.Models;
using Social_Media.Services;
using System.Text.RegularExpressions;

namespace Social_Media.Controllers
{
    [ApiController]
    [Route("/feed/[controller]")]
    public class PostInteractionController : ControllerBase
    {
        private readonly PostInteractionService _postInteractionService;

        // List of banned words
        private readonly List<string> _bannedWords = new List<string>
        {
            "monolith", "spaghettiCode", "goto", "hack", "architrixs", "quickAndDirty", "cowboy", "yo",
            "globalVariable", "recursiveHell", "backdoor", "hotfix", "leakyAbstraction", "mockup",
            "singleton", "silverBullet", "technicalDebt"
        };

        public PostInteractionController(PostInteractionService postInteractionService)
        {
            _postInteractionService = postInteractionService;
        }

        // Add a comment to a post
        [HttpPost("{postId}/comment")]
        public async Task<IActionResult> AddComment(int postId, [FromBody] Comment comment)
        {
            // Validation: Check if post exists
            if (!await _postInteractionService.PostExistsAsync(postId))
                return NotFound(new { message = "Post not found" });

            // Validation: Check for banned words
            if (ContainsBannedWords(comment.Content))
                return BadRequest(new { message = "Comment contains inappropriate language" });

            // Set the PostID for the comment
            comment.PostID = postId;

            // Add comment to the database
            await _postInteractionService.AddCommentAsync(comment);

            return Ok(new { message = "Comment added successfully" });
        }

        // Add a like to a post
        [HttpPost("{postId}/like")]
        public async Task<IActionResult> AddLike(int postId, [FromBody] Like like)
        {
            // Validation: Check if post exists
            if (!await _postInteractionService.PostExistsAsync(postId))
                return NotFound(new { message = "Post not found" });

            // Validation: Check if user has already liked the post
            if (await _postInteractionService.UserHasLikedAsync(postId, like.UserID))
                return BadRequest(new { message = "User has already liked this post" });

            // Ensure the PostID in the Like object matches the route parameter
            like.PostID = postId;

            // Add like to the database
            await _postInteractionService.AddLikeAsync(like);

            return Ok(new { message = "Like added successfully" });
        }



        // Check for banned words in the content
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
    }
}
