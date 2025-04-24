using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Cargo.BusinessLayer.Abstract;
using MultiShop.Cargo.DtoLayer.Dtos.CargoOperationDtos;
using MultiShop.Cargo.EntityLayer.Concrete;

namespace MultiShop.Cargo.WebApi.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class CargoOperationsController : ControllerBase
{
    private readonly ICargoOperationService _cargoOperationService;

    public CargoOperationsController(ICargoOperationService cargoOperationService)
    {
        _cargoOperationService = cargoOperationService;
    }

    [HttpGet]
    public IActionResult GetCargoOperationList()
    {
        var cargoOperations = _cargoOperationService.TGetAll();
        return Ok(cargoOperations);
    }

    [HttpGet("{id}")]
    public IActionResult GetCargoOperationById(int id)
    {
        var cargoOperation = _cargoOperationService.TGetById(id);
        if (cargoOperation == null)
            return NotFound($"Cargo operation with ID {id} not found.");

        return Ok(cargoOperation);
    }

    [HttpPost]
    public IActionResult CreateCargoOperation([FromBody] CreateCargoOperationDto createCargoOperationDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var cargoOperation = new CargoOperation()
        {
            Barcode = createCargoOperationDto.Barcode,
            Description = createCargoOperationDto.Description,
            OperationDate = createCargoOperationDto.OperationDate
        };

        _cargoOperationService.TInsert(cargoOperation);
        return Ok("Cargo operation was created successfully.");
    }

    [HttpDelete("{id}")]
    public IActionResult RemoveCargoOperation([FromRoute] int id)
    {
        _cargoOperationService.TDelete(id);
        return Ok("Cargo operation was deleted successfully.");
    }

    [HttpPut]
    public IActionResult UpdateCargoOperation([FromBody] UpdateCargoOperationDto updateCargoOperationDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var existingCargoOperation = _cargoOperationService.TGetById(updateCargoOperationDto.CargoOperationId);

        if (existingCargoOperation == null)
        {
            return NotFound($"Cargo operation with ID {updateCargoOperationDto.CargoOperationId} not found.");
        }

        existingCargoOperation.Barcode = updateCargoOperationDto.Barcode;
        existingCargoOperation.Description = updateCargoOperationDto.Description;
        existingCargoOperation.OperationDate = updateCargoOperationDto.OperationDate;

        _cargoOperationService.TUpdate(existingCargoOperation);

        return Ok("Cargo operation was updated successfully.");
    }
}
