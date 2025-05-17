using Microsoft.AspNetCore.Mvc;
using MultiShop.WebUI.Services.CargoServices.CargoCustomerServices;
using MultiShop.WebUI.Services.UserIdentityServices;

namespace MultiShop.WebUI.Areas.Admin.Controllers;

[Area("Admin")]
public class UserController : Controller
{
    private readonly IUserIdentityService _userIdentityService;
    private readonly ICargoCustomerService _cargoCustomerService;
    public UserController(IUserIdentityService userIdentityService, ICargoCustomerService cargoCustomerService)
    {
        _userIdentityService = userIdentityService;
        _cargoCustomerService = cargoCustomerService;
    }

    public async Task<IActionResult> UserList()
    {
        ViewBag.v0 = "Kullanıcı Listesi";
        ViewBag.v1 = "Admin";
        ViewBag.v2 = "Kullanıcı";
        ViewBag.v3 = "Kullanıcı Listesi";

        var users = await _userIdentityService.GetAllUserListAsync();
        return View(users);
    }

    public async Task<IActionResult> UserAddressInfo(string id)
    {
        ViewBag.v0 = "Kullanıcı Adres ve İletişim Bilgileri";
        ViewBag.v1 = "Admin";
        ViewBag.v2 = "Kullanıcı";
        ViewBag.v3 = "Adres ve İletişim Bilgileri";

        var cargoCustomer = await _cargoCustomerService.GetByIdCargoCustomerInfoAsync(id);
        return View(cargoCustomer);
    }
}
