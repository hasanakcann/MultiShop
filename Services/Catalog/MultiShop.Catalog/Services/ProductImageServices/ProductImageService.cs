using AutoMapper;
using MongoDB.Driver;
using MultiShop.Catalog.Dtos.ProductImageDtos;
using MultiShop.Catalog.Entities;
using MultiShop.Catalog.Settings;

namespace MultiShop.Catalog.Services.ProductImageServices;

public class ProductImageService : IProductImageService
{
    private readonly IMongoCollection<ProductImage> _productImageCollection;
    private readonly IMapper _mapper;

    public ProductImageService(IMapper mapper, IDatabaseSettings _databaseSettings)
    {
        var client = new MongoClient(_databaseSettings.ConnectionString);
        var database = client.GetDatabase(_databaseSettings.DatabaseName);
        _productImageCollection = database.GetCollection<ProductImage>(_databaseSettings.ProductImageCollectionName);
        _mapper = mapper;
    }

    public async Task CreateProductImageAsync(CreateProductImageDto createProductImageDto)
    {
        try
        {
            var productImage = _mapper.Map<ProductImage>(createProductImageDto);
            await _productImageCollection.InsertOneAsync(productImage);
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Failed to create product image.", ex);
        }
    }

    public async Task DeleteProductImageAsync(string id)
    {
        try
        {
            var result = await _productImageCollection.DeleteOneAsync(x => x.ProductImageId == id);
            if (result.DeletedCount == 0)
                throw new KeyNotFoundException("Product image not found.");
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Failed to delete product image.", ex);
        }
    }

    public async Task<List<ResultProductImageDto>> GetAllProductImageAsync()
    {
        try
        {
            var productImages = await _productImageCollection.Find(_ => true).ToListAsync();
            return _mapper.Map<List<ResultProductImageDto>>(productImages);
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Failed to retrieve product images.", ex);
        }
    }

    public async Task<GetByIdProductImageDto> GetByIdProductImageAsync(string id)
    {
        try
        {
            var productImage = await _productImageCollection.Find(x => x.ProductImageId == id).FirstOrDefaultAsync();
            if (productImage == null)
                throw new KeyNotFoundException("Product image not found.");

            return _mapper.Map<GetByIdProductImageDto>(productImage);
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Failed to retrieve product image by ID.", ex);
        }
    }

    public async Task UpdateProductImageAsync(UpdateProductImageDto updateProductImageDto)
    {
        try
        {
            var productImage = _mapper.Map<ProductImage>(updateProductImageDto);
            var result = await _productImageCollection.FindOneAndReplaceAsync(
                x => x.ProductImageId == updateProductImageDto.ProductImageId,
                productImage
            );

            if (result == null)
                throw new KeyNotFoundException("Product image to update not found.");
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Failed to update product image.", ex);
        }
    }

    public async Task<GetByIdProductImageDto> GetByProductIdProductImageAsync(string productId)
    {
        try
        {
            var productImage = await _productImageCollection.Find(x => x.ProductId == productId).FirstOrDefaultAsync();
            if (productImage == null)
                throw new KeyNotFoundException("Product image not found for the given product ID.");

            return _mapper.Map<GetByIdProductImageDto>(productImage);
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Failed to retrieve product image by product ID.", ex);
        }
    }
}
