using AutoMapper;
using MongoDB.Driver;
using MultiShop.Catalog.Dtos.FeatureSliderDtos;
using MultiShop.Catalog.Entities;
using MultiShop.Catalog.Settings;

namespace MultiShop.Catalog.Services.FeatureSliderServices;

public class FeatureSliderService : IFeatureSliderService
{
    private readonly IMongoCollection<FeatureSlider> _featureSliderCollection;
    private readonly IMapper _mapper;

    public FeatureSliderService(IMapper mapper, IDatabaseSettings _databaseSettings)
    {
        var client = new MongoClient(_databaseSettings.ConnectionString);
        var database = client.GetDatabase(_databaseSettings.DatabaseName);
        _featureSliderCollection = database.GetCollection<FeatureSlider>(_databaseSettings.FeatureSliderCollectionName);
        _mapper = mapper;
    }

    public async Task CreateFeatureSliderAsync(CreateFeatureSliderDto createFeatureSliderDto)
    {
        try
        {
            var featureSlider = _mapper.Map<FeatureSlider>(createFeatureSliderDto);
            await _featureSliderCollection.InsertOneAsync(featureSlider);
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Failed to create feature slider.", ex);
        }
    }

    public async Task DeleteFeatureSliderAsync(string id)
    {
        try
        {
            var result = await _featureSliderCollection.DeleteOneAsync(x => x.FeatureSliderId == id);
            if (result.DeletedCount == 0)
                throw new KeyNotFoundException("Feature slider not found.");
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An error occurred while deleting the feature slider.", ex);
        }
    }

    public async Task<List<ResultFeatureSliderDto>> GetAllFeatureSliderAsync()
    {
        try
        {
            var featureSliders = await _featureSliderCollection.Find(x => true).ToListAsync();
            return _mapper.Map<List<ResultFeatureSliderDto>>(featureSliders);
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Failed to retrieve feature sliders.", ex);
        }
    }

    public async Task<GetByIdFeatureSliderDto> GetByIdFeatureSliderAsync(string id)
    {
        try
        {
            var featureSlider = await _featureSliderCollection.Find(x => x.FeatureSliderId == id).FirstOrDefaultAsync();
            if (featureSlider == null)
                throw new KeyNotFoundException("Feature slider not found.");

            return _mapper.Map<GetByIdFeatureSliderDto>(featureSlider);
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Failed to retrieve the feature slider.", ex);
        }
    }

    public async Task UpdateFeatureSliderAsync(UpdateFeatureSliderDto updateFeatureSliderDto)
    {
        try
        {
            var featureSlider = _mapper.Map<FeatureSlider>(updateFeatureSliderDto);
            var result = await _featureSliderCollection.ReplaceOneAsync(
                x => x.FeatureSliderId == updateFeatureSliderDto.FeatureSliderId,
                featureSlider
            );

            if (result.MatchedCount == 0)
                throw new KeyNotFoundException("Feature slider to update not found.");
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Failed to update feature slider.", ex);
        }
    }

    public async Task FeatureSliderChangeStatusToFalse(string id)
    {
        try
        {
            var update = Builders<FeatureSlider>.Update.Set(x => x.Status, false);
            var result = await _featureSliderCollection.UpdateOneAsync(x => x.FeatureSliderId == id, update);

            if (result.MatchedCount == 0)
                throw new KeyNotFoundException("Feature slider to deactivate not found.");
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Failed to deactivate feature slider.", ex);
        }
    }

    public async Task FeatureSliderChangeStatusToTrue(string id)
    {
        try
        {
            var update = Builders<FeatureSlider>.Update.Set(x => x.Status, true);
            var result = await _featureSliderCollection.UpdateOneAsync(x => x.FeatureSliderId == id, update);

            if (result.MatchedCount == 0)
                throw new KeyNotFoundException("Feature slider to activate not found.");
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Failed to activate feature slider.", ex);
        }
    }
}
