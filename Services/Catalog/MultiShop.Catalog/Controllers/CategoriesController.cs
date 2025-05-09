using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Catalog.Dtos.CategoryDtos;
using MultiShop.Catalog.Services.CategoryServices;

namespace MultiShop.Catalog.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class CategoriesController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoriesController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllCategoryList()
    {
        var categories = await _categoryService.GetAllCategoryAsync();
        return Ok(categories);  
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCategoryById(string id)
    {
        var category = await _categoryService.GetByIdCategoryAsync(id);
        return category is null
            ? NotFound($"Category with ID '{id}' was not found.") 
            : Ok(category);
    }

    [HttpPost]
    public async Task<IActionResult> CreateCategory(CreateCategoryDto createCategoryDto)
    {
        await _categoryService.CreateCategoryAsync(createCategoryDto);
        return Ok("Category was successfully created.");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCategory(string id)
    {
        var existing = await _categoryService.GetByIdCategoryAsync(id);
        if (existing is null)
            return NotFound($"Category with ID '{id}' was not found.");

        await _categoryService.DeleteCategoryAsync(id);
        return Ok("Category was successfully deleted.");
    }

    [HttpPut]
    public async Task<IActionResult> UpdateCategory(UpdateCategoryDto updateCategoryDto)
    {
        var existing = await _categoryService.GetByIdCategoryAsync(updateCategoryDto.CategoryId);
        if (existing is null)
            return NotFound($"Category with ID '{updateCategoryDto.CategoryId}' was not found.");

        await _categoryService.UpdateCategoryAsync(updateCategoryDto);
        return Ok("Category was successfully updated.");
    }
}
