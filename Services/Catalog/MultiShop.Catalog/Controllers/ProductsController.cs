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
        var products = await _productService.GetAllProductAsync();
        return Ok(products);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProductById(string id)
    {
        var product = await _productService.GetByIdProductAsync(id);
        return product is null
            ? NotFound("Product not found.")
            : Ok(product);
    }

    [HttpPost]
    public async Task<IActionResult> CreateProduct(CreateProductDto createProductDto)
    {
        await _productService.CreateProductAsync(createProductDto);
        return Ok("Product successfully created.");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(string id)
    {
        var existing = await _productService.GetByIdProductAsync(id);
        if (existing is null)
            return NotFound("Product not found.");

        await _productService.DeleteProductAsync(id);
        return Ok("Product successfully deleted.");
    }

    [HttpPut]
    public async Task<IActionResult> UpdateProduct(UpdateProductDto updateProductDto)
    {
        var existing = await _productService.GetByIdProductAsync(updateProductDto.ProductId);
        if (existing is null)
            return NotFound($"Product with ID '{updateProductDto.ProductId}' was not found.");

        await _productService.UpdateProductAsync(updateProductDto);
        return Ok("Product successfully updated.");
    }

    [HttpGet("ProductListWithCategory")]
    public async Task<IActionResult> ProductListWithCategory()
    {
        var products = await _productService.GetProductsWithCategoryAsync();
        return Ok(products);
    }

    [HttpGet("ProductsWithCategoryByCategoryId/{id}")]
    public async Task<IActionResult> GetProductsWithCategoryByCategoryId(string id)
    {
        var products = await _productService.GetProductsWithCategoryByCategoryIdAsync(id);
        return Ok(products);
    }
}
