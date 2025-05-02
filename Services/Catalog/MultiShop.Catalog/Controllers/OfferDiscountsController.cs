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
        try
        {
            var offerDiscounts = await _offerDiscountService.GetAllOfferDiscountAsync();
            return Ok(offerDiscounts);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while fetching offer discounts: {ex.Message}");
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetOfferDiscountById(string id)
    {
        try
        {
            var offerDiscount = await _offerDiscountService.GetByIdOfferDiscountAsync(id);
            if (offerDiscount == null)
                return NotFound($"Offer discount with ID '{id}' was not found.");

            return Ok(offerDiscount);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while fetching the offer discount: {ex.Message}");
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateOfferDiscount(CreateOfferDiscountDto createOfferDiscountDto)
    {
        try
        {
            await _offerDiscountService.CreateOfferDiscountAsync(createOfferDiscountDto);
            return Ok("Offer discount was successfully added.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while creating the offer discount: {ex.Message}");
        }
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteOfferDiscount(string id)
    {
        try
        {
            var existing = await _offerDiscountService.GetByIdOfferDiscountAsync(id);
            if (existing == null)
                return NotFound($"Offer discount with ID '{id}' was not found.");

            await _offerDiscountService.DeleteOfferDiscountAsync(id);
            return Ok("Offer discount was successfully deleted.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while deleting the offer discount: {ex.Message}");
        }
    }

    [HttpPut]
    public async Task<IActionResult> UpdateOfferDiscount(UpdateOfferDiscountDto updateOfferDiscountDto)
    {
        try
        {
            var existing = await _offerDiscountService.GetByIdOfferDiscountAsync(updateOfferDiscountDto.OfferDiscountId);
            if (existing == null)
                return NotFound($"Offer discount with ID '{updateOfferDiscountDto.OfferDiscountId}' was not found.");

            await _offerDiscountService.UpdateOfferDiscountAsync(updateOfferDiscountDto);
            return Ok("Offer discount was successfully updated.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while updating the offer discount: {ex.Message}");
        }
    }
}
