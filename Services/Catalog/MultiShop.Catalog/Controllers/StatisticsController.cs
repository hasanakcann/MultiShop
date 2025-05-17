using Microsoft.AspNetCore.Mvc;
using MultiShop.Catalog.Services.StatisticServices;

namespace MultiShop.Catalog.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StatisticsController : ControllerBase
{
    private readonly IStatisticService _statisticService;
    public StatisticsController(IStatisticService statisticService)
    {
        _statisticService = statisticService;
    }

    [HttpGet("GetBrandCount")]
    public async Task<IActionResult> GetBrandCount()
    {
        var brandCount = await _statisticService.GetBrandCountAsync();
        return Ok(brandCount);
    }

    [HttpGet("GetCategoryCount")]
    public async Task<IActionResult> GetCategoryCount()
    {
        var categoryCount = await _statisticService.GetCategoryCountAsync();
        return Ok(categoryCount);
    }

    [HttpGet("GetProductCount")]
    public async Task<IActionResult> GetProductCount()
    {
        var productCount = await _statisticService.GetProductCountAsync();
        return Ok(productCount);
    }

    [HttpGet("GetProductAveragePrice")]
    public async Task<IActionResult> GetProductAveragePrice()
    {
        var averagePrice = await _statisticService.GetProductAveragePriceAsync();
        return Ok(averagePrice);
    }

    [HttpGet("GetMaxPriceProductName")]
    public async Task<IActionResult> GetMaxPriceProductName()
    {
        var maxPriceProductName = await _statisticService.GetMaxPriceProductNameAsync();
        return Ok(maxPriceProductName);
    }

    [HttpGet("GetMinPriceProductName")]
    public async Task<IActionResult> GetMinPriceProductName()
    {
        var minPriceProductName = await _statisticService.GetMinPriceProductNameAsync();
        return Ok(minPriceProductName);
    }
}
