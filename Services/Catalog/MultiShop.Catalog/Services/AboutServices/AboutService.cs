using AutoMapper;
using MongoDB.Driver;
using MultiShop.Catalog.Dtos.AboutDtos;
using MultiShop.Catalog.Entities;
using MultiShop.Catalog.Settings;

namespace MultiShop.Catalog.Services.AboutServices;

public class AboutService : IAboutService
{
    private readonly IMongoCollection<About> _aboutCollection;
    private readonly IMapper _mapper;

    public AboutService(IMapper mapper, IDatabaseSettings _databaseSettings)
    {
        var client = new MongoClient(_databaseSettings.ConnectionString);
        var database = client.GetDatabase(_databaseSettings.DatabaseName);
        _aboutCollection = database.GetCollection<About>(_databaseSettings.AboutCollectionName);
        _mapper = mapper;
    }

    public async Task CreateAboutAsync(CreateAboutDto createAboutDto)
    {
        try
        {
            var about = _mapper.Map<About>(createAboutDto);
            await _aboutCollection.InsertOneAsync(about);
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An error occurred while creating the about entry.", ex);
        }
    }

    public async Task DeleteAboutAsync(string id)
    {
        try
        {
            var result = await _aboutCollection.DeleteOneAsync(x => x.AboutId == id);

            if (result.DeletedCount == 0)
                throw new KeyNotFoundException("The about entry you are trying to delete was not found.");
        }
        catch (KeyNotFoundException ex)
        {
            throw new ApplicationException(ex.Message, ex);
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An error occurred while deleting the about entry.", ex);
        }
    }

    public async Task<List<ResultAboutDto>> GetAllAboutAsync()
    {
        try
        {
            var abouts = await _aboutCollection.Find(x => true).ToListAsync();
            return _mapper.Map<List<ResultAboutDto>>(abouts);
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An error occurred while retrieving about entries.", ex);
        }
    }

    public async Task<GetByIdAboutDto> GetByIdAboutAsync(string id)
    {
        try
        {
            var about = await _aboutCollection.Find(x => x.AboutId == id).FirstOrDefaultAsync();

            if (about == null)
                throw new KeyNotFoundException("The requested about entry was not found.");

            return _mapper.Map<GetByIdAboutDto>(about);
        }
        catch (KeyNotFoundException ex)
        {
            throw new ApplicationException(ex.Message, ex);
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An error occurred while retrieving the about entry.", ex);
        }
    }

    public async Task UpdateAboutAsync(UpdateAboutDto updateAboutDto)
    {
        try
        {
            var about = _mapper.Map<About>(updateAboutDto);
            var result = await _aboutCollection.ReplaceOneAsync(
                x => x.AboutId == updateAboutDto.AboutId,
                about
            );

            if (result.MatchedCount == 0)
                throw new KeyNotFoundException("The about entry to update was not found.");
        }
        catch (KeyNotFoundException ex)
        {
            throw new ApplicationException(ex.Message, ex);
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An error occurred while updating the about entry.", ex);
        }
    }
}
