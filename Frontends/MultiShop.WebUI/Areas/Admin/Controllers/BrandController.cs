﻿using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.CatalogDtos.BrandDtos;
using MultiShop.WebUI.Services.CatalogServices.BrandServices;

namespace MultiShop.WebUI.Areas.Admin.Controllers;

[Area("Admin")]
[Route("Admin/Brand")]
public class BrandController : Controller
{
    private readonly IBrandService _brandService;
    public BrandController(IBrandService brandService)
    {
        _brandService = brandService;
    }

    [Route("Index")]
    public async Task<IActionResult> Index()
    {
        BrandViewBagList();

        var brands = await _brandService.GetAllBrandAsync();
        return View(brands);
    }

    [HttpGet]
    [Route("CreateBrand")]
    public IActionResult CreateBrand()
    {
        BrandViewBagList();
        return View();
    }

    [HttpPost]
    [Route("CreateBrand")]
    public async Task<IActionResult> CreateBrand(CreateBrandDto createBrandDto)
    {
        await _brandService.CreateBrandAsync(createBrandDto);
        return RedirectToAction("Index", "Brand", new { area = "Admin" });
    }

    [Route("DeleteBrand/{id}")]
    public async Task<IActionResult> DeleteBrand(string id)
    {
        await _brandService.DeleteBrandAsync(id);
        return RedirectToAction("Index", "Brand", new { area = "Admin" });
    }

    [HttpGet]
    [Route("UpdateBrand/{id}")]
    public async Task<IActionResult> UpdateBrand(string id)
    {
        BrandViewBagList();
        var brand = await _brandService.GetByIdBrandAsync(id);
        return View(brand);
    }

    [HttpPost]
    [Route("UpdateBrand/{Id}")]
    public async Task<IActionResult> UpdateBrand(UpdateBrandDto updateBrandDto)
    {
        await _brandService.UpdateBrandAsync(updateBrandDto);
        return RedirectToAction("Index", "Brand", new { area = "Admin" });
    }

    void BrandViewBagList()
    {
        ViewBag.v0 = "Marka İşlemleri";
        ViewBag.v1 = "Ana Sayfa";
        ViewBag.v2 = "Markalar";
        ViewBag.v3 = "Marka Listesi";
    }
}
