﻿using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.CatalogDtos.FeatureDtos;
using MultiShop.WebUI.Services.CatalogServices.FeatureServices;

namespace MultiShop.WebUI.Areas.Admin.Controllers;

[Area("Admin")]
[Route("Admin/Feature")]
public class FeatureController : Controller
{
    private readonly IFeatureService _featureService;
    public FeatureController(IFeatureService featureService)
    {
        _featureService = featureService;
    }

    [Route("Index")]
    public async Task<IActionResult> Index()
    {
        FeatureViewBagList();
        var features = await _featureService.GetAllFeatureAsync();
        return View(features);
    }

    [HttpGet]
    [Route("CreateFeature")]
    public IActionResult CreateFeature()
    {
        FeatureViewBagList();
        return View();
    }

    [HttpPost]
    [Route("CreateFeature")]
    public async Task<IActionResult> CreateFeature(CreateFeatureDto createFeatureDto)
    {
        await _featureService.CreateFeatureAsync(createFeatureDto);
        return RedirectToAction("Index", "Feature", new { area = "Admin" });
    }

    [Route("DeleteFeature/{id}")]
    public async Task<IActionResult> DeleteFeature(string id)
    {
        await _featureService.DeleteFeatureAsync(id);
        return RedirectToAction("Index", "Feature", new { area = "Admin" });
    }

    [HttpGet]
    [Route("UpdateFeature/{id}")]
    public async Task<IActionResult> UpdateFeature(string id)
    {
        FeatureViewBagList();
        var feature = await _featureService.GetByIdFeatureAsync(id);
        return View(feature);
    }

    [HttpPost]
    [Route("UpdateFeature/{Id}")]
    public async Task<IActionResult> UpdateFeature(UpdateFeatureDto updateFeatureDto)
    {
        await _featureService.UpdateFeatureAsync(updateFeatureDto);
        return RedirectToAction("Index", "Feature", new { area = "Admin" });
    }

    void FeatureViewBagList()
    {
        ViewBag.v0 = "Anasayfa Öne Çıkan İşlemleri";
        ViewBag.v1 = "Ana Sayfa";
        ViewBag.v2 = "Öne Çıkan Alanlar";
        ViewBag.v3 = "Öne Çıkan Listesi";
    }
}
