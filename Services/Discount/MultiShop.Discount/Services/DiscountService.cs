using Dapper;
using MultiShop.Discount.Context;
using MultiShop.Discount.Dtos;

namespace MultiShop.Discount.Services;

public class DiscountService : IDiscountService
{
    private readonly DapperContext _context;

    public DiscountService(DapperContext context)
    {
        _context = context;
    }

    public async Task CreateDiscountCouponAsync(CreateDiscountCouponDto createCouponDto)
    {
        string query = "INSERT INTO Coupons (Code, Rate, IsActive, ValidDate) VALUES (@code, @rate, @isActive, @validDate)";
        var parameters = new DynamicParameters();
        parameters.Add("@code", createCouponDto.Code);
        parameters.Add("@rate", createCouponDto.Rate);
        parameters.Add("@isActive", createCouponDto.IsActive);
        parameters.Add("@validDate", createCouponDto.ValidDate);

        try
        {
            using var connection = _context.CreateConnection();
            await connection.ExecuteAsync(query, parameters);
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An error occurred while creating the discount coupon.", ex);
        }
    }

    public async Task DeleteDiscountCouponAsync(int id)
    {
        string query = "DELETE FROM Coupons WHERE CouponId = @couponId";
        var parameters = new DynamicParameters();
        parameters.Add("@couponId", id);

        try
        {
            using var connection = _context.CreateConnection();
            await connection.ExecuteAsync(query, parameters);
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An error occurred while deleting the discount coupon.", ex);
        }
    }

    public async Task<List<ResultDiscountCouponDto>> GetAllDiscountCouponAsync()
    {
        string query = "SELECT * FROM Coupons";

        try
        {
            using var connection = _context.CreateConnection();
            var coupons = await connection.QueryAsync<ResultDiscountCouponDto>(query);
            return coupons?.ToList() ?? new List<ResultDiscountCouponDto>();
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An error occurred while retrieving the discount coupons.", ex);
        }
    }

    public async Task<GetByIdDiscountCouponDto> GetByIdDiscountCouponAsync(int id)
    {
        string query = "SELECT * FROM Coupons WHERE CouponId = @couponId";
        var parameters = new DynamicParameters();
        parameters.Add("@couponId", id);

        try
        {
            using var connection = _context.CreateConnection();
            var coupon = await connection.QueryFirstOrDefaultAsync<GetByIdDiscountCouponDto>(query, parameters);

            if (coupon == null)
                throw new KeyNotFoundException($"Discount coupon with ID {id} was not found.");

            return coupon;
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An error occurred while retrieving the discount coupon.", ex);
        }
    }

    public async Task<ResultDiscountCouponDto> GetCodeDetailByCodeAsync(string code)
    {
        string query = "SELECT * FROM Coupons WHERE Code = @code";
        var parameters = new DynamicParameters();
        parameters.Add("@code", code);

        try
        {
            using var connection = _context.CreateConnection();
            var couponDetail = await connection.QueryFirstOrDefaultAsync<ResultDiscountCouponDto>(query, parameters);

            if (couponDetail == null)
                throw new KeyNotFoundException($"Discount code {code} was not found.");

            return couponDetail;
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An error occurred while retrieving the discount code details.", ex);
        }
    }

    public async Task<int> GetDiscountCouponCount()
    {
        string query = "SELECT COUNT(*) FROM Coupons";

        try
        {
            using var connection = _context.CreateConnection();
            var couponCount = await connection.QueryFirstOrDefaultAsync<int>(query);
            return couponCount;
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An error occurred while retrieving the discount coupon count.", ex);
        }
    }

    public int GetDiscountCouponRate(string code)
    {
        string query = "SELECT Rate FROM Coupons WHERE Code = @code";
        var parameters = new DynamicParameters();
        parameters.Add("@code", code);

        try
        {
            using var connection = _context.CreateConnection();
            var discountRate = connection.QueryFirstOrDefault<int?>(query, parameters);

            if (discountRate == null)
                throw new KeyNotFoundException($"Rate for discount code {code} was not found.");

            return discountRate.Value;
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An error occurred while retrieving the discount rate.", ex);
        }
    }

    public async Task UpdateDiscountCouponAsync(UpdateDiscountCouponDto updateCouponDto)
    {
        string query = "UPDATE Coupons SET Code = @code, Rate = @rate, IsActive = @isActive, ValidDate = @validDate WHERE CouponId = @couponId";
        var parameters = new DynamicParameters();
        parameters.Add("@code", updateCouponDto.Code);
        parameters.Add("@rate", updateCouponDto.Rate);
        parameters.Add("@isActive", updateCouponDto.IsActive);
        parameters.Add("@validDate", updateCouponDto.ValidDate);
        parameters.Add("@couponId", updateCouponDto.CouponId);

        try
        {
            using var connection = _context.CreateConnection();
            await connection.ExecuteAsync(query, parameters);
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An error occurred while updating the discount coupon.", ex);
        }
    }
}
