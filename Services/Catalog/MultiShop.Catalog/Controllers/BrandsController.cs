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
        try
        {
            var brandList = await _brandService.GetAllBrandAsync();
            return Ok(brandList);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while fetching brand list: {ex.Message}");
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetBrandById(string id)
    {
        try
        {
            var brand = await _brandService.GetByIdBrandAsync(id);
            if (brand == null)
                return NotFound($"Brand with ID '{id}' was not found.");

            return Ok(brand);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while fetching brand: {ex.Message}");
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateBrand(CreateBrandDto createBrandDto)
    {
        try
        {
            await _brandService.CreateBrandAsync(createBrandDto);
            return Ok("Brand was successfully added.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while creating brand: {ex.Message}");
        }
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteBrand(string id)
    {
        try
        {
            var existing = await _brandService.GetByIdBrandAsync(id);
            if (existing == null)
                return NotFound($"Brand with ID '{id}' was not found.");

            await _brandService.DeleteBrandAsync(id);
            return Ok("Brand was successfully deleted.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while deleting brand: {ex.Message}");
        }
    }

    [HttpPut]
    public async Task<IActionResult> UpdateBrand(UpdateBrandDto updateBrandDto)
    {
        try
        {
            var existing = await _brandService.GetByIdBrandAsync(updateBrandDto.BrandId);
            if (existing == null)
                return NotFound($"Brand with ID '{updateBrandDto.BrandId}' was not found.");

            await _brandService.UpdateBrandAsync(updateBrandDto);
            return Ok("Brand was successfully updated.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while updating brand: {ex.Message}");
        }
    }
}
