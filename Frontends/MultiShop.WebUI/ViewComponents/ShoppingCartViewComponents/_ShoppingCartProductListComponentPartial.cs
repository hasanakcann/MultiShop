﻿using Microsoft.AspNetCore.Mvc;
using MultiShop.WebUI.Services.BasketServices;

namespace MultiShop.WebUI.ViewComponents.ShoppingCartViewComponents;

public class _ShoppingCartProductListComponentPartial : ViewComponent
{
    private readonly IBasketService _basketService;
    public _ShoppingCartProductListComponentPartial(IBasketService basketService)
    {
        _basketService = basketService;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var basket = await _basketService.GetBasket();
        var basketItems = basket.BasketItems;
        return View(basketItems);
    }
}
