using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MultiShop.IdentityServer.Dtos;
using MultiShop.IdentityServer.Models;
using System.Linq;
using System.Threading.Tasks;

namespace MultiShop.IdentityServer.Controllers;

[AllowAnonymous]
[Route("api/[controller]")]
[ApiController]
public class RegistersController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;

    public RegistersController(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    [HttpPost]
    public async Task<IActionResult> UserRegister(UserRegisterDto userRegisterDto)
    {
        var newUser = new ApplicationUser()
        {
            UserName = userRegisterDto.UserName,
            Name = userRegisterDto.Name,
            Surname = userRegisterDto.Surname,
            Email = userRegisterDto.Email
        };

        var createResult = await _userManager.CreateAsync(newUser, userRegisterDto.Password);

        if (createResult.Succeeded)
        {
            return Ok("User was successfully added.");
        }

        var errorMessages = createResult.Errors.Select(e => e.Description).ToList();
        return BadRequest(new { Message = "User registration failed.", Errors = errorMessages });
    }
}
