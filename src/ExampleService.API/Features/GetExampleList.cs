using ExampleService.API.Persistence;
using ExampleService.API.Services;
using FluentValidation;
using ReservationSystem.Shared.Results;

namespace ExampleService.API.Features;

public record GetExampleListRequest(int Page, int PageSize); // Im not going to do the use case now, implement pagination by yourself :)

public record GetExampleListResponse(IEnumerable<Example> Examples);

public class GetExampleListRequestValidator : AbstractValidator<GetExampleListRequest>
{
    public GetExampleListRequestValidator()
    {
        RuleFor(x => x.Page).GreaterThanOrEqualTo(1);
        RuleFor(x => x.PageSize).GreaterThanOrEqualTo(1);
    }
}

public class GetExampleListHandler
{
    private readonly ServiceExample _serviceExample;

    public GetExampleListHandler(ServiceExample serviceExample)
    {
        _serviceExample = serviceExample;
    }

    public async Task<ApiResult<GetExampleListResponse>> Handle(GetExampleListRequest request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var examples = await _serviceExample.GetAllExamplesAsync(); // here you would pass the pagination etc

        return new ApiResult<GetExampleListResponse>(new GetExampleListResponse(examples));
    }
}


public static class GetExampleListEndpoint
{
    public static void Register(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/examples",
            async (GetExampleListRequest request, GetExampleListHandler handler, CancellationToken cancellationToken) =>
            {
                var validationResult = await new GetExampleListRequestValidator().ValidateAsync(request, cancellationToken);
                
                if (!validationResult.IsValid)
                {
                    var errorMessages = validationResult.Errors.Select(x => x.ErrorMessage);
                    return Results.BadRequest(new ApiResult<IEnumerable<string>>(errorMessages, false, "Validation failed"));
                }
                
                var result = await handler.Handle(request, cancellationToken);
                
                return Results.Ok(result);
            });
    }
}