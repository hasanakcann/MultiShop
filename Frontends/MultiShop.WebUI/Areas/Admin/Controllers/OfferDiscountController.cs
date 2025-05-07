using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.CatalogDtos.OfferDiscountDtos;
using MultiShop.WebUI.Services.CatalogServices.OfferDiscountServices;

namespace MultiShop.WebUI.Areas.Admin.Controllers;

[Area("Admin")]
[Route("Admin/OfferDiscount")]
public class OfferDiscountController : Controller
{
   private readonly IOfferDiscountService _offerDiscountService;

    public OfferDiscountController(IOfferDiscountService offerDiscountService)
    {
        _offerDiscountService = offerDiscountService;
    }
    
    [Route("Index")]
    public async Task<IActionResult> Index()
    {
        OfferDiscountViewBagList();

        var offerDiscounts = await _offerDiscountService.GetAllOfferDiscountAsync();
        return View(offerDiscounts);
    }

    [HttpGet]
    [Route("CreateOfferDiscount")]
    public IActionResult CreateOfferDiscount()
    {
        OfferDiscountViewBagList();
        return View();
    }

    [HttpPost]
    [Route("CreateOfferDiscount")]
    public async Task<IActionResult> CreateOfferDiscount(CreateOfferDiscountDto createOfferDiscountDto)
    {
        await _offerDiscountService.CreateOfferDiscountAsync(createOfferDiscountDto);
        return RedirectToAction("Index", "OfferDiscount", new { area = "Admin" });
    }

    [Route("DeleteOfferDiscount/{id}")]
    public async Task<IActionResult> DeleteOfferDiscount(string id)
    {
        await _offerDiscountService.DeleteOfferDiscountAsync(id);
        return RedirectToAction("Index", "OfferDiscount", new { area = "Admin" });
    }

    [HttpGet]
    [Route("UpdateOfferDiscount/{id}")]
    public async Task<IActionResult> UpdateOfferDiscount(string id)
    {
        OfferDiscountViewBagList();

        var offerDiscount = await _offerDiscountService.GetByIdOfferDiscountAsync(id);
        return View(offerDiscount);
    }

    [HttpPost]
    [Route("UpdateOfferDiscount/{Id}")]
    public async Task<IActionResult> UpdateOfferDiscount(UpdateOfferDiscountDto updateOfferDiscountDto)
    {
        await _offerDiscountService.UpdateOfferDiscountAsync(updateOfferDiscountDto);
        return RedirectToAction("Index", "OfferDiscount", new { area = "Admin" });
    }

    void OfferDiscountViewBagList()
    {
        ViewBag.v0 = "İndirim Teklif İşlemleri";
        ViewBag.v1 = "Ana Sayfa";
        ViewBag.v2 = "İndirim Teklifleri";
        ViewBag.v3 = "İndirim Teklif Listesi";
    }
}
