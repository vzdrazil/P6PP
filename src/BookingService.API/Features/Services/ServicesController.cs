using BookingService.API.Domain.Enums;
using System.Net;
using BookingService.API.Features.Bookings.Commands;
using BookingService.API.Features.Bookings.Models;
using BookingService.API.Features.Bookings.Queries;
using BookingService.API.Features.Services.Commands;
using BookingService.API.Features.Services.Models;
using BookingService.API.Features.Services.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BookingService.API.Features.Services;

[Route("api/[controller]")]
public sealed class ServicesController(IMediator mediator) : ApiControllerBase(mediator)
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
        => await ExecuteAsync(new GetServicesQuery());

    [HttpGet("trainer/{trainerId}")]
    public async Task<IActionResult> Get(int trainerId)
        => await ExecuteAsync(new GetServicesQuery(trainerId));

    [HttpGet("{serviceId}")]
    public async Task<IActionResult> GetDetail(int serviceId)
        => await ExecuteAsync(new GetServiceDetailQuery(serviceId));

    [HttpPost]
    public async Task<IActionResult> Create(CreateServiceRequest service)
       => await ExecuteWithCreatedAtActionAsync(new CreateServiceCommand(service), nameof(GetDetail), response => new { serviceId = response.Id });


    //[HttpDelete("{serviceId}")]
    //public async Task<IActionResult> Delete(int serviceId)
    //    => await ExecuteWithNoContentAsync(new DeleteServiceCommand(serviceId));
}
