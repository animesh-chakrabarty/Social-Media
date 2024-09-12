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

    // method 1 that handles get request on
    // /post/
    // get all the posts
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Post>>> GetPosts()
    {
        var posts = await _postService.GetPostsAsync();
        return Ok(posts);
    }

    // method 2 that also handles get request on /post/id
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

    // method 3 that handles POST request on /post/
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

    // method 4 that handles PUT request on /post/id
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

    // method 5 that handles DEL request on /post/id
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