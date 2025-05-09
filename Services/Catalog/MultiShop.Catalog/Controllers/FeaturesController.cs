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
        var features = await _featureService.GetAllFeatureAsync();
        return Ok(features);  
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetFeatureById(string id)
    {
        var feature = await _featureService.GetByIdFeatureAsync(id);
        return feature is null
            ? NotFound($"Highlighted feature with ID '{id}' was not found.")  
            : Ok(feature);
    }

    [HttpPost]
    public async Task<IActionResult> CreateFeature(CreateFeatureDto createFeatureDto)
    {
        await _featureService.CreateFeatureAsync(createFeatureDto);
        return Ok("Highlighted feature was successfully added.");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteFeature(string id)
    {
        var existing = await _featureService.GetByIdFeatureAsync(id);
        if (existing is null)
            return NotFound($"Highlighted feature with ID '{id}' was not found.");

        await _featureService.DeleteFeatureAsync(id);
        return Ok("Highlighted feature was successfully deleted.");
    }

    [HttpPut]
    public async Task<IActionResult> UpdateFeature(UpdateFeatureDto updateFeatureDto)
    {
        var existing = await _featureService.GetByIdFeatureAsync(updateFeatureDto.FeatureId);
        if (existing is null)
            return NotFound($"Highlighted feature with ID '{updateFeatureDto.FeatureId}' was not found.");

        await _featureService.UpdateFeatureAsync(updateFeatureDto);
        return Ok("Highlighted feature was successfully updated.");
    }
}
