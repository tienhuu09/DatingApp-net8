using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API;

public class UsersController(DataContext context) : BaseApiController
{
    [AllowAnonymous] // Không cần token vẫn có thể call
    [HttpGet] // người dùng gọi API
    public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
    {
        var users = await context.Users.ToListAsync();

        return users;
    }

    [Authorize] // Cần đưa vào cái token
    [HttpGet("{id:int}")]  // /api/users/2
    public async Task<ActionResult<AppUser>> GetUser(int id)
    {
        var user = await context.Users.FindAsync(id);

        if (user == null) return NotFound();

        return user;
    }
}
