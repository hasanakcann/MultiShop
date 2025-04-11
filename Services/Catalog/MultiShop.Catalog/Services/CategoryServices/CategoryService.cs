using AutoMapper;
using MongoDB.Driver;
using MultiShop.Catalog.Dtos.CategoryDtos;
using MultiShop.Catalog.Entities;
using MultiShop.Catalog.Settings;

namespace MultiShop.Catalog.Services.CategoryServices;

public class CategoryService : ICategoryService
{
    private readonly IMongoCollection<Category> _categoryCollection;
    private readonly IMapper _mapper;

    public CategoryService(IMapper mapper, IDatabaseSettings _databaseSettings)
    {
        var client = new MongoClient(_databaseSettings.ConnectionString);
        var database = client.GetDatabase(_databaseSettings.DatabaseName);
        _categoryCollection = database.GetCollection<Category>(_databaseSettings.CategoryCollectionName);
        _mapper = mapper;
    }

    public async Task CreateCategoryAsync(CreateCategoryDto createCategoryDto)
    {
        try
        {
            var category = _mapper.Map<Category>(createCategoryDto);
            await _categoryCollection.InsertOneAsync(category);
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An error occurred while creating the category.", ex);
        }
    }

    public async Task DeleteCategoryAsync(string id)
    {
        try
        {
            var result = await _categoryCollection.DeleteOneAsync(x => x.CategoryId == id);
            if (result.DeletedCount == 0)
            {
                throw new KeyNotFoundException("Category not found.");
            }
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An error occurred while deleting the category.", ex);
        }
    }

    public async Task<List<ResultCategoryDto>> GetAllCategoryAsync()
    {
        try
        {
            var categories = await _categoryCollection.Find(x => true).ToListAsync();
            return _mapper.Map<List<ResultCategoryDto>>(categories);
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An error occurred while retrieving categories.", ex);
        }
    }

    public async Task<GetByIdCategoryDto> GetByIdCategoryAsync(string id)
    {
        try
        {
            var category = await _categoryCollection.Find(x => x.CategoryId == id).FirstOrDefaultAsync();
            if (category == null)
                throw new KeyNotFoundException("Category not found.");

            return _mapper.Map<GetByIdCategoryDto>(category);
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An error occurred while retrieving the category.", ex);
        }
    }

    public async Task UpdateCategoryAsync(UpdateCategoryDto updateCategoryDto)
    {
        try
        {
            var category = _mapper.Map<Category>(updateCategoryDto);
            var result = await _categoryCollection.FindOneAndReplaceAsync(x => x.CategoryId == updateCategoryDto.CategoryId, category);

            if (result == null)
                throw new KeyNotFoundException("Category to update not found.");
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An error occurred while updating the category.", ex);
        }
    }
}
