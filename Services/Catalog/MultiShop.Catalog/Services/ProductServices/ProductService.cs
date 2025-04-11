using AutoMapper;
using MongoDB.Driver;
using MultiShop.Catalog.Dtos.ProductDtos;
using MultiShop.Catalog.Entities;
using MultiShop.Catalog.Settings;

namespace MultiShop.Catalog.Services.ProductServices;

public class ProductService : IProductService
{
    private readonly IMapper _mapper;
    private readonly IMongoCollection<Product> _productCollection;
    private readonly IMongoCollection<Category> _categoryCollection;

    public ProductService(IMapper mapper, IDatabaseSettings _databaseSettings)
    {
        var client = new MongoClient(_databaseSettings.ConnectionString);
        var database = client.GetDatabase(_databaseSettings.DatabaseName);
        _mapper = mapper;
        _productCollection = database.GetCollection<Product>(_databaseSettings.ProductCollectionName);
        _categoryCollection = database.GetCollection<Category>(_databaseSettings.CategoryCollectionName);
    }

    public async Task CreateProductAsync(CreateProductDto createProductDto)
    {
        try
        {
            var product = _mapper.Map<Product>(createProductDto);
            await _productCollection.InsertOneAsync(product);
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An error occurred while creating the product.", ex);
        }
    }

    public async Task DeleteProductAsync(string id)
    {
        try
        {
            var result = await _productCollection.DeleteOneAsync(x => x.ProductId == id);
            if (result.DeletedCount == 0)
                throw new KeyNotFoundException("Product not found.");
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An error occurred while deleting the product.", ex);
        }
    }

    public async Task<List<ResultProductDto>> GetAllProductAsync()
    {
        try
        {
            var products = await _productCollection.Find(x => true).ToListAsync();
            return _mapper.Map<List<ResultProductDto>>(products);
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An error occurred while retrieving all products.", ex);
        }
    }

    public async Task<GetByIdProductDto> GetByIdProductAsync(string id)
    {
        try
        {
            var product = await _productCollection.Find(x => x.ProductId == id).FirstOrDefaultAsync();
            if (product == null)
                throw new KeyNotFoundException("Product not found.");

            return _mapper.Map<GetByIdProductDto>(product);
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An error occurred while retrieving the product by ID.", ex);
        }
    }

    public async Task<List<ResultProductsWithCategoryDto>> GetProductsWithCategoryAsync()
    {
        try
        {
            var products = await _productCollection.Find(x => true).ToListAsync();
            foreach (var product in products)
            {
                product.Category = await _categoryCollection.Find(x => x.CategoryId == product.CategoryId).FirstOrDefaultAsync() ?? throw new KeyNotFoundException("Category not found for product.");
            }

            return _mapper.Map<List<ResultProductsWithCategoryDto>>(products);
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An error occurred while retrieving products with categories.", ex);
        }
    }

    public async Task UpdateProductAsync(UpdateProductDto updateProductDto)
    {
        try
        {
            var product = _mapper.Map<Product>(updateProductDto);
            var result = await _productCollection.FindOneAndReplaceAsync(x => x.ProductId == updateProductDto.ProductId, product);

            if (result == null)
                throw new KeyNotFoundException("Product to update not found.");
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An error occurred while updating the product.", ex);
        }
    }

    public async Task<List<ResultProductsWithCategoryDto>> GetProductsWithCategoryByCategoryIdAsync(string categoryId)
    {
        try
        {
            var products = await _productCollection.Find(x => x.CategoryId == categoryId).ToListAsync();
            foreach (var product in products)
            {
                product.Category = await _categoryCollection.Find(x => x.CategoryId == product.CategoryId).FirstOrDefaultAsync() ?? throw new KeyNotFoundException("Category not found for product.");
            }

            return _mapper.Map<List<ResultProductsWithCategoryDto>>(products);
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An error occurred while retrieving products by category.", ex);
        }
    }
}
