using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Catalog.Dtos.ProductDtos;
using MultiShop.Catalog.Services.ProductServices;

namespace MultiShop.Catalog.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllProductList()
    {
        try
        {
            var products = await _productService.GetAllProductAsync();
            return Ok(products);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while retrieving products: {ex.Message}");
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProductById(string id)
    {
        try
        {
            var product = await _productService.GetByIdProductAsync(id);
            if (product == null)
                return NotFound("Product not found.");

            return Ok(product);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while retrieving the product: {ex.Message}");
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateProduct(CreateProductDto createProductDto)
    {
        try
        {
            await _productService.CreateProductAsync(createProductDto);
            return Ok("Product successfully created.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while creating the product: {ex.Message}");
        }
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteProduct(string id)
    {
        try
        {
            await _productService.DeleteProductAsync(id);
            return Ok("Product successfully deleted.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while deleting the product: {ex.Message}");
        }
    }

    [HttpPut]
    public async Task<IActionResult> UpdateProduct(UpdateProductDto updateProductDto)
    {
        try
        {
            await _productService.UpdateProductAsync(updateProductDto);
            return Ok("Product successfully updated.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while updating the product: {ex.Message}");
        }
    }

    [HttpGet("ProductListWithCategory")]
    public async Task<IActionResult> ProductListWithCategory()
    {
        try
        {
            var products = await _productService.GetProductsWithCategoryAsync();
            return Ok(products);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while retrieving products with categories: {ex.Message}");
        }
    }

    [HttpGet("ProductsWithCategoryByCategoryId/{id}")]
    public async Task<IActionResult> GetProductsWithCategoryByCategoryId(string id)
    {
        try
        {
            var products = await _productService.GetProductsWithCategoryByCategoryIdAsync(id);
            return Ok(products);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while retrieving products by category: {ex.Message}");
        }
    }
}
