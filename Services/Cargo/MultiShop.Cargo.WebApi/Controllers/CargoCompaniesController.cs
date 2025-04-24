using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Cargo.BusinessLayer.Abstract;
using MultiShop.Cargo.DtoLayer.Dtos.CargoCompanyDtos;
using MultiShop.Cargo.EntityLayer.Concrete;

namespace MultiShop.Cargo.WebApi.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class CargoCompaniesController : ControllerBase
{
    private readonly ICargoCompanyService _cargoCompanyService;

    public CargoCompaniesController(ICargoCompanyService cargoCompanyService)
    {
        _cargoCompanyService = cargoCompanyService;
    }

    [HttpGet]
    public IActionResult GetCargoCompanyList()
    {
        var companies = _cargoCompanyService.TGetAll();
        return Ok(companies);
    }

    [HttpPost]
    public IActionResult CreateCargoCompany([FromBody] CreateCargoCompanyDto createCargoCompanyDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var company = new CargoCompany
        {
            CargoCompanyName = createCargoCompanyDto.CargoCompanyName
        };

        _cargoCompanyService.TInsert(company);
        return Ok("Cargo company was created successfully.");
    }

    [HttpDelete("{id}")]
    public IActionResult RemoveCargoCompany([FromRoute] int id)
    {
        _cargoCompanyService.TDelete(id);
        return Ok("Cargo company was deleted successfully.");
    }

    [HttpGet("{id}")]
    public IActionResult GetCargoCompanyById(int id)
    {
        var company = _cargoCompanyService.TGetById(id);

        if (company == null)
            return NotFound($"Cargo company with ID {id} not found.");

        return Ok(company);
    }

    [HttpPut]
    public IActionResult UpdateCargoCompany([FromBody] UpdateCargoCompanyDto updateCargoCompanyDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var existingCompany = _cargoCompanyService.TGetById(updateCargoCompanyDto.CargoCompanyId);

        if (existingCompany == null)
        {
            return NotFound($"Cargo company with ID {updateCargoCompanyDto.CargoCompanyId} not found.");
        }

        existingCompany.CargoCompanyName = updateCargoCompanyDto.CargoCompanyName;

        _cargoCompanyService.TUpdate(existingCompany);

        return Ok("Cargo company was updated successfully.");
    }
}
