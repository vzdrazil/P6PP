using FluentValidation;
using ReservationSystem.Shared.Results;
using UserService.API.Persistence.Entities;

namespace UserService.API.Features;

public record UpdateUserRequest(string Username, string FirstName, string LastName, string Email, string PhoneNumber, decimal Weight, decimal Height);

public class UpdateUserValidator : AbstractValidator<UpdateUserRequest>
{
    public UpdateUserValidator()
    {
        RuleFor(x => x.Username).NotEmpty().MaximumLength(20)
            .WithMessage("Username must be between 1 and 20 characters");
        RuleFor(x => x.FirstName).NotEmpty().MaximumLength(20)
            .WithMessage("First name must be between 1 and 20 characters");
        RuleFor(x => x.LastName).NotEmpty().MaximumLength(20)
            .WithMessage("Last name must be between 1 and 20 characters");
        RuleFor(x => x.Email).NotEmpty().MaximumLength(50).EmailAddress()
            .WithMessage("Invalid email address");
        RuleFor(x => x.PhoneNumber).NotEmpty().MaximumLength(20)
            .WithMessage("Phone number must be between 1 and 20 characters");

        RuleFor(x => x.Weight).GreaterThan(0).LessThan(1000)
            .WithMessage("Weight must be between 0 and 1000");
        RuleFor(x => x.Height).GreaterThan(0).LessThan(300)
            .WithMessage("Height must be between 0 and 300");
    }
}

public class UpdateUserHandler
{
    private readonly Services.UserService _userService;

    public UpdateUserHandler(Services.UserService userService)
    {
        _userService = userService;
    }

    public async Task<ApiResult<User>> HandleAsync(int id, UpdateUserRequest request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var user = await _userService.GetUserByIdAsync(id, cancellationToken);

        if (user is null)
        {
            return new ApiResult<User>(null, false, "User not found");
        }

        user.Username = request.Username;
        user.FirstName = request.FirstName;
        user.LastName = request.LastName;
        user.Email = request.Email;
        user.PhoneNumber = request.PhoneNumber;
        user.Weight = request.Weight;
        user.Height = request.Height;
        user.UpdatedOn = DateTime.UtcNow;

        await _userService.UpdateUserAsync(user, cancellationToken);

        return new ApiResult<User>(user, true, "User updated successfully");
    }
}

public static class UpdateUserEndpoint
{
    public static void Register(IEndpointRouteBuilder app)
    {
        app.MapPut("/api/user/{id:int}",
            async (int id,
                UpdateUserRequest updateRequest,
                UpdateUserValidator validator,
                UpdateUserHandler handler,
                CancellationToken cancellationToken) =>
            {
                var validationResult = await validator.ValidateAsync(updateRequest, cancellationToken);

                if (!validationResult.IsValid)
                {
                    var errorMessages = validationResult.Errors.Select(x => x.ErrorMessage);
                    return Results.BadRequest(new ApiResult<IEnumerable<string>>(errorMessages, false, "Validation failed"));
                }

                var result = await handler.HandleAsync(id, updateRequest, cancellationToken);

                return result.Success
                    ? Results.Ok(result)
                    : Results.NotFound(result);
            });
    }
}