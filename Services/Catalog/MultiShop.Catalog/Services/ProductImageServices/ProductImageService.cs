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
            var image = _mapper.Map<ProductImage>(createProductImageDto);
            await _productImageCollection.InsertOneAsync(image);
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An error occurred while creating product image.", ex);
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
            throw new ApplicationException("An error occurred while deleting product image.", ex);
        }
    }

    public async Task<List<ResultProductImageDto>> GetAllProductImageAsync()
    {
        try
        {
            var images = await _productImageCollection.Find(x => true).ToListAsync();
            return _mapper.Map<List<ResultProductImageDto>>(images);
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An error occurred while retrieving product images.", ex);
        }
    }

    public async Task<GetByIdProductImageDto> GetByIdProductImageAsync(string id)
    {
        try
        {
            var image = await _productImageCollection.Find(x => x.ProductImageId == id).FirstOrDefaultAsync();
            if (image == null)
                throw new KeyNotFoundException("Product image not found.");

            return _mapper.Map<GetByIdProductImageDto>(image);
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An error occurred while retrieving product image by ID.", ex);
        }
    }

    public async Task UpdateProductImageAsync(UpdateProductImageDto updateProductImageDto)
    {
        try
        {
            var image = _mapper.Map<ProductImage>(updateProductImageDto);
            var result = await _productImageCollection.FindOneAndReplaceAsync(x => x.ProductImageId == updateProductImageDto.ProductImageId, image);

            if (result == null)
                throw new KeyNotFoundException("Product image to update not found.");
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An error occurred while updating product image.", ex);
        }
    }

    public async Task<GetByIdProductImageDto> GetByProductIdProductImageAsync(string id)
    {
        try
        {
            var image = await _productImageCollection.Find(x => x.ProductId == id).FirstOrDefaultAsync();
            if (image == null)
                throw new KeyNotFoundException("Product image not found for the given product ID.");

            return _mapper.Map<GetByIdProductImageDto>(image);
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An error occurred while retrieving product image by product ID.", ex);
        }
    }
}
