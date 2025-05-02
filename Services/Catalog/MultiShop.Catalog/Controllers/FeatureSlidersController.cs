using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Catalog.Dtos.FeatureSliderDtos;
using MultiShop.Catalog.Services.FeatureSliderServices;

namespace MultiShop.Catalog.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class FeatureSlidersController : ControllerBase
{
    private readonly IFeatureSliderService _featureSliderService;

    public FeatureSlidersController(IFeatureSliderService featureSliderService)
    {
        _featureSliderService = featureSliderService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllFeatureSliderList()
    {
        try
        {
            var featureSliders = await _featureSliderService.GetAllFeatureSliderAsync();
            return Ok(featureSliders);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while fetching feature sliders: {ex.Message}");
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetFeatureSliderById(string id)
    {
        try
        {
            var featureSlider = await _featureSliderService.GetByIdFeatureSliderAsync(id);
            if (featureSlider == null)
                return NotFound($"Feature slider with ID '{id}' was not found.");

            return Ok(featureSlider);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while fetching feature slider: {ex.Message}");
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateFeatureSlider(CreateFeatureSliderDto createFeatureSliderDto)
    {
        try
        {
            await _featureSliderService.CreateFeatureSliderAsync(createFeatureSliderDto);
            return Ok("Feature slider successfully added.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while creating feature slider: {ex.Message}");
        }
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteFeatureSlider(string id)
    {
        try
        {
            var existing = await _featureSliderService.GetByIdFeatureSliderAsync(id);
            if (existing == null)
                return NotFound($"Feature slider with ID '{id}' was not found.");

            await _featureSliderService.DeleteFeatureSliderAsync(id);
            return Ok("Feature slider successfully deleted.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while deleting feature slider: {ex.Message}");
        }
    }

    [HttpPut]
    public async Task<IActionResult> UpdateFeatureSlider(UpdateFeatureSliderDto updateFeatureSliderDto)
    {
        try
        {
            var existing = await _featureSliderService.GetByIdFeatureSliderAsync(updateFeatureSliderDto.FeatureSliderId);
            if (existing == null)
                return NotFound($"Feature slider with ID '{updateFeatureSliderDto.FeatureSliderId}' was not found.");

            await _featureSliderService.UpdateFeatureSliderAsync(updateFeatureSliderDto);
            return Ok("Feature slider successfully updated.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while updating feature slider: {ex.Message}");
        }
    }
}
