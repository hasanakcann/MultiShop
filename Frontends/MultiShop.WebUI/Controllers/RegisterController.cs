using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.IdentityDtos.RegisterDtos;
using Newtonsoft.Json;
using System.Text;

namespace MultiShop.WebUI.Controllers;

public class RegisterController : Controller
{
    private readonly IHttpClientFactory _httpClientFactory;
    private const string registerApiUrl = "http://localhost:5001/api/Registers";

    public RegisterController(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Index(CreateRegisterDto createRegisterDto)
    {
        if (!ModelState.IsValid)
        {
            return View(createRegisterDto);
        }

        if (createRegisterDto.Password != createRegisterDto.ConfirmPassword)
        {
            ModelState.AddModelError(string.Empty, "Passwords do not match.");
            return View(createRegisterDto);
        }

        var client = _httpClientFactory.CreateClient();
        var jsonData = JsonConvert.SerializeObject(createRegisterDto);
        var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

        try
        {
            var response = await client.PostAsync(registerApiUrl, content);
            if (response.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "Registration successful. You can now log in.";
                return RedirectToAction("Index", "Login");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "An error occurred during registration. Please try again.");
            }
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, $"Server error: {ex.Message}");
        }

        return View(createRegisterDto);
    }
}
