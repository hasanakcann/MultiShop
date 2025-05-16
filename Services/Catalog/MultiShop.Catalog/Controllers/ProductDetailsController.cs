using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Catalog.Dtos.ProductDetailDtos;
using MultiShop.Catalog.Services.ProductDetailServices;

namespace MultiShop.Catalog.Controllers;

[AllowAnonymous]
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
        var details = await _productDetailService.GetAllProductDetailAsync();
        return Ok(details);  
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProductDetailById(string id)
    {
        var detail = await _productDetailService.GetByIdProductDetailAsync(id);
        return detail is null
            ? NotFound("Product detail not found.")  
            : Ok(detail);
    }

    [HttpPost]
    public async Task<IActionResult> CreateProductDetail(CreateProductDetailDto createProductDetailDto)
    {
        await _productDetailService.CreateProductDetailAsync(createProductDetailDto);
        return Ok("Product detail successfully created.");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProductDetail(string id)
    {
        var existing = await _productDetailService.GetByIdProductDetailAsync(id);
        if (existing is null)
            return NotFound("Product detail not found.");

        await _productDetailService.DeleteProductDetailAsync(id);
        return Ok("Product detail successfully deleted.");
    }

    [HttpPut]
    public async Task<IActionResult> UpdateProductDetail(UpdateProductDetailDto updateProductDetailDto)
    {
        var existing = await _productDetailService.GetByIdProductDetailAsync(updateProductDetailDto.ProductDetailId);
        if (existing is null)
            return NotFound($"Product detail with ID '{updateProductDetailDto.ProductDetailId}' was not found.");

        await _productDetailService.UpdateProductDetailAsync(updateProductDetailDto);
        return Ok("Product detail successfully updated.");
    }

    [HttpGet("GetProductDetailByProductId/{id}")]
    public async Task<IActionResult> GetProductDetailByProductId(string id)
    {
        var details = await _productDetailService.GetByProductIdProductDetailAsync(id);
        return Ok(details);  
    }
}
