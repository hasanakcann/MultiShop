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
            throw new ApplicationException("Failed to create the category. Please try again later.", ex);
        }
    }

    public async Task DeleteCategoryAsync(string id)
    {
        try
        {
            var result = await _categoryCollection.DeleteOneAsync(x => x.CategoryId == id);

            if (result.DeletedCount == 0)
                throw new KeyNotFoundException("The category to delete was not found.");
        }
        catch (KeyNotFoundException ex)
        {
            throw new ApplicationException(ex.Message, ex);
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An error occurred while deleting the category. Please try again later.", ex);
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
            throw new ApplicationException("Failed to retrieve categories. Please try again later.", ex);
        }
    }

    public async Task<GetByIdCategoryDto> GetByIdCategoryAsync(string id)
    {
        try
        {
            var category = await _categoryCollection.Find(x => x.CategoryId == id).FirstOrDefaultAsync();

            if (category == null)
                throw new KeyNotFoundException("The requested category was not found.");

            return _mapper.Map<GetByIdCategoryDto>(category);
        }
        catch (KeyNotFoundException ex)
        {
            throw new ApplicationException(ex.Message, ex);
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An error occurred while retrieving the category. Please try again later.", ex);
        }
    }

    public async Task UpdateCategoryAsync(UpdateCategoryDto updateCategoryDto)
    {
        try
        {
            var category = _mapper.Map<Category>(updateCategoryDto);
            var result = await _categoryCollection.ReplaceOneAsync(x => x.CategoryId == updateCategoryDto.CategoryId, category);

            if (result.MatchedCount == 0)
                throw new KeyNotFoundException("The category to update was not found.");
        }
        catch (KeyNotFoundException ex)
        {
            throw new ApplicationException(ex.Message, ex);
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Failed to update the category. Please try again later.", ex);
        }
    }
}
