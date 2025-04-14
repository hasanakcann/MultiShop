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
        string query = "Insert Into Coupons (Code, Rate, IsActive, ValidDate) values (@code, @rate, @isActive, @validDate)";
        var parameters = new DynamicParameters();
        parameters.Add("@code", createCouponDto.Code);
        parameters.Add("@rate", createCouponDto.Rate);
        parameters.Add("@isActive", createCouponDto.IsActive);
        parameters.Add("@validDate", createCouponDto.ValidDate);

        try
        {
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }
        catch (Exception)
        {
            throw new Exception("An error occurred while creating the discount coupon.");
        }
    }

    public async Task DeleteDiscountCouponAsync(int id)
    {
        string query = "Delete From Coupons where CouponId=@couponId";
        var parameters = new DynamicParameters();
        parameters.Add("@couponId", id);

        try
        {
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }
        catch (Exception)
        {
            throw new Exception("An error occurred while deleting the discount coupon.");
        }
    }

    public async Task<List<ResultDiscountCouponDto>> GetAllDiscountCouponAsync()
    {
        string query = "Select * From Coupons";

        try
        {
            using (var connection = _context.CreateConnection())
            {
                var coupons = await connection.QueryAsync<ResultDiscountCouponDto>(query);
                return coupons?.ToList() ?? new List<ResultDiscountCouponDto>();
            }
        }
        catch (Exception)
        {
            throw new Exception("An error occurred while retrieving the discount coupons.");
        }
    }

    public async Task<GetByIdDiscountCouponDto> GetByIdDiscountCouponAsync(int id)
    {
        string query = "Select * From Coupons Where CouponId=@couponId";
        var parameters = new DynamicParameters();
        parameters.Add("@couponId", id);

        try
        {
            using (var connection = _context.CreateConnection())
            {
                var coupon = await connection.QueryFirstOrDefaultAsync<GetByIdDiscountCouponDto>(query, parameters);

                if (coupon == null)
                    throw new Exception("Discount coupon not found.");

                return coupon;
            }
        }
        catch (Exception)
        {
            throw new Exception("An error occurred while retrieving the discount coupon.");
        }
    }

    public async Task<ResultDiscountCouponDto> GetCodeDetailByCodeAsync(string code)
    {
        string query = "Select * From Coupons Where Code=@code";
        var parameters = new DynamicParameters();
        parameters.Add("@code", code);

        try
        {
            using (var connection = _context.CreateConnection())
            {
                var couponDetail = await connection.QueryFirstOrDefaultAsync<ResultDiscountCouponDto>(query, parameters);

                if (couponDetail == null)
                    throw new Exception("Discount code not found.");

                return couponDetail;
            }
        }
        catch (Exception)
        {
            throw new Exception("An error occurred while retrieving the discount code details.");
        }
    }

    public async Task<int> GetDiscountCouponCount()
    {
        string query = "Select Count(*) From Coupons";

        try
        {
            using (var connection = _context.CreateConnection())
            {
                var couponCount = await connection.QueryFirstOrDefaultAsync<int>(query);
                return couponCount;
            }
        }
        catch (Exception)
        {
            throw new Exception("An error occurred while retrieving the discount coupon count.");
        }
    }

    public int GetDiscountCouponRate(string code)
    {
        string query = "Select Rate From Coupons Where Code=@code";
        var parameters = new DynamicParameters();
        parameters.Add("@code", code);

        try
        {
            using (var connection = _context.CreateConnection())
            {
                var discountRate = connection.QueryFirstOrDefault<int?>(query, parameters);

                if (discountRate == null)
                    throw new Exception("Rate for the given discount code was not found.");

                return discountRate.Value;
            }
        }
        catch (Exception)
        {
            throw new Exception("An error occurred while retrieving the discount rate.");
        }
    }

    public async Task UpdateDiscountCouponAsync(UpdateDiscountCouponDto updateCouponDto)
    {
        string query = "Update Coupons Set Code=@code, Rate=@rate, IsActive=@isActive, ValidDate=@validDate where CouponId=@couponId";
        var parameters = new DynamicParameters();
        parameters.Add("@code", updateCouponDto.Code);
        parameters.Add("@rate", updateCouponDto.Rate);
        parameters.Add("@isActive", updateCouponDto.IsActive);
        parameters.Add("@validDate", updateCouponDto.ValidDate);
        parameters.Add("@couponId", updateCouponDto.CouponId);

        try
        {
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }
        catch (Exception)
        {
            throw new Exception("An error occurred while updating the discount coupon.");
        }
    }
}
