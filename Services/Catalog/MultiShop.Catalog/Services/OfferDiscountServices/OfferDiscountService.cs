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
        var offerDiscount = _mapper.Map<OfferDiscount>(createOfferDiscountDto);
        await _offerDiscountCollection.InsertOneAsync(offerDiscount);
    }

    public async Task DeleteOfferDiscountAsync(string id)
    {
        await _offerDiscountCollection.DeleteOneAsync(x => x.OfferDiscountId == id);
    }

    public async Task<List<ResultOfferDiscountDto>> GetAllOfferDiscountAsync()
    {
        var offerDiscounts = await _offerDiscountCollection.Find(x => true).ToListAsync();
        return _mapper.Map<List<ResultOfferDiscountDto>>(offerDiscounts);
    }

    public async Task<GetByIdOfferDiscountDto> GetByIdOfferDiscountAsync(string id)
    {
        var offerDiscount = await _offerDiscountCollection.Find(x => x.OfferDiscountId == id).FirstOrDefaultAsync();
        return _mapper.Map<GetByIdOfferDiscountDto>(offerDiscount);
    }

    public async Task UpdateOfferDiscountAsync(UpdateOfferDiscountDto updateOfferDiscountDto)
    {
        var offerDiscount = _mapper.Map<OfferDiscount>(updateOfferDiscountDto);
        await _offerDiscountCollection.FindOneAndReplaceAsync(x => x.OfferDiscountId == updateOfferDiscountDto.OfferDiscountId, offerDiscount);
    }
}
