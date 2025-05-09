using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Catalog.Dtos.ProductImageDtos;
using MultiShop.Catalog.Services.ProductImageServices;

namespace MultiShop.Catalog.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class ProductImagesController : ControllerBase
{
    private readonly IProductImageService _productImageService;

    public ProductImagesController(IProductImageService productImageService)
    {
        _productImageService = productImageService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllProductImageList()
    {
        var images = await _productImageService.GetAllProductImageAsync();
        return Ok(images);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProductImageById(string id)
    {
        var image = await _productImageService.GetByIdProductImageAsync(id);
        return image is null
            ? NotFound("Product image not found.")
            : Ok(image);
    }

    [HttpPost]
    public async Task<IActionResult> CreateProductImage(CreateProductImageDto createProductImageDto)
    {
        await _productImageService.CreateProductImageAsync(createProductImageDto);
        return Ok("Product image successfully added.");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProductImage(string id)
    {
        var existing = await _productImageService.GetByIdProductImageAsync(id);
        if (existing is null)
            return NotFound("Product image not found.");

        await _productImageService.DeleteProductImageAsync(id);
        return Ok("Product image successfully deleted.");
    }

    [HttpPut]
    public async Task<IActionResult> UpdateProductImage(UpdateProductImageDto updateProductImageDto)
    {
        var existing = await _productImageService.GetByIdProductImageAsync(updateProductImageDto.ProductImageId);
        if (existing is null)
            return NotFound($"Product image with ID '{updateProductImageDto.ProductImageId}' was not found.");

        await _productImageService.UpdateProductImageAsync(updateProductImageDto);
        return Ok("Product image successfully updated.");
    }

    [HttpGet("ProductImagesByProductId/{id}")]
    public async Task<IActionResult> ProductImagesByProductId(string id)
    {
        var images = await _productImageService.GetByProductIdProductImageAsync(id);
        return Ok(images);
    }
}
