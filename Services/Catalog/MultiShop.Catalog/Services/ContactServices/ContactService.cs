using AutoMapper;
using MongoDB.Driver;
using MultiShop.Catalog.Dtos.ContactDtos;
using MultiShop.Catalog.Entities;
using MultiShop.Catalog.Settings;

namespace MultiShop.Catalog.Services.ContactServices;

public class ContactService : IContactService
{
    private readonly IMongoCollection<Contact> _contactCollection;
    private readonly IMapper _mapper;

    public ContactService(IMapper mapper, IDatabaseSettings _databaseSettings)
    {
        var client = new MongoClient(_databaseSettings.ConnectionString);
        var database = client.GetDatabase(_databaseSettings.DatabaseName);
        _contactCollection = database.GetCollection<Contact>(_databaseSettings.ContactCollectionName);
        _mapper = mapper;
    }

    public async Task CreateContactAsync(CreateContactDto createContactDto)
    {
        try
        {
            var contact = _mapper.Map<Contact>(createContactDto);
            await _contactCollection.InsertOneAsync(contact);
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Failed to create contact. Please try again later.", ex);
        }
    }

    public async Task DeleteContactAsync(string id)
    {
        try
        {
            var result = await _contactCollection.DeleteOneAsync(x => x.ContactId == id);

            if (result.DeletedCount == 0)
                throw new KeyNotFoundException("The contact to delete was not found.");
        }
        catch (KeyNotFoundException ex)
        {
            throw new ApplicationException(ex.Message, ex);
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An error occurred while deleting the contact. Please try again later.", ex);
        }
    }

    public async Task<List<ResultContactDto>> GetAllContactAsync()
    {
        try
        {
            var contacts = await _contactCollection.Find(x => true).ToListAsync();
            return _mapper.Map<List<ResultContactDto>>(contacts);
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Failed to retrieve contact list. Please try again later.", ex);
        }
    }

    public async Task<GetByIdContactDto> GetByIdContactAsync(string id)
    {
        try
        {
            var contact = await _contactCollection.Find(x => x.ContactId == id).FirstOrDefaultAsync();

            if (contact == null)
                throw new KeyNotFoundException("The requested contact was not found.");

            return _mapper.Map<GetByIdContactDto>(contact);
        }
        catch (KeyNotFoundException ex)
        {
            throw new ApplicationException(ex.Message, ex);
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An error occurred while retrieving the contact. Please try again later.", ex);
        }
    }

    public async Task UpdateContactAsync(UpdateContactDto updateContactDto)
    {
        try
        {
            var contact = _mapper.Map<Contact>(updateContactDto);
            var result = await _contactCollection.ReplaceOneAsync(x => x.ContactId == updateContactDto.ContactId, contact);

            if (result.MatchedCount == 0)
                throw new KeyNotFoundException("The contact to update was not found.");
        }
        catch (KeyNotFoundException ex)
        {
            throw new ApplicationException(ex.Message, ex);
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Failed to update contact. Please try again later.", ex);
        }
    }
}
