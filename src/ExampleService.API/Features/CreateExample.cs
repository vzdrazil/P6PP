using ExampleService.API.Persistence;
using ExampleService.API.Services;
using FluentValidation;
using ReservationSystem.Shared.Results;

namespace ExampleService.API.Features;

public record CreateExampleRequest(string Name, string? Description);

public record CreateExampleResponse(int Id);

public class CreateExampleRequestValidator : AbstractValidator<CreateExampleRequest>
{
    public CreateExampleRequestValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
        RuleFor(x => x.Description).MaximumLength(500);
    }
}

public class CreateExampleHandler
{
    private readonly ServiceExample _serviceExample;

    public CreateExampleHandler(ServiceExample serviceExample)
    {
        _serviceExample = serviceExample;
    }

    public async Task<ApiResult<CreateExampleResponse>> Handle(CreateExampleRequest request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var example = new Example
        {
            Name = request.Name,
            Description = request.Description
        };

        var id = await _serviceExample.CreateExampleAsync(example, cancellationToken);

        return id.HasValue
            ? new ApiResult<CreateExampleResponse>(new CreateExampleResponse(id.Value)) // for success no need to pass message or success flag
            : new ApiResult<CreateExampleResponse>(null, false, "Failed to create example"); // for failure pass message and success flag
    }
}

public static class CreateExampleEndpoint
{
    public static void Register(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/examples/create",
            async (CreateExampleRequest request, CreateExampleHandler handler, CreateExampleRequestValidator validator, CancellationToken cancellationToken) =>
            {
                var validationResult = await validator.ValidateAsync(request, cancellationToken);

                if (!validationResult.IsValid)
                {
                    var errorMessages = validationResult.Errors.Select(x => x.ErrorMessage);
                    return Results.BadRequest(new ApiResult<IEnumerable<string>>(errorMessages, false, "Validation failed"));
                }

                var result = await handler.Handle(request, cancellationToken);

                return result.Success
                    ? Results.Ok(result)
                    : Results.BadRequest(result);
            });
    }
}