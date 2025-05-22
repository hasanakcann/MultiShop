using Microsoft.AspNetCore.Mvc;

namespace MultiShop.WebUI.Controllers;

public class InformationController : Controller
{
    public IActionResult Index()
    {
        ViewBag.directory1 = "MultiShop";
        ViewBag.directory2 = "Ana Sayfa";
        ViewBag.directory3 = "Çoklu Dil Desteği";
        return View();
    }
}
