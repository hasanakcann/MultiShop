using Microsoft.AspNetCore.Mvc;
using MultiShop.WebUI.Services.CatalogServices.CategoryServices;

namespace MultiShop.WebUI.ViewComponents.DefaultViewComponents;

public class _CategoriesDefaultComponentPartial : ViewComponent
{
    private readonly ICategoryService _categoryService;
    public _CategoriesDefaultComponentPartial(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var categories = await _categoryService.GetAllCategoryAsync();
        return View(categories);
    }
}
