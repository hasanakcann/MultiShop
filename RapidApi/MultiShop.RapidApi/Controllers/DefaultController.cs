using Microsoft.AspNetCore.Mvc;
using MultiShop.RapidApi.Models.Exchange;
using MultiShop.RapidApi.Models.Weather;
using Newtonsoft.Json;

namespace MultiShop.RapidApi.Controllers;

public class DefaultController : Controller
{
    public async Task<IActionResult> WeatherDetail()
    {
        var client = new HttpClient();
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri("https://the-weather-api.p.rapidapi.com/api/weather/istanbul"),
            Headers =
            {
                { "x-rapidapi-key", "856b8f787amsh3ff97a9011755b7p150001jsnd395b6312f10" },
                { "x-rapidapi-host", "the-weather-api.p.rapidapi.com" },
            },
        };

        using (var response = await client.SendAsync(request))
        {
            response.EnsureSuccessStatusCode();

            var body = await response.Content.ReadAsStringAsync();
            var weatherData = JsonConvert.DeserializeObject<WeatherViewModel>(body);

            if (weatherData == null || weatherData.Data == null)
            {
                ViewBag.ErrorMessage = "Weather data is not available.";
                return View();
            }

            ViewBag.cityTemp = weatherData.Data.Temp;
            return View(weatherData);
        }
    }

    public async Task<IActionResult> Exchange()
    {
        using var client = new HttpClient();
        client.DefaultRequestHeaders.Add("x-rapidapi-key", "856b8f787amsh3ff97a9011755b7p150001jsnd395b6312f10");
        client.DefaultRequestHeaders.Add("x-rapidapi-host", "real-time-finance-data.p.rapidapi.com");

        async Task<ExchangeViewModel?> GetExchangeRate(string fromSymbol, string toSymbol)
        {
            var url = $"https://real-time-finance-data.p.rapidapi.com/currency-exchange-rate?from_symbol={fromSymbol}&to_symbol={toSymbol}&language=en";
            var response = await client.GetAsync(url);
            if (!response.IsSuccessStatusCode) return null;

            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ExchangeViewModel>(json);
        }

        var usdToTry = await GetExchangeRate("USD", "TRY");
        var usdToEur = await GetExchangeRate("USD", "EUR");
        var eurToTry = await GetExchangeRate("EUR", "TRY");

        if (usdToTry == null || usdToTry.Data == null ||
            usdToEur == null || usdToEur.Data == null ||
            eurToTry == null || eurToTry.Data == null)
        {
            ViewBag.ErrorMessage = "Exchange data is not available.";
            return View();
        }

        ViewBag.UsdToTryRate = usdToTry.Data.ExchangeRate;
        ViewBag.UsdToTryPreviousClose = usdToTry.Data.PreviousClose;
        ViewBag.UsdToTryLastUpdate = usdToTry.Data.LastUpdateUtc;

        ViewBag.UsdToEurRate = usdToEur.Data.ExchangeRate;
        ViewBag.UsdToEurPreviousClose = usdToEur.Data.PreviousClose;
        ViewBag.UsdToEurLastUpdate = usdToEur.Data.LastUpdateUtc;

        ViewBag.EurToTryRate = eurToTry.Data.ExchangeRate;
        ViewBag.EurToTryPreviousClose = eurToTry.Data.PreviousClose;
        ViewBag.EurToTryLastUpdate = eurToTry.Data.LastUpdateUtc;

        return View();
    }
}
