using AutoMapper;
using MongoDB.Driver;
using MultiShop.Catalog.Dtos.SpecialOfferDtos;
using MultiShop.Catalog.Entities;
using MultiShop.Catalog.Settings;

namespace MultiShop.Catalog.Services.SpecialOfferServices;

public class SpecialOfferService : ISpecialOfferService
{
    private readonly IMongoCollection<SpecialOffer> _specialOfferCollection;
    private readonly IMapper _mapper;

    public SpecialOfferService(IMapper mapper, IDatabaseSettings _databaseSettings)
    {
        var client = new MongoClient(_databaseSettings.ConnectionString);
        var database = client.GetDatabase(_databaseSettings.DatabaseName);
        _specialOfferCollection = database.GetCollection<SpecialOffer>(_databaseSettings.SpecialOfferCollectionName);
        _mapper = mapper;
    }

    public async Task CreateSpecialOfferAsync(CreateSpecialOfferDto createSpecialOfferDto)
    {
        try
        {
            var specialOffer = _mapper.Map<SpecialOffer>(createSpecialOfferDto);
            await _specialOfferCollection.InsertOneAsync(specialOffer);
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An error occurred while creating special offer.", ex);
        }
    }

    public async Task DeleteSpecialOfferAsync(string id)
    {
        try
        {
            var result = await _specialOfferCollection.DeleteOneAsync(x => x.SpecialOfferId == id);
            if (result.DeletedCount == 0)
                throw new KeyNotFoundException("Special offer not found.");
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An error occurred while deleting special offer.", ex);
        }
    }

    public async Task<List<ResultSpecialOfferDto>> GetAllSpecialOfferAsync()
    {
        try
        {
            var specialOffers = await _specialOfferCollection.Find(x => true).ToListAsync();
            return _mapper.Map<List<ResultSpecialOfferDto>>(specialOffers);
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An error occurred while retrieving all special offers.", ex);
        }
    }

    public async Task<GetByIdSpecialOfferDto> GetByIdSpecialOfferAsync(string id)
    {
        try
        {
            var specialOffer = await _specialOfferCollection.Find(x => x.SpecialOfferId == id).FirstOrDefaultAsync();
            if (specialOffer == null)
                throw new KeyNotFoundException("Special offer not found.");

            return _mapper.Map<GetByIdSpecialOfferDto>(specialOffer);
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An error occurred while retrieving special offer by ID.", ex);
        }
    }

    public async Task UpdateSpecialOfferAsync(UpdateSpecialOfferDto updateSpecialOfferDto)
    {
        try
        {
            var specialOffer = _mapper.Map<SpecialOffer>(updateSpecialOfferDto);
            var result = await _specialOfferCollection.FindOneAndReplaceAsync(x => x.SpecialOfferId == updateSpecialOfferDto.SpecialOfferId, specialOffer);

            if (result == null)
                throw new KeyNotFoundException("Special offer to update not found.");
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An error occurred while updating special offer.", ex);
        }
    }
}
