using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.JsonWebTokens;
using MultiShop.IdentityServer.Models;
using System.Linq;
using System.Threading.Tasks;
using static IdentityServer4.IdentityServerConstants;

namespace MultiShop.IdentityServer.Controllers;

[Authorize(LocalApi.PolicyName)]
[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;

    public UsersController(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    [HttpGet("GetUser")]
    public async Task<IActionResult> GetUser()
    {
        var userClaim = User.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Sub);

        if (userClaim == null)
        {
            return BadRequest(new { message = "User claim not found in the token." });
        }

        var user = await _userManager.FindByIdAsync(userClaim.Value);

        if (user == null)
        {
            return NotFound(new { message = "User not found." });
        }

        return Ok(new
        {
            user.Id,
            user.Name,
            user.Surname,
            user.Email,
            user.UserName
        });
    }

    [HttpGet("GetAllUserList")]
    public async Task<IActionResult> GetAllUserList()
    {
        var users = await _userManager.Users.ToListAsync();

        if (users == null || !users.Any())
        {
            return Ok(new { message = "No users found.", data = new object[] { } });
        }

        return Ok(new { data = users });
    }
}
