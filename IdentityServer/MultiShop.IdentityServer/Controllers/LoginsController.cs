using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MultiShop.IdentityServer.Dtos;
using MultiShop.IdentityServer.Models;
using MultiShop.IdentityServer.Tools;
using System.Threading.Tasks;

namespace MultiShop.IdentityServer.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LoginsController : ControllerBase
{
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly UserManager<ApplicationUser> _userManager;

    public LoginsController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
    {
        _signInManager = signInManager;
        _userManager = userManager;
    }

    [HttpPost]
    public async Task<IActionResult> UserLogin(UserLoginDto userLoginDto)
    {
        var signInResult = await _signInManager.PasswordSignInAsync(
            userLoginDto.UserName,
            userLoginDto.Password,
            isPersistent: false,
            lockoutOnFailure: false
        );

        if (signInResult.Succeeded)
        {
            var user = await _userManager.FindByNameAsync(userLoginDto.UserName);
            if (user == null)
            {
                return BadRequest("User not found.");
            }

            var userInfo = new GetCheckAppUserViewModel
            {
                UserName = user.UserName,
                Id = user.Id
            };

            var token = JwtTokenGenerator.GenerateToken(userInfo);
            return Ok(token);
        }

        return BadRequest("Invalid username or password.");
    }
}
