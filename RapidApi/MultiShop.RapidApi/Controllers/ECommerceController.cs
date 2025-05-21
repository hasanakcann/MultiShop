using Microsoft.AspNetCore.Mvc;
using MultiShop.RapidApi.Models.ECommerce;
using Newtonsoft.Json;

namespace MultiShop.RapidApi.Controllers;

public class ECommerceController : Controller
{
    public async Task<IActionResult> ECommerceList()
    {
        var client = new HttpClient();
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri("https://real-time-product-search.p.rapidapi.com/product-offers-v2?product_id=catalogid%3A15554707778408471208%2Cgpcid%3A6219277726645206819%2CheadlineOfferDocid%3A8835386203856143595%2Crds%3APC_15478400683365031707%7CPROD_PC_15478400683365031707%2CimageDocid%3A10653897321817113741%2Cmid%3A576462815432560445%2Cpvt%3Ahg%2Cpvf%3A&page=1&country=us&language=en"),
            Headers =
            {
                { "x-rapidapi-key", "856b8f787amsh3ff97a9011755b7p150001jsnd395b6312f10" },
                { "x-rapidapi-host", "real-time-product-search.p.rapidapi.com" },
            },
        };

        using (var response = await client.SendAsync(request))
        {
            response.EnsureSuccessStatusCode();
            var body = await response.Content.ReadAsStringAsync();
            var ecommerceData = JsonConvert.DeserializeObject<ECommerceViewModel>(body);
            return View(ecommerceData);
        }
    }
}
