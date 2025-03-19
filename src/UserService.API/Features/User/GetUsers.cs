using ReservationSystem.Shared.Results;
using UserService.API.Persistence.Entities;

namespace UserService.API.Features;

public record GetUsersResponse(IEnumerable<User> Users);

public class GetUsersHandler
{
    private readonly Services.UserService _userService;
    
    public GetUsersHandler(Services.UserService userService)
    {
        _userService = userService;
    }
    
    public async Task<ApiResult<GetUsersResponse>> HandleAsync(CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        
        var users = await _userService.GetAllUsersAsync(cancellationToken);

        return users.Any() is false
            ? new ApiResult<GetUsersResponse>(null, false, "Users not found")
            : new ApiResult<GetUsersResponse>(new GetUsersResponse(users));
    }
}

public static class GetUsersEndpoint
{
    public static void Register(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/users",
            async (GetUsersHandler handler,
                CancellationToken cancellationToken) =>
            {
                var result = await handler.HandleAsync(cancellationToken);
                
                return result.Success
                    ? Results.Ok(result)
                    : Results.NotFound(result);
            });
    }
}