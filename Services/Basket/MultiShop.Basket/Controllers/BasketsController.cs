using Microsoft.AspNetCore.Mvc;
using MultiShop.Basket.Dtos;
using MultiShop.Basket.LoginServices;
using MultiShop.Basket.Services;

namespace MultiShop.Basket.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BasketsController : ControllerBase
{
    private readonly IBasketService _basketService;
    private readonly ILoginService _loginService;

    public BasketsController(IBasketService basketService, ILoginService loginService)
    {
        _basketService = basketService;
        _loginService = loginService;
    }

    [HttpGet]
    public async Task<IActionResult> GetMyBasketDetail()
    {
        try
        {
            var userId = _loginService.GetUserId;
            var basket = await _basketService.GetBasket(userId);

            if (basket == null)
                return NotFound("Basket not found.");

            return Ok(basket);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while retrieving the basket. Details: {ex.Message}");
        }
    }

    [HttpPost]
    public async Task<IActionResult> SaveMyBasket(BasketTotalDto basketTotalDto)
    {
        try
        {
            basketTotalDto.UserId = _loginService.GetUserId;
            await _basketService.SaveBasket(basketTotalDto);

            return Ok("Changes to the basket have been saved successfully.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while saving the basket. Details: {ex.Message}");
        }
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteBasket()
    {
        try
        {
            await _basketService.DeleteBasket(_loginService.GetUserId);
            return Ok("Basket deleted successfully.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while deleting the basket. Details: {ex.Message}");
        }
    }
}
