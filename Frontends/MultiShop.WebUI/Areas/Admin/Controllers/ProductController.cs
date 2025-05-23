﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MultiShop.DtoLayer.CatalogDtos.ProductDtos;
using MultiShop.WebUI.Services.CatalogServices.CategoryServices;
using MultiShop.WebUI.Services.CatalogServices.ProductServices;

namespace MultiShop.WebUI.Areas.Admin.Controllers;

[Area("Admin")]
[Route("Admin/Product")]
public class ProductController : Controller
{
    private readonly IProductService _productService;
    private readonly ICategoryService _categoryService;

    public ProductController(IProductService productService, ICategoryService categoryService)
    {
        _productService = productService;
        _categoryService = categoryService;
    }

    [Route("Index")]
    public async Task<IActionResult> Index()
    {
        ProductViewBagList();
        var products = await _productService.GetAllProductAsync();
        return View(products);
    }

    [HttpGet]
    [Route("CreateProduct")]
    public async Task<IActionResult> CreateProduct()
    {
        ProductViewBagList();

        var categories = await _categoryService.GetAllCategoryAsync();
        List<SelectListItem> categoryValues = (from x in categories
                                               select new SelectListItem
                                               {
                                                   Text = x.CategoryName,
                                                   Value = x.CategoryId
                                               }).ToList();
        ViewBag.CategoryValues = categoryValues;

        return View();
    }

    [HttpPost]
    [Route("CreateProduct")]
    public async Task<IActionResult> CreateProduct(CreateProductDto createProductDto)
    {
        await _productService.CreateProductAsync(createProductDto);
        return RedirectToAction("Index", "Product", new { area = "Admin" });
    }

    [Route("DeleteProduct/{id}")]
    public async Task<IActionResult> DeleteProduct(string id)
    {
        await _productService.DeleteProductAsync(id);
        return RedirectToAction("Index", "Product", new { area = "Admin" });
    }

    [HttpGet]
    [Route("UpdateProduct/{id}")]
    public async Task<IActionResult> UpdateProduct(string id)
    {
        ProductViewBagList();

        var categories = await _categoryService.GetAllCategoryAsync();
        List<SelectListItem> categoryValues = (from x in categories
                                               select new SelectListItem
                                               {
                                                   Text = x.CategoryName,
                                                   Value = x.CategoryId
                                               }).ToList();
        ViewBag.CategoryValues = categoryValues;

        var product = await _productService.GetByIdProductAsync(id);
        return View(product);
    }

    [HttpPost]
    [Route("UpdateProduct/{Id}")]
    public async Task<IActionResult> UpdateProduct(UpdateProductDto updateProductDto)
    {
        await _productService.UpdateProductAsync(updateProductDto);
        return RedirectToAction("Index", "Product", new { area = "Admin" });
    }

    [Route("ProductListWithCategory")]
    public async Task<IActionResult> ProductListWithCategory()
    {
        ProductViewBagList();

        var productsWithCategory = await _productService.GetProductsWithCategoryAsync();
        return View(productsWithCategory);
    }

    void ProductViewBagList()
    {
        ViewBag.v0 = "Ürün İşlemleri";
        ViewBag.v1 = "Ana Sayfa";
        ViewBag.v2 = "Ürünler";
        ViewBag.v3 = "Ürün Listesi";
    }
}
