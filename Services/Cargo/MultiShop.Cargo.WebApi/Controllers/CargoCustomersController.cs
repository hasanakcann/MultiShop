using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Cargo.BusinessLayer.Abstract;
using MultiShop.Cargo.DtoLayer.Dtos.CargoCustomerDtos;
using MultiShop.Cargo.EntityLayer.Concrete;

namespace MultiShop.Cargo.WebApi.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class CargoCustomersController : ControllerBase
{
    private readonly ICargoCustomerService _cargoCustomerService;

    public CargoCustomersController(ICargoCustomerService cargoCustomerService)
    {
        _cargoCustomerService = cargoCustomerService;
    }

    [HttpGet]
    public IActionResult GetCargoCustomerList()
    {
        var customers = _cargoCustomerService.TGetAll();
        return Ok(customers);
    }

    [HttpGet("{id}")]
    public IActionResult GetCargoCustomerById(int id)
    {
        var customer = _cargoCustomerService.TGetById(id);
        if (customer == null)
            return NotFound($"Cargo customer with ID {id} not found.");

        return Ok(customer);
    }

    [HttpPost]
    public IActionResult CreateCargoCustomer([FromBody] CreateCargoCustomerDto createCargoCustomerDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var cargoCustomer = new CargoCustomer
        {
            Address = createCargoCustomerDto.Address,
            City = createCargoCustomerDto.City,
            District = createCargoCustomerDto.District,
            Email = createCargoCustomerDto.Email,
            Name = createCargoCustomerDto.Name,
            Phone = createCargoCustomerDto.Phone,
            Surname = createCargoCustomerDto.Surname,
            UserCustomerId = createCargoCustomerDto.UserCustomerId
        };

        _cargoCustomerService.TInsert(cargoCustomer);
        return Ok("Cargo customer was added successfully.");
    }

    [HttpDelete("{id}")]
    public IActionResult RemoveCargoCustomer([FromRoute] int id)
    {
        _cargoCustomerService.TDelete(id);
        return Ok("Cargo customer was deleted successfully.");
    }

    [HttpPut]
    public IActionResult UpdateCargoCustomer([FromBody] UpdateCargoCustomerDto updateCargoCustomerDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var existingCustomer = _cargoCustomerService.TGetById(updateCargoCustomerDto.CargoCustomerId);

        if (existingCustomer == null)
        {
            return NotFound($"Cargo customer with ID {updateCargoCustomerDto.CargoCustomerId} not found.");
        }

        existingCustomer.Address = updateCargoCustomerDto.Address;
        existingCustomer.City = updateCargoCustomerDto.City;
        existingCustomer.District = updateCargoCustomerDto.District;
        existingCustomer.Email = updateCargoCustomerDto.Email;
        existingCustomer.Name = updateCargoCustomerDto.Name;
        existingCustomer.Phone = updateCargoCustomerDto.Phone;
        existingCustomer.Surname = updateCargoCustomerDto.Surname;
        existingCustomer.UserCustomerId = updateCargoCustomerDto.UserCustomerId;

        _cargoCustomerService.TUpdate(existingCustomer);

        return Ok("Cargo customer was updated successfully.");
    }


    [HttpGet("GetCargoCustomerByUserId/{id}")]
    public IActionResult GetCargoCustomerById(string id)
    {
        var customer = _cargoCustomerService.TGetCargoCustomerById(id);
        if (customer == null)
            return NotFound($"Cargo customer with UserCustomerId {id} not found.");

        return Ok(customer);
    }
}
