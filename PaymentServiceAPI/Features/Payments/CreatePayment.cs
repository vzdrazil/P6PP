using FluentValidation;
using ReservationSystem.Shared.Results;
using PaymentService.API.Persistence.Entities.DB.Models;
using PaymentService.API.; // using UserService.API.Services;

namespace UserService.API.Features.Roles;

public record CreatePaymentRequest(string Name, string Description);

public class CreatePaymentValidator : AbstractValidator<CreatePaymentRequest>
{
    public CreatePaymentValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Description).NotEmpty();
    }
}

public class CreatePaymentHandler
{
    private readonly PaymentService _paymentService;

    public CreatePaymentHandler(PaymentService roleService)
    {
        _paymentService = roleService;
    }

    public async Task<ApiResult<int>> HandleAsync(CreatePaymentRequest request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var role = new Payment;
        {
            Name = request.Name,                            
            Description = request.Description
        };

        var id = await _paymentService.AddRoleAsync(role, cancellationToken);

        return id.HasValue
            ? new ApiResult<int>(id.Value)
            : new ApiResult<int>(0, false, "Role not created");
    }
}


public static class CreatePaymentEndpoint
{
    public static void Register(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/role",
            async (CreatePaymentRequest request,
                CreatePaymentHandler handler,
                CreatePaymentValidator validator,
                CancellationToken cancellationToken) =>
            {
                var validationResult = await validator.ValidateAsync(request, cancellationToken);
                
                if (!validationResult.IsValid)
                {
                    var errorMessages = validationResult.Errors.Select(x => x.ErrorMessage);
                    return Results.BadRequest(new ApiResult<IEnumerable<string>>(errorMessages, false, "Validation failed"));
                }

                var result = await handler.HandleAsync(request, cancellationToken);
                
                return result.Success
                    ? Results.Ok(result)
                    : Results.BadRequest(result);
            });
    }
}