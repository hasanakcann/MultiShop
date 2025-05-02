using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Catalog.Dtos.FeatureDtos;
using MultiShop.Catalog.Services.FeatureServices;

namespace MultiShop.Catalog.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class FeaturesController : ControllerBase
{
    private readonly IFeatureService _featureService;

    public FeaturesController(IFeatureService featureService)
    {
        _featureService = featureService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllFeatureList()
    {
        try
        {
            var features = await _featureService.GetAllFeatureAsync();
            return Ok(features);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while fetching feature list: {ex.Message}");
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetFeatureById(string id)
    {
        try
        {
            var feature = await _featureService.GetByIdFeatureAsync(id);
            if (feature == null)
                return NotFound($"Highlighted feature with ID '{id}' was not found.");

            return Ok(feature);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while fetching feature: {ex.Message}");
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateFeature(CreateFeatureDto createFeatureDto)
    {
        try
        {
            await _featureService.CreateFeatureAsync(createFeatureDto);
            return Ok("Highlighted feature was successfully added.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while creating feature: {ex.Message}");
        }
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteFeature(string id)
    {
        try
        {
            var existing = await _featureService.GetByIdFeatureAsync(id);
            if (existing == null)
                return NotFound($"Highlighted feature with ID '{id}' was not found.");

            await _featureService.DeleteFeatureAsync(id);
            return Ok("Highlighted feature was successfully deleted.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while deleting feature: {ex.Message}");
        }
    }

    [HttpPut]
    public async Task<IActionResult> UpdateFeature(UpdateFeatureDto updateFeatureDto)
    {
        try
        {
            var existing = await _featureService.GetByIdFeatureAsync(updateFeatureDto.FeatureId);
            if (existing == null)
                return NotFound($"Highlighted feature with ID '{updateFeatureDto.FeatureId}' was not found.");

            await _featureService.UpdateFeatureAsync(updateFeatureDto);
            return Ok("Highlighted feature was successfully updated.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while updating feature: {ex.Message}");
        }
    }
}
