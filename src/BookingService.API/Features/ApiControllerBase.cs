using BookingService.API.Common.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BookingService.API.Features;

[ApiController]
public abstract class ApiControllerBase : ControllerBase
{
    private readonly IMediator _mediator;

    protected ApiControllerBase(IMediator mediator)
    {
        _mediator = mediator;
    }

    protected async Task<IActionResult> ExecuteAsync<T>(IRequest<T> request) where T : class
        => await HandleRequestAsync(async () =>
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        });

    protected async Task<IActionResult> ExecuteWithNoContentAsync(IRequest request)
        => await HandleRequestAsync(async () =>
        {
            await _mediator.Send(request);
            return NoContent();
        });

    protected async Task<IActionResult> ExecuteWithCreatedAtActionAsync<T>(IRequest<T> request, string actionName, Func<T, object> routeValuesFunc) where T : class
    {
        return await HandleRequestAsync(async () =>
        {
            T result = await _mediator.Send(request);

            var routeValues = routeValuesFunc(result);

            return CreatedAtAction(actionName, routeValues, result);
        });
    }

    private async Task<IActionResult> HandleRequestAsync(Func<Task<IActionResult>> action)
    {
        try
        {
            return await action();
        }
        catch (ValidationException ex)
        {
            return BadRequest(new { Error = ex.Message });
        }
        catch (NotFoundException ex)
        {
            return NotFound(new { Error = ex.Message });
        }
        catch (AuthException ex)
        {
            return Unauthorized(new { Error = ex.Message });
        }
        catch (Exception)
        {
            // TODO: Log exception
            return StatusCode(StatusCodes.Status500InternalServerError, new { Error = "An unexpected error happened." });
        }
    }
}