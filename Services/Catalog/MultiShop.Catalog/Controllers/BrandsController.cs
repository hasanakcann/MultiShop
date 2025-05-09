using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Catalog.Dtos.BrandDtos;
using MultiShop.Catalog.Services.BrandServices;

namespace MultiShop.Catalog.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class BrandsController : ControllerBase
{
    private readonly IBrandService _brandService;

    public BrandsController(IBrandService brandService)
    {
        _brandService = brandService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllBrandList()
    {
        var brandList = await _brandService.GetAllBrandAsync();
        return Ok(brandList); 
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetBrandById(string id)
    {
        var brand = await _brandService.GetByIdBrandAsync(id);
        return brand is null
            ? NotFound($"Brand with ID '{id}' was not found.") 
            : Ok(brand);
    }

    [HttpPost]
    public async Task<IActionResult> CreateBrand(CreateBrandDto createBrandDto)
    {
        await _brandService.CreateBrandAsync(createBrandDto);
        return Ok("Brand was successfully added.");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBrand(string id)
    {
        var existing = await _brandService.GetByIdBrandAsync(id);
        if (existing is null)
            return NotFound($"Brand with ID '{id}' was not found.");

        await _brandService.DeleteBrandAsync(id);
        return Ok("Brand was successfully deleted.");
    }

    [HttpPut]
    public async Task<IActionResult> UpdateBrand(UpdateBrandDto updateBrandDto)
    {
        var existing = await _brandService.GetByIdBrandAsync(updateBrandDto.BrandId);
        if (existing is null)
            return NotFound($"Brand with ID '{updateBrandDto.BrandId}' was not found.");

        await _brandService.UpdateBrandAsync(updateBrandDto);
        return Ok("Brand was successfully updated.");
    }
}
