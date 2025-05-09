using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Catalog.Dtos.OfferDiscountDtos;
using MultiShop.Catalog.Services.OfferDiscountServices;

namespace MultiShop.Catalog.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class OfferDiscountsController : ControllerBase
{
    private readonly IOfferDiscountService _offerDiscountService;

    public OfferDiscountsController(IOfferDiscountService offerDiscountService)
    {
        _offerDiscountService = offerDiscountService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllOfferDiscountList()
    {
        var offerDiscounts = await _offerDiscountService.GetAllOfferDiscountAsync();
        return Ok(offerDiscounts);  
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetOfferDiscountById(string id)
    {
        var offerDiscount = await _offerDiscountService.GetByIdOfferDiscountAsync(id);
        return offerDiscount is null
            ? NotFound($"Offer discount with ID '{id}' was not found.")  
            : Ok(offerDiscount);
    }

    [HttpPost]
    public async Task<IActionResult> CreateOfferDiscount(CreateOfferDiscountDto createOfferDiscountDto)
    {
        await _offerDiscountService.CreateOfferDiscountAsync(createOfferDiscountDto);
        return Ok("Offer discount was successfully added.");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteOfferDiscount(string id)
    {
        var existing = await _offerDiscountService.GetByIdOfferDiscountAsync(id);
        if (existing is null)
            return NotFound($"Offer discount with ID '{id}' was not found.");

        await _offerDiscountService.DeleteOfferDiscountAsync(id);
        return Ok("Offer discount was successfully deleted.");
    }

    [HttpPut]
    public async Task<IActionResult> UpdateOfferDiscount(UpdateOfferDiscountDto updateOfferDiscountDto)
    {
        var existing = await _offerDiscountService.GetByIdOfferDiscountAsync(updateOfferDiscountDto.OfferDiscountId);
        if (existing is null)
            return NotFound($"Offer discount with ID '{updateOfferDiscountDto.OfferDiscountId}' was not found.");

        await _offerDiscountService.UpdateOfferDiscountAsync(updateOfferDiscountDto);
        return Ok("Offer discount was successfully updated.");
    }
}
