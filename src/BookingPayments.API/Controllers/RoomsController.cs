using BookingPayments.API.Application.Abstraction;
using BookingPayments.API.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BookingPayments.API.Controllers;

[ApiController]
[Route("api/rooms")]
//[Authorize(Roles = nameof(Roles.Admin) + ", " + nameof(Roles.Instructor))]
public class RoomsController : Controller
{
    private readonly IRoomAppService _roomAppService;

    public RoomsController(IRoomAppService roomAppService)
    {
        _roomAppService = roomAppService;
    }

    [HttpGet]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(IEnumerable<Room>))]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized, Type = typeof(ProblemDetails))]
    public async Task<IActionResult> GetAll()
    {
        var rooms = await _roomAppService.GetAllAsync();
        return Ok(rooms);
    }

    [HttpGet("{roomId}")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(Room))]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized, Type = typeof(ProblemDetails))]
    [ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(ProblemDetails))]
    public async Task<IActionResult> Get(int roomId)
    {
        var room = await _roomAppService.GetByIdAsync(roomId);

        if (room is null)
            return NotFound();

        return Ok(room);
    }

    [HttpPost]
    [ProducesResponseType((int)HttpStatusCode.Created)]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized, Type = typeof(ProblemDetails))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ValidationProblemDetails))]
    public async Task<IActionResult> Create(Room room)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        await _roomAppService.CreateAsync(room);
        return Created();
    }

    [HttpPut]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(Room))]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized, Type = typeof(ProblemDetails))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ValidationProblemDetails))]
    public async Task<IActionResult> Edit(Room room)
    {
        if (!ModelState.IsValid)
            return BadRequest();

        await _roomAppService.UpdateAsync(room);
        return Ok(room);
    }

    [HttpDelete]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(ProblemDetails))]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _roomAppService.DeleteAsync(id);

        return deleted ? Ok() : NotFound();
    }
}
