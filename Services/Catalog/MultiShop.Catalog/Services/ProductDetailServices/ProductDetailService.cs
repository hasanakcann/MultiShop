using AutoMapper;
using MongoDB.Driver;
using MultiShop.Catalog.Dtos.ProductDetailDtos;
using MultiShop.Catalog.Entities;
using MultiShop.Catalog.Settings;

namespace MultiShop.Catalog.Services.ProductDetailServices;

public class ProductDetailService : IProductDetailService
{
    private readonly IMongoCollection<ProductDetail> _productDetailCollection;
    private readonly IMapper _mapper;

    public ProductDetailService(IMapper mapper, IDatabaseSettings _databaseSettings)
    {
        var client = new MongoClient(_databaseSettings.ConnectionString);
        var database = client.GetDatabase(_databaseSettings.DatabaseName);
        _productDetailCollection = database.GetCollection<ProductDetail>(_databaseSettings.ProductDetailCollectionName);
        _mapper = mapper;
    }

    public async Task CreateProductDetailAsync(CreateProductDetailDto createProductDetailDto)
    {
        try
        {
            var productDetail = _mapper.Map<ProductDetail>(createProductDetailDto);
            await _productDetailCollection.InsertOneAsync(productDetail);
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Failed to create product detail.", ex);
        }
    }

    public async Task DeleteProductDetailAsync(string id)
    {
        try
        {
            var result = await _productDetailCollection.DeleteOneAsync(x => x.ProductDetailId == id);
            if (result.DeletedCount == 0)
                throw new KeyNotFoundException("Product detail not found.");
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Failed to delete product detail.", ex);
        }
    }

    public async Task<List<ResultProductDetailDto>> GetAllProductDetailAsync()
    {
        try
        {
            var productDetails = await _productDetailCollection.Find(_ => true).ToListAsync();
            return _mapper.Map<List<ResultProductDetailDto>>(productDetails);
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Failed to retrieve all product details.", ex);
        }
    }

    public async Task<GetByIdProductDetailDto> GetByIdProductDetailAsync(string id)
    {
        try
        {
            var productDetail = await _productDetailCollection.Find(x => x.ProductDetailId == id).FirstOrDefaultAsync();
            if (productDetail == null)
                throw new KeyNotFoundException("Product detail not found.");

            return _mapper.Map<GetByIdProductDetailDto>(productDetail);
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Failed to retrieve product detail by ID.", ex);
        }
    }

    public async Task UpdateProductDetailAsync(UpdateProductDetailDto updateProductDetailDto)
    {
        try
        {
            var productDetail = _mapper.Map<ProductDetail>(updateProductDetailDto);
            var result = await _productDetailCollection.FindOneAndReplaceAsync(
                x => x.ProductDetailId == updateProductDetailDto.ProductDetailId,
                productDetail
            );

            if (result == null)
                throw new KeyNotFoundException("Product detail to update not found.");
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Failed to update product detail.", ex);
        }
    }

    public async Task<GetByIdProductDetailDto> GetByProductIdProductDetailAsync(string productId)
    {
        try
        {
            var productDetail = await _productDetailCollection.Find(x => x.ProductId == productId).FirstOrDefaultAsync();
            if (productDetail == null)
                throw new KeyNotFoundException("Product detail not found for the given product ID.");

            return _mapper.Map<GetByIdProductDetailDto>(productDetail);
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Failed to retrieve product detail by product ID.", ex);
        }
    }
}
