using Microsoft.AspNetCore.Mvc;
using MultiShop.Images.WebUI.DAL.Entities;
using MultiShop.Images.WebUI.Services;

namespace MultiShop.Images.WebUI.Controllers;

public class DefaultController : Controller
{
    private readonly ICloudStorageService _cloudStorageService;

    public DefaultController(ICloudStorageService cloudStorageService)
    {
        _cloudStorageService = cloudStorageService ?? throw new ArgumentNullException(nameof(cloudStorageService));
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(ImageDrive imageDrive)
    {
        if (imageDrive == null)
        {
            ViewBag.ErrorMessage = "Geçersiz veri gönderildi.";
            return View();
        }

        if (imageDrive.Photo != null && imageDrive.Photo.Length > 0)
        {
            imageDrive.SavedFileName = GenerateFileNameToSave(imageDrive.Photo.FileName);
            imageDrive.SavedUrl = await _cloudStorageService.UploadFileAsync(imageDrive.Photo, imageDrive.SavedFileName!);

            ViewBag.SuccessMessage = "Resim başarıyla yüklendi.";
        }
        else
        {
            ViewBag.WarningMessage = "Yüklenecek bir dosya bulunamadı.";
        }

        return View();
    }

    private static string GenerateFileNameToSave(string incomingFileName)
    {
        var fileName = Path.GetFileNameWithoutExtension(incomingFileName);
        var extension = Path.GetExtension(incomingFileName);
        var timestamp = DateTime.UtcNow.ToString("yyyyMMddHHmmss");
        return $"{fileName}-{timestamp}{extension}";
    }
}
