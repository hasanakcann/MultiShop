using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Discount.Dtos;
using MultiShop.Discount.Services;

namespace MultiShop.Discount.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class DiscountsController : ControllerBase
{
    private readonly IDiscountService _discountService;

    public DiscountsController(IDiscountService discountService)
    {
        _discountService = discountService;
    }

    [HttpGet]
    public async Task<IActionResult> DiscountCouponList()
    {
        var discountList = await _discountService.GetAllDiscountCouponAsync();
        return Ok(discountList);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetDiscountCouponById(int id)
    {
        var discountCoupon = await _discountService.GetByIdDiscountCouponAsync(id);
        return discountCoupon is null
            ? NotFound("Discount coupon not found.")
            : Ok(discountCoupon);
    }

    [HttpGet("by-code/{code}")]
    public async Task<IActionResult> GetCodeDetailByCodeAsync(string code)
    {
        var discountCoupon = await _discountService.GetCodeDetailByCodeAsync(code);
        return discountCoupon is null
            ? NotFound("Discount coupon with the specified code was not found.")
            : Ok(discountCoupon);
    }

    [HttpPost]
    public async Task<IActionResult> CreateDiscountCoupon(CreateDiscountCouponDto createCouponDto)
    {
        await _discountService.CreateDiscountCouponAsync(createCouponDto);
        return Ok("Discount coupon has been created successfully.");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteDiscountCoupon(int id)
    {
        await _discountService.DeleteDiscountCouponAsync(id);
        return Ok("Discount coupon has been deleted successfully.");
    }

    [HttpPut]
    public async Task<IActionResult> UpdateDiscountCoupon(UpdateDiscountCouponDto updateCouponDto)
    {
        await _discountService.UpdateDiscountCouponAsync(updateCouponDto);
        return Ok("Discount coupon has been updated successfully.");
    }

    [HttpGet("rate/{code}")]
    public IActionResult GetDiscountCouponRate(string code)
    {
        var discountRate = _discountService.GetDiscountCouponRate(code);
        return Ok(discountRate);
    }

    [HttpGet("count")]
    public async Task<IActionResult> GetDiscountCouponCount()
    {
        var couponCount = await _discountService.GetDiscountCouponCount();
        return Ok(new { Count = couponCount });
    }
}
