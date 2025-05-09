using MongoDB.Bson;
using MongoDB.Driver;
using MultiShop.Catalog.Entities;
using MultiShop.Catalog.Settings;

namespace MultiShop.Catalog.Services.StatisticServices;

public class StatisticService : IStatisticService
{
    private readonly IMongoCollection<Category> _categoryCollection;
    private readonly IMongoCollection<Product> _productCollection;
    private readonly IMongoCollection<Brand> _brandCollection;

    public StatisticService(IDatabaseSettings _databaseSettings)
    {
        var client = new MongoClient(_databaseSettings.ConnectionString);
        var database = client.GetDatabase(_databaseSettings.DatabaseName);
        _categoryCollection = database.GetCollection<Category>(_databaseSettings.CategoryCollectionName);
        _productCollection = database.GetCollection<Product>(_databaseSettings.ProductCollectionName);
        _brandCollection = database.GetCollection<Brand>(_databaseSettings.BrandCollectionName);
    }

    public async Task<long> GetBrandCount()
    {
        try
        {
            return await _brandCollection.CountDocumentsAsync(FilterDefinition<Brand>.Empty);
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An error occurred while retrieving brand count.", ex);
        }
    }

    public async Task<long> GetCategoryCount()
    {
        try
        {
            return await _categoryCollection.CountDocumentsAsync(FilterDefinition<Category>.Empty);
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An error occurred while retrieving category count.", ex);
        }
    }

    public async Task<decimal> GetProductAveragePrice()
    {
        try
        {
            var pipeline = new BsonDocument[]
            {
                new BsonDocument("$group", new BsonDocument
                {
                    { "_id", null },
                    { "averagePrice", new BsonDocument("$avg", "$ProductPrice") }
                })
            };
            var result = await _productCollection.AggregateAsync<BsonDocument>(pipeline);
            var price = result.FirstOrDefault()?.GetValue("averagePrice", decimal.Zero).AsDecimal ?? decimal.Zero;
            return price;
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An error occurred while calculating the average product price.", ex);
        }
    }

    public async Task<long> GetProductCount()
    {
        try
        {
            return await _productCollection.CountDocumentsAsync(FilterDefinition<Product>.Empty);
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An error occurred while retrieving product count.", ex);
        }
    }

    public async Task<string> GetMaxPriceProductName()
    {
        try
        {
            var filter = Builders<Product>.Filter.Empty;
            var sort = Builders<Product>.Sort.Descending(x => x.ProductPrice);
            var projection = Builders<Product>.Projection.Include(y => y.ProductName).Exclude("ProductId");
            var product = await _productCollection.Find(filter).Sort(sort).Project(projection).FirstOrDefaultAsync();

            if (product == null)
                throw new KeyNotFoundException("No product found with the maximum price.");

            return product.GetValue("ProductName").AsString;
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An error occurred while retrieving the product with the highest price.", ex);
        }
    }

    public async Task<string> GetMinPriceProductName()
    {
        try
        {
            var filter = Builders<Product>.Filter.Empty;
            var sort = Builders<Product>.Sort.Ascending(x => x.ProductPrice);
            var projection = Builders<Product>.Projection.Include(y => y.ProductName).Exclude("ProductId");
            var product = await _productCollection.Find(filter).Sort(sort).Project(projection).FirstOrDefaultAsync();

            if (product == null)
                throw new KeyNotFoundException("No product found with the minimum price.");

            return product.GetValue("ProductName").AsString;
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An error occurred while retrieving the product with the lowest price.", ex);
        }
    }
}
