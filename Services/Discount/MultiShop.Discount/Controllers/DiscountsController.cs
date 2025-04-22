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
        try
        {
            var discountList = await _discountService.GetAllDiscountCouponAsync();
            return Ok(discountList);
        }
        catch (Exception)
        {
            return StatusCode(500, "An error occurred while retrieving the discount coupons.");
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetDiscountCouponById(int id)
    {
        try
        {
            var discountCoupon = await _discountService.GetByIdDiscountCouponAsync(id);
            if (discountCoupon == null)
                return NotFound("Discount coupon not found.");

            return Ok(discountCoupon);
        }
        catch (Exception)
        {
            return StatusCode(500, "An error occurred while retrieving the discount coupon.");
        }
    }

    [HttpGet("GetCodeDetailByCodeAsync")]
    public async Task<IActionResult> GetCodeDetailByCodeAsync(string code)
    {
        try
        {
            var discountCoupon = await _discountService.GetCodeDetailByCodeAsync(code);
            if (discountCoupon == null)
                return NotFound("Discount coupon with the specified code was not found.");

            return Ok(discountCoupon);
        }
        catch (Exception)
        {
            return StatusCode(500, "An error occurred while retrieving the discount code details.");
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateDiscountCoupon(CreateDiscountCouponDto createCouponDto)
    {
        try
        {
            await _discountService.CreateDiscountCouponAsync(createCouponDto);
            return Ok("Discount coupon has been created successfully.");
        }
        catch (Exception)
        {
            return StatusCode(500, "An error occurred while creating the discount coupon.");
        }
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteDiscountCoupon(int id)
    {
        try
        {
            await _discountService.DeleteDiscountCouponAsync(id);
            return Ok("Discount coupon has been deleted successfully.");
        }
        catch (Exception)
        {
            return StatusCode(500, "An error occurred while deleting the discount coupon.");
        }
    }

    [HttpPut]
    public async Task<IActionResult> UpdateDiscountCoupon(UpdateDiscountCouponDto updateCouponDto)
    {
        try
        {
            await _discountService.UpdateDiscountCouponAsync(updateCouponDto);
            return Ok("Discount coupon has been updated successfully.");
        }
        catch (Exception)
        {
            return StatusCode(500, "An error occurred while updating the discount coupon.");
        }
    }

    [HttpGet("GetDiscountCouponRate")]
    public IActionResult GetDiscountCouponRate(string code)
    {
        try
        {
            var discountRate = _discountService.GetDiscountCouponRate(code);
            return Ok(discountRate);
        }
        catch (Exception)
        {
            return StatusCode(500, "An error occurred while retrieving the discount rate.");
        }
    }

    [HttpGet("GetDiscountCouponCount")]
    public async Task<IActionResult> GetDiscountCouponCount()
    {
        try
        {
            var couponCount = await _discountService.GetDiscountCouponCount();
            return Ok($"Total discount coupons: {couponCount}");
        }
        catch (Exception)
        {
            return StatusCode(500, "An error occurred while retrieving the discount coupon count.");
        }
    }
}
