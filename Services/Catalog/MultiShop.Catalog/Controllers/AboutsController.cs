using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Catalog.Dtos.AboutDtos;
using MultiShop.Catalog.Services.AboutServices;

namespace MultiShop.Catalog.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class AboutsController : ControllerBase
{
    private readonly IAboutService _aboutService;

    public AboutsController(IAboutService aboutService)
    {
        _aboutService = aboutService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAboutList()
    {
        try
        {
            var aboutList = await _aboutService.GetAllAboutAsync();
            return Ok(aboutList);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while fetching about list: {ex.Message}");
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAboutById(string id)
    {
        try
        {
            var about = await _aboutService.GetByIdAboutAsync(id);
            if (about == null)
                return NotFound($"About section with ID '{id}' was not found.");

            return Ok(about);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while fetching about section: {ex.Message}");
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateAbout(CreateAboutDto createAboutDto)
    {
        try
        {
            await _aboutService.CreateAboutAsync(createAboutDto);
            return Ok("About section was successfully added.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while creating about section: {ex.Message}");
        }
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteAbout(string id)
    {
        try
        {
            var existing = await _aboutService.GetByIdAboutAsync(id);
            if (existing == null)
                return NotFound($"About section with ID '{id}' was not found.");

            await _aboutService.DeleteAboutAsync(id);
            return Ok("About section was successfully deleted.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while deleting about section: {ex.Message}");
        }
    }

    [HttpPut]
    public async Task<IActionResult> UpdateAbout(UpdateAboutDto updateAboutDto)
    {
        try
        {
            var existing = await _aboutService.GetByIdAboutAsync(updateAboutDto.AboutId);
            if (existing == null)
                return NotFound($"About section with ID '{updateAboutDto.AboutId}' was not found.");

            await _aboutService.UpdateAboutAsync(updateAboutDto);
            return Ok("About section was successfully updated.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while updating about section: {ex.Message}");
        }
    }
}
