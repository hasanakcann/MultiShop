using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Catalog.Dtos.CategoryDtos;
using MultiShop.Catalog.Services.CategoryServices;

namespace MultiShop.Catalog.Controllers;

//[Authorize]
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
        try
        {
            var categories = await _categoryService.GetAllCategoryAsync();
            return Ok(categories);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while retrieving categories: {ex.Message}");
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCategoryById(string id)
    {
        try
        {
            var category = await _categoryService.GetByIdCategoryAsync(id);
            if (category == null)
                return NotFound("Category not found.");

            return Ok(category);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while retrieving the category: {ex.Message}");
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateCategory(CreateCategoryDto createCategoryDto)
    {
        try
        {
            await _categoryService.CreateCategoryAsync(createCategoryDto);
            return Ok("Category successfully created.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while creating the category: {ex.Message}");
        }
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteCategory(string id)
    {
        try
        {
            await _categoryService.DeleteCategoryAsync(id);
            return Ok("Category successfully deleted.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while deleting the category: {ex.Message}");
        }
    }

    [HttpPut]
    public async Task<IActionResult> UpdateCategory(UpdateCategoryDto updateCategoryDto)
    {
        try
        {
            await _categoryService.UpdateCategoryAsync(updateCategoryDto);
            return Ok("Category successfully updated.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while updating the category: {ex.Message}");
        }
    }
}
