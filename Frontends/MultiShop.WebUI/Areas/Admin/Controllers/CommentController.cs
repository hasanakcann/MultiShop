using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.CommentDtos;
using Newtonsoft.Json;
using System.Text;

namespace MultiShop.WebUI.Areas.Admin.Controllers;

[Area("Admin")]
[Route("Admin/Comment")]
public class CommentController : Controller
{
    private readonly IHttpClientFactory _httpClientFactory;
    private const string baseUrl = "http://localhost:7075/api/Comments";

    public CommentController(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    [Route("Index")]
    public async Task<IActionResult> Index()
    {
        ViewBag.v0 = "Yorum İşlemleri";
        ViewBag.v1 = "Ana Sayfa";
        ViewBag.v2 = "Yorumlar";
        ViewBag.v3 = "Yorum Listesi";

        var client = _httpClientFactory.CreateClient();
        var responseMessage = await client.GetAsync(baseUrl);

        if (responseMessage.IsSuccessStatusCode)
        {
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<ResultCommentDto>>(jsonData);
            return View(values);
        }

        return View();
    }

    [Route("DeleteComment/{id}")]
    public async Task<IActionResult> DeleteComment(string id)
    {
        var client = _httpClientFactory.CreateClient();
        var responseMessage = await client.DeleteAsync($"{baseUrl}/{id}");
        return RedirectToAction("Index", "Comment", new { area = "Admin" });
    }

    [HttpGet]
    [Route("UpdateComment/{id}")]
    public async Task<IActionResult> UpdateComment(string id)
    {
        ViewBag.v0 = "Yorum İşlemleri";
        ViewBag.v1 = "Ana Sayfa";
        ViewBag.v2 = "Yorumlar";
        ViewBag.v3 = "Yorum Güncelleme Sayfası";

        var client = _httpClientFactory.CreateClient();
        var responseMessage = await client.GetAsync($"{baseUrl}/{id}");
        if (responseMessage.IsSuccessStatusCode)
        {
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<UpdateCommentDto>(jsonData);
            return View(values);
        }
        return View();
    }

    [HttpPost]
    [Route("UpdateComment/{Id}")]
    public async Task<IActionResult> UpdateComment(UpdateCommentDto updateCommentDto)
    {
        updateCommentDto.Status = true;
        var client = _httpClientFactory.CreateClient();
        var jsonData = JsonConvert.SerializeObject(updateCommentDto);
        StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");

        var responseMessage = await client.PutAsync(baseUrl, stringContent);
        if (responseMessage.IsSuccessStatusCode)
        {
            return RedirectToAction("Index", "Comment", new { area = "Admin" });
        }
        return View();
    }
}
