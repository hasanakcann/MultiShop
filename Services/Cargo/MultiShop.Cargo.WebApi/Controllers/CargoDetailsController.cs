using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Cargo.BusinessLayer.Abstract;
using MultiShop.Cargo.DtoLayer.Dtos.CargoDetailDtos;
using MultiShop.Cargo.EntityLayer.Concrete;

namespace MultiShop.Cargo.WebApi.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class CargoDetailsController : ControllerBase
{
    private readonly ICargoDetailService _cargoDetailService;

    public CargoDetailsController(ICargoDetailService cargoDetailService)
    {
        _cargoDetailService = cargoDetailService;
    }

    [HttpGet]
    public IActionResult GetCargoDetailList()
    {
        var cargoDetails = _cargoDetailService.TGetAll();
        return Ok(cargoDetails);
    }

    [HttpGet("{id}")]
    public IActionResult GetCargoDetailById(int id)
    {
        var cargoDetail = _cargoDetailService.TGetById(id);
        if (cargoDetail == null)
            return NotFound($"Cargo detail with ID {id} not found.");

        return Ok(cargoDetail);
    }

    [HttpPost]
    public IActionResult CreateCargoDetail([FromBody] CreateCargoDetailDto createCargoDetailDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var cargoDetail = new CargoDetail
        {
            Barcode = createCargoDetailDto.Barcode,
            SenderCustomer = createCargoDetailDto.SenderCustomer,
            ReceiverCustomer = createCargoDetailDto.ReceiverCustomer,
            CargoCompanyId = createCargoDetailDto.CargoCompanyId
        };

        _cargoDetailService.TInsert(cargoDetail);
        return Ok("Cargo detail was created successfully.");
    }

    [HttpDelete("{id}")]
    public IActionResult RemoveCargoDetail([FromRoute] int id)
    {
        _cargoDetailService.TDelete(id);
        return Ok("Cargo detail was deleted successfully.");
    }

    [HttpPut]
    public IActionResult UpdateCargoDetail([FromBody] UpdateCargoDetailDto updateCargoDetailDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var existingCargoDetail = _cargoDetailService.TGetById(updateCargoDetailDto.CargoDetailId);

        if (existingCargoDetail == null)
        {
            return NotFound($"Cargo detail with ID {updateCargoDetailDto.CargoDetailId} not found.");
        }

        existingCargoDetail.Barcode = updateCargoDetailDto.Barcode;
        existingCargoDetail.SenderCustomer = updateCargoDetailDto.SenderCustomer;
        existingCargoDetail.ReceiverCustomer = updateCargoDetailDto.ReceiverCustomer;
        existingCargoDetail.CargoCompanyId = updateCargoDetailDto.CargoCompanyId;

        _cargoDetailService.TUpdate(existingCargoDetail);

        return Ok("Cargo detail was updated successfully.");
    }

}
