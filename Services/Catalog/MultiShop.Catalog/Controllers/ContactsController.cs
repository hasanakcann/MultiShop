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
        return Ok(contact);
    }

    [HttpPost]
    public async Task<IActionResult> CreateContact(CreateContactDto createContactDto)
    {
        await _contactService.CreateContactAsync(createContactDto);
        return Ok("Message was successfully added.");
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteContact(string id)
    {
        await _contactService.DeleteContactAsync(id);
        return Ok("Message was successfully deleted.");
    }

    [HttpPut]
    public async Task<IActionResult> UpdateContact(UpdateContactDto updateContactDto)
    {
        await _contactService.UpdateContactAsync(updateContactDto);
        return Ok("Message was successfully updated.");
    }
}
