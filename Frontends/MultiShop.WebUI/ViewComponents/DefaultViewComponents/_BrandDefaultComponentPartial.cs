using Microsoft.AspNetCore.Mvc;
using MultiShop.WebUI.Services.CatalogServices.BrandServices;

namespace MultiShop.WebUI.ViewComponents.DefaultViewComponents;

public class _BrandDefaultComponentPartial : ViewComponent
{
    private readonly IBrandService _brandService;
    public _BrandDefaultComponentPartial(IBrandService brandService)
    {
        _brandService = brandService;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var brands = await _brandService.GetAllBrandAsync();
        return View(brands);
    }
}
