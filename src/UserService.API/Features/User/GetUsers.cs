using ReservationSystem.Shared.Results;
using UserService.API.Persistence.Entities;

namespace UserService.API.Features;

public record GetUsersResponse(IEnumerable<User> Users, int TotalCount);

public class GetUsersHandler
{
    private readonly Services.UserService _userService;
    
    public GetUsersHandler(Services.UserService userService)
    {
        _userService = userService;
    }
    
    public async Task<ApiResult<GetUsersResponse>> HandleAsync(int limit, int offset, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        
        var (users, totalCount) = await _userService.GetAllUsersAsync(limit, offset, cancellationToken);

        return users.Any() is false
            ? new ApiResult<GetUsersResponse>(null, false, "Users not found")
            : new ApiResult<GetUsersResponse>(new GetUsersResponse(users, totalCount));
    }
}

public static class GetUsersEndpoint
{
    public static void Register(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/users",
            async (GetUsersHandler handler,
                CancellationToken cancellationToken,
                int limit = 10,
                int offset = 0) =>
            {
                var result = await handler.HandleAsync(limit, offset, cancellationToken);
                
                return result.Success
                    ? Results.Ok(result)
                    : Results.NotFound(result);
            });
    }
}