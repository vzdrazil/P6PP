﻿using BookingPayments.API.Common.Exceptions;
using BookingPayments.API.Common.MediatR.Interfaces;
using MediatR;
using System.Security.Claims;

namespace BookingPayments.API.Common.MediatR;

public sealed class UserIdBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>, IRequestWithUserId
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserIdBehavior(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var httpContext = _httpContextAccessor.HttpContext
            ?? throw new AuthException("Invalid JWT: HttpContext is null.");

        var identity = httpContext.User.Identity as ClaimsIdentity
            ?? throw new AuthException("Invalid JWT: Identity is null.");

        var userIdClaim = identity.FindFirst(ClaimTypes.NameIdentifier)
            ?? throw new AuthException("Invalid JWT: UserId claim is missing.");

        if (!int.TryParse(userIdClaim.Value, out var userId))
        {
            throw new AuthException("Invalid JWT: UserId claim is not a valid integer.");
        }

        request.UserId = userId;

        return await next();
    }
}
