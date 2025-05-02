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
        try
        {
            var contactList = await _contactService.GetAllContactAsync();
            return Ok(contactList);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while fetching contact list: {ex.Message}");
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetContactById(string id)
    {
        try
        {
            var contact = await _contactService.GetByIdContactAsync(id);
            if (contact == null)
                return NotFound($"Message with ID '{id}' was not found.");

            return Ok(contact);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while fetching message: {ex.Message}");
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateContact(CreateContactDto createContactDto)
    {
        try
        {
            await _contactService.CreateContactAsync(createContactDto);
            return Ok("Message was successfully added.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while adding message: {ex.Message}");
        }
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteContact(string id)
    {
        try
        {
            var existing = await _contactService.GetByIdContactAsync(id);
            if (existing == null)
                return NotFound($"Message with ID '{id}' was not found.");

            await _contactService.DeleteContactAsync(id);
            return Ok("Message was successfully deleted.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while deleting message: {ex.Message}");
        }
    }

    [HttpPut]
    public async Task<IActionResult> UpdateContact(UpdateContactDto updateContactDto)
    {
        try
        {
            var existing = await _contactService.GetByIdContactAsync(updateContactDto.ContactId);
            if (existing == null)
                return NotFound($"Message with ID '{updateContactDto.ContactId}' was not found.");

            await _contactService.UpdateContactAsync(updateContactDto);
            return Ok("Message was successfully updated.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while updating message: {ex.Message}");
        }
    }
}
