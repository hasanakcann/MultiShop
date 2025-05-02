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
        try
        {
            var value = await _statisticService.GetBrandCount();
            return Ok(value);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error fetching brand count: {ex.Message}");
        }
    }

    [HttpGet("GetCategoryCount")]
    public async Task<IActionResult> GetCategoryCount()
    {
        try
        {
            var value = await _statisticService.GetCategoryCount();
            return Ok(value);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error fetching category count: {ex.Message}");
        }
    }

    [HttpGet("GetProductCount")]
    public async Task<IActionResult> GetProductCount()
    {
        try
        {
            var value = await _statisticService.GetProductCount();
            return Ok(value);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error fetching product count: {ex.Message}");
        }
    }

    [HttpGet("GetProductAveragePrice")]
    public async Task<IActionResult> GetProductAveragePrice()
    {
        try
        {
            var value = await _statisticService.GetProductAveragePrice();
            return Ok(value);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error fetching average product price: {ex.Message}");
        }
    }

    [HttpGet("GetMaxPriceProductName")]
    public async Task<IActionResult> GetMaxPriceProductName()
    {
        try
        {
            var value = await _statisticService.GetMaxPriceProductName();
            return Ok(value);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error fetching max price product name: {ex.Message}");
        }
    }

    [HttpGet("GetMinPriceProductName")]
    public async Task<IActionResult> GetMinPriceProductName()
    {
        try
        {
            var value = await _statisticService.GetMinPriceProductName();
            return Ok(value);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error fetching min price product name: {ex.Message}");
        }
    }
}
