using AutoMapper;
using MongoDB.Driver;
using MultiShop.Catalog.Dtos.FeatureDtos;
using MultiShop.Catalog.Entities;
using MultiShop.Catalog.Settings;

namespace MultiShop.Catalog.Services.FeatureServices;

public class FeatureService : IFeatureService
{
    private readonly IMongoCollection<Feature> _featureCollection;
    private readonly IMapper _mapper;

    public FeatureService(IMapper mapper, IDatabaseSettings _databaseSettings)
    {
        var client = new MongoClient(_databaseSettings.ConnectionString);
        var database = client.GetDatabase(_databaseSettings.DatabaseName);
        _featureCollection = database.GetCollection<Feature>(_databaseSettings.FeatureCollectionName);
        _mapper = mapper;
    }

    public async Task CreateFeatureAsync(CreateFeatureDto createFeatureDto)
    {
        try
        {
            var feature = _mapper.Map<Feature>(createFeatureDto);
            await _featureCollection.InsertOneAsync(feature);
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Failed to create feature. Please try again later.", ex);
        }
    }

    public async Task DeleteFeatureAsync(string id)
    {
        try
        {
            var result = await _featureCollection.DeleteOneAsync(x => x.FeatureId == id);

            if (result.DeletedCount == 0)
                throw new KeyNotFoundException("The feature to delete was not found.");
        }
        catch (KeyNotFoundException ex)
        {
            throw new ApplicationException(ex.Message, ex);
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An error occurred while deleting the feature. Please try again later.", ex);
        }
    }

    public async Task<List<ResultFeatureDto>> GetAllFeatureAsync()
    {
        try
        {
            var features = await _featureCollection.Find(x => true).ToListAsync();
            return _mapper.Map<List<ResultFeatureDto>>(features);
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Failed to retrieve feature list. Please try again later.", ex);
        }
    }

    public async Task<GetByIdFeatureDto> GetByIdFeatureAsync(string id)
    {
        try
        {
            var feature = await _featureCollection.Find(x => x.FeatureId == id).FirstOrDefaultAsync();

            if (feature == null)
                throw new KeyNotFoundException("The requested feature was not found.");

            return _mapper.Map<GetByIdFeatureDto>(feature);
        }
        catch (KeyNotFoundException ex)
        {
            throw new ApplicationException(ex.Message, ex);
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An error occurred while retrieving the feature. Please try again later.", ex);
        }
    }

    public async Task UpdateFeatureAsync(UpdateFeatureDto updateFeatureDto)
    {
        try
        {
            var feature = _mapper.Map<Feature>(updateFeatureDto);
            var result = await _featureCollection.ReplaceOneAsync(
                x => x.FeatureId == updateFeatureDto.FeatureId,
                feature
            );

            if (result.MatchedCount == 0)
                throw new KeyNotFoundException("The feature to update was not found.");
        }
        catch (KeyNotFoundException ex)
        {
            throw new ApplicationException(ex.Message, ex);
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Failed to update feature. Please try again later.", ex);
        }
    }
}
