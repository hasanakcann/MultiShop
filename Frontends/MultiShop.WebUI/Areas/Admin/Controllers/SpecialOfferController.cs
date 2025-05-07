using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.CatalogDtos.SpecialOfferDtos;
using MultiShop.WebUI.Services.CatalogServices.SpecialOfferServices;

namespace MultiShop.WebUI.Areas.Admin.Controllers;

[Area("Admin")]
[Route("Admin/SpecialOffer")]
public class SpecialOfferController : Controller
{
    private readonly ISpecialOfferService _specialOfferService;
    public SpecialOfferController(ISpecialOfferService specialOfferService)
    {
        _specialOfferService = specialOfferService;
    }

    [Route("Index")]
    public async Task<IActionResult> Index()
    {
        SpecialOfferViewBagList();

        var specialOffers = await _specialOfferService.GetAllSpecialOfferAsync();
        return View(specialOffers);
    }

    [HttpGet]
    [Route("CreateSpecialOffer")]
    public IActionResult CreateSpecialOffer()
    {
        SpecialOfferViewBagList();

        return View();
    }

    [HttpPost]
    [Route("CreateSpecialOffer")]
    public async Task<IActionResult> CreateSpecialOffer(CreateSpecialOfferDto createSpecialOfferDto)
    {
        await _specialOfferService.CreateSpecialOfferAsync(createSpecialOfferDto);
        return RedirectToAction("Index", "SpecialOffer", new { area = "Admin" });
    }

    [Route("DeleteSpecialOffer/{id}")]
    public async Task<IActionResult> DeleteSpecialOffer(string id)
    {
        await _specialOfferService.DeleteSpecialOfferAsync(id);
        return RedirectToAction("Index", "SpecialOffer", new { area = "Admin" });
    }

    [HttpGet]
    [Route("UpdateSpecialOffer/{id}")]
    public async Task<IActionResult> UpdateSpecialOffer(string id)
    {
        SpecialOfferViewBagList();

        var specialOffer = await _specialOfferService.GetByIdSpecialOfferAsync(id);
        return View(specialOffer);
    }

    [HttpPost]
    [Route("UpdateSpecialOffer/{Id}")]
    public async Task<IActionResult> UpdateSpecialOffer(UpdateSpecialOfferDto updateSpecialOfferDto)
    {
        await _specialOfferService.UpdateSpecialOfferAsync(updateSpecialOfferDto);
        return RedirectToAction("Index", "SpecialOffer", new { area = "Admin" });
    }

    void SpecialOfferViewBagList()
    {
        ViewBag.v0 = "Özel Teklif İşlemleri";
        ViewBag.v1 = "Ana Sayfa";
        ViewBag.v2 = "Özel Teklifler";
        ViewBag.v3 = "Özel Teklif ve Günün İndirim Listesi";
    }
}
