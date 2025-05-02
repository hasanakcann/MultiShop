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
        return Ok(brand);
    }

    [HttpPost]
    public async Task<IActionResult> CreateBrand(CreateBrandDto createBrandDto)
    {
        await _brandService.CreateBrandAsync(createBrandDto);
        return Ok("Brand was successfully added.");
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteBrand(string id)
    {
        await _brandService.DeleteBrandAsync(id);
        return Ok("Brand was successfully deleted.");
    }

    [HttpPut]
    public async Task<IActionResult> UpdateBrand(UpdateBrandDto updateBrandDto)
    {
        await _brandService.UpdateBrandAsync(updateBrandDto);
        return Ok("Brand was successfully updated.");
    }
}
