using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.OrderDtos.OrderAddressDtos;
using MultiShop.WebUI.Services.Interfaces;
using MultiShop.WebUI.Services.OrderServices.OrderAddressServices;

namespace MultiShop.WebUI.Controllers;

public class OrderController : Controller
{
    private readonly IOrderAddressService _orderAdressService;
    private readonly IUserService _userService;
    public OrderController(IOrderAddressService orderAdressService, IUserService userService)
    {
        _orderAdressService = orderAdressService;
        _userService = userService;
    }

    [HttpGet]
    public IActionResult Index()
    {
        ViewBag.directory1 = "MultiShop";
        ViewBag.directory2 = "Siparişler";
        ViewBag.directory3 = "Sipariş İşlemleri";
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Index(CreateOrderAddressDto createOrderAddressDto)
    {
        var user = await _userService.GetUserInfo();
        createOrderAddressDto.UserId = user.Id;
        await _orderAdressService.CreateOrderAddressAsync(createOrderAddressDto);
        return RedirectToAction("Index", "Payment");
    }
}
