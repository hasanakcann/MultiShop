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
        try
        {
            var images = await _productImageService.GetAllProductImageAsync();
            return Ok(images);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while retrieving product images: {ex.Message}");
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProductImageById(string id)
    {
        try
        {
            var image = await _productImageService.GetByIdProductImageAsync(id);
            if (image == null)
                return NotFound("Product image not found.");

            return Ok(image);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while retrieving the product image: {ex.Message}");
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateProductImage(CreateProductImageDto createProductImageDto)
    {
        try
        {
            await _productImageService.CreateProductImageAsync(createProductImageDto);
            return Ok("Product image successfully added.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while adding the product image: {ex.Message}");
        }
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteProductImage(string id)
    {
        try
        {
            await _productImageService.DeleteProductImageAsync(id);
            return Ok("Product image successfully deleted.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while deleting the product image: {ex.Message}");
        }
    }

    [HttpPut]
    public async Task<IActionResult> UpdateProductImage(UpdateProductImageDto updateProductImageDto)
    {
        try
        {
            await _productImageService.UpdateProductImageAsync(updateProductImageDto);
            return Ok("Product image successfully updated.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while updating the product image: {ex.Message}");
        }
    }

    [HttpGet("ProductImagesByProductId/{id}")]
    public async Task<IActionResult> ProductImagesByProductId(string id)
    {
        try
        {
            var images = await _productImageService.GetByProductIdProductImageAsync(id);
            return Ok(images);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while retrieving images by product ID: {ex.Message}");
        }
    }
}
