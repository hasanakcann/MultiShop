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
        var feature = _mapper.Map<Feature>(createFeatureDto);
        await _featureCollection.InsertOneAsync(feature);
    }

    public async Task DeleteFeatureAsync(string id)
    {
        await _featureCollection.DeleteOneAsync(x => x.FeatureId == id);
    }

    public async Task<List<ResultFeatureDto>> GetAllFeatureAsync()
    {
        var features = await _featureCollection.Find(x => true).ToListAsync();
        return _mapper.Map<List<ResultFeatureDto>>(features);
    }

    public async Task<GetByIdFeatureDto> GetByIdFeatureAsync(string id)
    {
        var feature = await _featureCollection.Find(x => x.FeatureId == id).FirstOrDefaultAsync();
        return _mapper.Map<GetByIdFeatureDto>(feature);
    }

    public async Task UpdateFeatureAsync(UpdateFeatureDto updateFeatureDto)
    {
        var feature = _mapper.Map<Feature>(updateFeatureDto);
        await _featureCollection.FindOneAndReplaceAsync(x => x.FeatureId == updateFeatureDto.FeatureId, feature);
    }
}
