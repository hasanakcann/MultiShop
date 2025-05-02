using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Catalog.Dtos.SpecialOfferDtos;
using MultiShop.Catalog.Services.SpecialOfferServices;

namespace MultiShop.Catalog.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class SpecialOffersController : ControllerBase
{
    private readonly ISpecialOfferService _specialOfferService;

    public SpecialOffersController(ISpecialOfferService specialOfferService)
    {
        _specialOfferService = specialOfferService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllSpecialOfferList()
    {
        try
        {
            var specialOffers = await _specialOfferService.GetAllSpecialOfferAsync();
            return Ok(specialOffers);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while fetching special offers: {ex.Message}");
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetSpecialOfferById(string id)
    {
        try
        {
            var specialOffer = await _specialOfferService.GetByIdSpecialOfferAsync(id);
            if (specialOffer == null)
                return NotFound($"Special offer with ID '{id}' was not found.");

            return Ok(specialOffer);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while fetching the special offer: {ex.Message}");
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateSpecialOffer(CreateSpecialOfferDto createSpecialOfferDto)
    {
        try
        {
            await _specialOfferService.CreateSpecialOfferAsync(createSpecialOfferDto);
            return Ok("Special offer was successfully added.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while creating the special offer: {ex.Message}");
        }
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteSpecialOffer(string id)
    {
        try
        {
            var existing = await _specialOfferService.GetByIdSpecialOfferAsync(id);
            if (existing == null)
                return NotFound($"Special offer with ID '{id}' was not found.");

            await _specialOfferService.DeleteSpecialOfferAsync(id);
            return Ok("Special offer was successfully deleted.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while deleting the special offer: {ex.Message}");
        }
    }

    [HttpPut]
    public async Task<IActionResult> UpdateSpecialOffer(UpdateSpecialOfferDto updateSpecialOfferDto)
    {
        try
        {
            var existing = await _specialOfferService.GetByIdSpecialOfferAsync(updateSpecialOfferDto.SpecialOfferId);
            if (existing == null)
                return NotFound($"Special offer with ID '{updateSpecialOfferDto.SpecialOfferId}' was not found.");

            await _specialOfferService.UpdateSpecialOfferAsync(updateSpecialOfferDto);
            return Ok("Special offer was successfully updated.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while updating the special offer: {ex.Message}");
        }
    }
}
