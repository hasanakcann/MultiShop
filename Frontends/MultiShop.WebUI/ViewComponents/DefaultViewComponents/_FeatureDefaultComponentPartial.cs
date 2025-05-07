using Microsoft.AspNetCore.Mvc;
using MultiShop.WebUI.Services.CatalogServices.FeatureServices;

namespace MultiShop.WebUI.ViewComponents.DefaultViewComponents;

public class _FeatureDefaultComponentPartial : ViewComponent
{
    private readonly IFeatureService _featureService;
    public _FeatureDefaultComponentPartial(IFeatureService featureService)
    {
        _featureService = featureService;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var features = await _featureService.GetAllFeatureAsync();
        return View(features);
    }
}
