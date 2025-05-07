using Microsoft.AspNetCore.Mvc;
using MultiShop.WebUI.Services.CatalogServices.SpecialOfferServices;

namespace MultiShop.WebUI.ViewComponents.DefaultViewComponents;

public class _SpecialOfferDefaultComponentPartial : ViewComponent
{
    private readonly ISpecialOfferService _specialOfferService;

    public _SpecialOfferDefaultComponentPartial(ISpecialOfferService specialOfferService)
    {
        _specialOfferService = specialOfferService;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var specialOffers = await _specialOfferService.GetAllSpecialOfferAsync();
        return View(specialOffers);
    }
}
