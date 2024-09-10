using Microsoft.AspNetCore.Mvc;
using Social_Media.Models;
using Social_Media.Services;

namespace Social_Media.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{

    private readonly UserService _userService;

    public UserController(UserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<User>>> GetUsers()
    {
        var users = await _userService.GetUsersAsync();
        return Ok(users);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<User>> GetUser(int id)
    {
        var user = await _userService.GetUserByIdAsync(id);

        if (user == null)
        {
            return NotFound();
        }

        return Ok(user);
    }

    [HttpPost]
    public async Task<ActionResult<User>> CreateUser(User user)
    {
        await _userService.CreateUserAsync(user);
        return CreatedAtAction(nameof(GetUser), new { id = user.UserID }, user);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser(int id, User user)
    {
        if (id != user.UserID)
        {
            return BadRequest();
        }

        var result = await _userService.UpdateUserAsync(user);

        if (result == null)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        var result = await _userService.DeleteUserAsync(id);

        if (result == null)
        {
            return NotFound();
        }

        return NoContent();
    }


    [HttpGet("calculate-leaderboard")]
    public async Task<ActionResult<IEnumerable<UserEngagement>>> GetUserEngagementScores()
    {
        var engagementScores = await _userService.GetUserEngagementScoresAsync();
        return Ok(engagementScores);
    }

}