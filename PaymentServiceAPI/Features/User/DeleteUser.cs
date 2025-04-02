using FluentValidation;
using ReservationSystem.Shared.Results;

namespace UserService.API.Features;

public record DeleteUserRequest(int Id);

public class DeleteUserValidator : AbstractValidator<DeleteUserRequest>
{
    public DeleteUserValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
    }
}

public class DeleteUserHandler
{
    private readonly Services.UserService _userService;

    public DeleteUserHandler(Services.UserService userService)
    {
        _userService = userService;
    }

    public async Task<ApiResult<bool>> HandleAsync(DeleteUserRequest request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var success = await _userService.DeleteUserAsync(request.Id, cancellationToken);
        // TODO: Call to auth/delete ??
        return success 
            ? new ApiResult<bool>(success) 
            : new ApiResult<bool>(success, false, "User not found");
    }
}

public static class DeleteUserEndpoint
{
    public static void Register(IEndpointRouteBuilder app)
    {
        app.MapDelete("/api/user/{id:int}",
            async (int id,
                DeleteUserHandler handler,
                DeleteUserValidator validator,
                CancellationToken cancellationToken) =>
            {
                var request = new DeleteUserRequest(id);
                var validationResult = await validator.ValidateAsync(request, cancellationToken);
                
                if (!validationResult.IsValid)
                {
                    var errorMessages = validationResult.Errors.Select(x => x.ErrorMessage);
                    return Results.BadRequest(new ApiResult<IEnumerable<string>>(errorMessages, false, "Validation failed"));
                }

                var result = await handler.HandleAsync(request, cancellationToken);

                return result.Success
                    ? Results.Ok(result)
                    : Results.NotFound(result);
            });
    }
}