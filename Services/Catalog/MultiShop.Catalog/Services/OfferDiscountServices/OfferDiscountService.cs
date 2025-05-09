using AutoMapper;
using MongoDB.Driver;
using MultiShop.Catalog.Dtos.OfferDiscountDtos;
using MultiShop.Catalog.Entities;
using MultiShop.Catalog.Settings;

namespace MultiShop.Catalog.Services.OfferDiscountServices;

public class OfferDiscountService : IOfferDiscountService
{
    private readonly IMongoCollection<OfferDiscount> _offerDiscountCollection;
    private readonly IMapper _mapper;

    public OfferDiscountService(IMapper mapper, IDatabaseSettings _databaseSettings)
    {
        var client = new MongoClient(_databaseSettings.ConnectionString);
        var database = client.GetDatabase(_databaseSettings.DatabaseName);
        _offerDiscountCollection = database.GetCollection<OfferDiscount>(_databaseSettings.OfferDiscountCollectionName);
        _mapper = mapper;
    }

    public async Task CreateOfferDiscountAsync(CreateOfferDiscountDto createOfferDiscountDto)
    {
        try
        {
            var offerDiscount = _mapper.Map<OfferDiscount>(createOfferDiscountDto);
            await _offerDiscountCollection.InsertOneAsync(offerDiscount);
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Failed to create offer discount.", ex);
        }
    }

    public async Task DeleteOfferDiscountAsync(string id)
    {
        try
        {
            var result = await _offerDiscountCollection.DeleteOneAsync(x => x.OfferDiscountId == id);
            if (result.DeletedCount == 0)
                throw new KeyNotFoundException("Offer discount not found.");
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Failed to delete offer discount.", ex);
        }
    }

    public async Task<List<ResultOfferDiscountDto>> GetAllOfferDiscountAsync()
    {
        try
        {
            var offerDiscounts = await _offerDiscountCollection.Find(x => true).ToListAsync();
            return _mapper.Map<List<ResultOfferDiscountDto>>(offerDiscounts);
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Failed to retrieve offer discounts.", ex);
        }
    }

    public async Task<GetByIdOfferDiscountDto> GetByIdOfferDiscountAsync(string id)
    {
        try
        {
            var offerDiscount = await _offerDiscountCollection.Find(x => x.OfferDiscountId == id).FirstOrDefaultAsync();
            if (offerDiscount == null)
                throw new KeyNotFoundException("Offer discount not found.");

            return _mapper.Map<GetByIdOfferDiscountDto>(offerDiscount);
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Failed to retrieve offer discount.", ex);
        }
    }

    public async Task UpdateOfferDiscountAsync(UpdateOfferDiscountDto updateOfferDiscountDto)
    {
        try
        {
            var offerDiscount = _mapper.Map<OfferDiscount>(updateOfferDiscountDto);
            var result = await _offerDiscountCollection.ReplaceOneAsync(
                x => x.OfferDiscountId == updateOfferDiscountDto.OfferDiscountId,
                offerDiscount
            );

            if (result.MatchedCount == 0)
                throw new KeyNotFoundException("Offer discount to update not found.");
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Failed to update offer discount.", ex);
        }
    }
}
