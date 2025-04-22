using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Catalog.Dtos.ProductDetailDtos;
using MultiShop.Catalog.Services.ProductDetailServices;

namespace MultiShop.Catalog.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class ProductDetailsController : ControllerBase
{
    private readonly IProductDetailService _productDetailService;

    public ProductDetailsController(IProductDetailService productDetailService)
    {
        _productDetailService = productDetailService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllProductDetailList()
    {
        try
        {
            var details = await _productDetailService.GetAllProductDetailAsync();
            return Ok(details);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while retrieving product details: {ex.Message}");
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProductDetailById(string id)
    {
        try
        {
            var detail = await _productDetailService.GetByIdProductDetailAsync(id);
            if (detail == null)
                return NotFound("Product detail not found.");

            return Ok(detail);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while retrieving the product detail: {ex.Message}");
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateProductDetail(CreateProductDetailDto createProductDetailDto)
    {
        try
        {
            await _productDetailService.CreateProductDetailAsync(createProductDetailDto);
            return Ok("Product detail successfully created.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while creating the product detail: {ex.Message}");
        }
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteProductDetail(string id)
    {
        try
        {
            await _productDetailService.DeleteProductDetailAsync(id);
            return Ok("Product detail successfully deleted.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while deleting the product detail: {ex.Message}");
        }
    }

    [HttpPut]
    public async Task<IActionResult> UpdateProductDetail(UpdateProductDetailDto updateProductDetailDto)
    {
        try
        {
            await _productDetailService.UpdateProductDetailAsync(updateProductDetailDto);
            return Ok("Product detail successfully updated.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while updating the product detail: {ex.Message}");
        }
    }

    [HttpGet("GetProductDetailByProductId/{id}")]
    public async Task<IActionResult> GetProductDetailByProductId(string id)
    {
        try
        {
            var details = await _productDetailService.GetByProductIdProductDetailAsync(id);
            return Ok(details);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while retrieving product details by product ID: {ex.Message}");
        }
    }
}
