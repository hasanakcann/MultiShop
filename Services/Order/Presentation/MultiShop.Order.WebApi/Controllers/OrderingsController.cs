using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Order.Application.Features.Mediator.Commands.OrderingCommands;
using MultiShop.Order.Application.Features.Mediator.Queries.OrderingQueries;

namespace MultiShop.Order.WebApi.Controllers;

//[Authorize]
[Route("api/[controller]")]
[ApiController]
public class OrderingsController : ControllerBase
{
    private readonly IMediator _mediator;

    public OrderingsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> OrderingList()
    {
        var orderList = await _mediator.Send(new GetOrderingQuery());
        return Ok(orderList);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetOrderingById(int id)
    {
        var order = await _mediator.Send(new GetOrderingByIdQuery(id));
        return Ok(order);
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrdering(CreateOrderingCommand command)
    {
        await _mediator.Send(command);
        return Ok("Order has been successfully created.");
    }

    [HttpPut]
    public async Task<IActionResult> UpdateOrdering(UpdateOrderingCommand command)
    {
        await _mediator.Send(command);
        return Ok("Order has been successfully updated.");
    }

    [HttpDelete]
    public async Task<IActionResult> RemoveOrdering(int id)
    {
        await _mediator.Send(new RemoveOrderingCommand(id));
        return Ok("Order has been successfully deleted.");
    }

    [HttpGet("GetOrderingByUserId/{id}")]
    public async Task<IActionResult> GetOrderingByUserId(string id)
    {
        var userOrders = await _mediator.Send(new GetOrderingByUserIdQuery(id));
        return Ok(userOrders);
    }
}
