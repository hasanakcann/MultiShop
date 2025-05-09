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
        var featureSliders = await _featureSliderService.GetAllFeatureSliderAsync();
        return Ok(featureSliders);  
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetFeatureSliderById(string id)
    {
        var featureSlider = await _featureSliderService.GetByIdFeatureSliderAsync(id);
        return featureSlider is null
            ? NotFound($"Feature slider with ID '{id}' was not found.")  
            : Ok(featureSlider);
    }

    [HttpPost]
    public async Task<IActionResult> CreateFeatureSlider(CreateFeatureSliderDto createFeatureSliderDto)
    {
        await _featureSliderService.CreateFeatureSliderAsync(createFeatureSliderDto);
        return Ok("Feature slider successfully added.");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteFeatureSlider(string id)
    {
        var existing = await _featureSliderService.GetByIdFeatureSliderAsync(id);
        if (existing is null)
            return NotFound($"Feature slider with ID '{id}' was not found.");

        await _featureSliderService.DeleteFeatureSliderAsync(id);
        return Ok("Feature slider successfully deleted.");
    }

    [HttpPut]
    public async Task<IActionResult> UpdateFeatureSlider(UpdateFeatureSliderDto updateFeatureSliderDto)
    {
        var existing = await _featureSliderService.GetByIdFeatureSliderAsync(updateFeatureSliderDto.FeatureSliderId);
        if (existing is null)
            return NotFound($"Feature slider with ID '{updateFeatureSliderDto.FeatureSliderId}' was not found.");

        await _featureSliderService.UpdateFeatureSliderAsync(updateFeatureSliderDto);
        return Ok("Feature slider successfully updated.");
    }
}
