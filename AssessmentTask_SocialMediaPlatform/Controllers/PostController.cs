using Social_Media.Models;
using Social_Media.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Social_Media.Controllers;

[ApiController] // indicats it's a controller
[Route("[controller]")] // sets the base route for the PostController
// the [controller] will be reaplaced by controller class name that is post here 
public class PostController : ControllerBase
{
    private readonly PostService _postService;

    public PostController(PostService postService)
    {
        _postService = postService;
    }

    // get all the posts
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Post>>> GetPosts()
    {
        var posts = await _postService.GetPostsAsync();
        return Ok(posts);
    }

    // get post by ID
    [HttpGet("{id}")]
    public async Task<ActionResult<Post>> GetPost(int id)
    {
        var post = await _postService.GetPostByIdAsync(id);

        if (post == null)
        {
            return NotFound();
        }

        return Ok(post);
    }

    // create a post
    [HttpPost]
    public async Task<ActionResult<Post>> CreatePost(Post post)
    {
        try
        {
            await _postService.CreatePostAsync(post);
            return CreatedAtAction(nameof(GetPost), new { id = post.PostID }, post);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    // update a post by id
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePost(int id, Post post)
    {
        if (id != post.PostID)
        {
            return BadRequest();
        }

        var result = await _postService.UpdatePostAsync(post);

        if (result == null)
        {
            return NotFound();
        }

        return NoContent();
    }

    // delete a post by id
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePost(int id)
    {
        var result = await _postService.DeletePostAsync(id);

        if (result == null)
        {
            return NotFound();
        }

        return NoContent();
    }
}