using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Catalog.Dtos.ContactDtos;
using MultiShop.Catalog.Services.ContactServices;

namespace MultiShop.Catalog.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class ContactsController : ControllerBase
{
    private readonly IContactService _contactService;

    public ContactsController(IContactService contactService)
    {
        _contactService = contactService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllContactList()
    {
        var contactList = await _contactService.GetAllContactAsync();
        return Ok(contactList);  
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetContactById(string id)
    {
        var contact = await _contactService.GetByIdContactAsync(id);
        return contact is null
            ? NotFound($"Message with ID '{id}' was not found.")  
            : Ok(contact);
    }

    [HttpPost]
    public async Task<IActionResult> CreateContact(CreateContactDto createContactDto)
    {
        await _contactService.CreateContactAsync(createContactDto);
        return Ok("Message was successfully added.");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteContact(string id)
    {
        var existing = await _contactService.GetByIdContactAsync(id);
        if (existing is null)
            return NotFound($"Message with ID '{id}' was not found.");

        await _contactService.DeleteContactAsync(id);
        return Ok("Message was successfully deleted.");
    }

    [HttpPut]
    public async Task<IActionResult> UpdateContact(UpdateContactDto updateContactDto)
    {
        var existing = await _contactService.GetByIdContactAsync(updateContactDto.ContactId);
        if (existing is null)
            return NotFound($"Message with ID '{updateContactDto.ContactId}' was not found.");

        await _contactService.UpdateContactAsync(updateContactDto);
        return Ok("Message was successfully updated.");
    }
}
