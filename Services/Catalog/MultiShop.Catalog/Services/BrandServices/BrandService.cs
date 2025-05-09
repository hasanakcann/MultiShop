using AutoMapper;
using MongoDB.Driver;
using MultiShop.Catalog.Dtos.BrandDtos;
using MultiShop.Catalog.Entities;
using MultiShop.Catalog.Settings;

namespace MultiShop.Catalog.Services.BrandServices;

public class BrandService : IBrandService
{
    private readonly IMongoCollection<Brand> _brandCollection;
    private readonly IMapper _mapper;

    public BrandService(IMapper mapper, IDatabaseSettings _databaseSettings)
    {
        var client = new MongoClient(_databaseSettings.ConnectionString);
        var database = client.GetDatabase(_databaseSettings.DatabaseName);
        _brandCollection = database.GetCollection<Brand>(_databaseSettings.BrandCollectionName);
        _mapper = mapper;
    }

    public async Task CreateBrandAsync(CreateBrandDto createBrandDto)
    {
        try
        {
            var brand = _mapper.Map<Brand>(createBrandDto);
            await _brandCollection.InsertOneAsync(brand);
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An error occurred while creating the brand.", ex);
        }
    }

    public async Task DeleteBrandAsync(string id)
    {
        try
        {
            var result = await _brandCollection.DeleteOneAsync(x => x.BrandId == id);

            if (result.DeletedCount == 0)
                throw new KeyNotFoundException("The brand you are trying to delete was not found.");
        }
        catch (KeyNotFoundException ex)
        {
            throw new ApplicationException(ex.Message, ex);
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An error occurred while deleting the brand.", ex);
        }
    }

    public async Task<List<ResultBrandDto>> GetAllBrandAsync()
    {
        try
        {
            var brands = await _brandCollection.Find(x => true).ToListAsync();
            return _mapper.Map<List<ResultBrandDto>>(brands);
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An error occurred while retrieving the brand list.", ex);
        }
    }

    public async Task<GetByIdBrandDto> GetByIdBrandAsync(string id)
    {
        try
        {
            var brand = await _brandCollection.Find(x => x.BrandId == id).FirstOrDefaultAsync();

            if (brand == null)
                throw new KeyNotFoundException("The brand you are looking for was not found.");

            return _mapper.Map<GetByIdBrandDto>(brand);
        }
        catch (KeyNotFoundException ex)
        {
            throw new ApplicationException(ex.Message, ex);
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An error occurred while retrieving the brand.", ex);
        }
    }

    public async Task UpdateBrandAsync(UpdateBrandDto updateBrandDto)
    {
        try
        {
            var brand = _mapper.Map<Brand>(updateBrandDto);
            var result = await _brandCollection.ReplaceOneAsync(
                x => x.BrandId == updateBrandDto.BrandId,
                brand
            );

            if (result.MatchedCount == 0)
                throw new KeyNotFoundException("The brand you are trying to update was not found.");
        }
        catch (KeyNotFoundException ex)
        {
            throw new ApplicationException(ex.Message, ex);
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An error occurred while updating the brand.", ex);
        }
    }
}
