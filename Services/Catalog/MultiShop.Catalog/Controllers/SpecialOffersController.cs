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
        var specialOffers = await _specialOfferService.GetAllSpecialOfferAsync();
        return Ok(specialOffers);  
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetSpecialOfferById(string id)
    {
        var specialOffer = await _specialOfferService.GetByIdSpecialOfferAsync(id);
        return specialOffer is null
            ? NotFound($"Special offer with ID '{id}' was not found.")  
            : Ok(specialOffer);
    }

    [HttpPost]
    public async Task<IActionResult> CreateSpecialOffer(CreateSpecialOfferDto createSpecialOfferDto)
    {
        await _specialOfferService.CreateSpecialOfferAsync(createSpecialOfferDto);
        return Ok("Special offer was successfully added.");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSpecialOffer(string id)
    {
        var existing = await _specialOfferService.GetByIdSpecialOfferAsync(id);
        if (existing is null)
            return NotFound($"Special offer with ID '{id}' was not found.");

        await _specialOfferService.DeleteSpecialOfferAsync(id);
        return Ok("Special offer was successfully deleted.");
    }

    [HttpPut]
    public async Task<IActionResult> UpdateSpecialOffer(UpdateSpecialOfferDto updateSpecialOfferDto)
    {
        var existing = await _specialOfferService.GetByIdSpecialOfferAsync(updateSpecialOfferDto.SpecialOfferId);
        if (existing is null)
            return NotFound($"Special offer with ID '{updateSpecialOfferDto.SpecialOfferId}' was not found.");

        await _specialOfferService.UpdateSpecialOfferAsync(updateSpecialOfferDto);
        return Ok("Special offer was successfully updated.");
    }
}
