using Microsoft.Extensions.Caching.Memory;
using UserService.API.Exceptions;
using UserService.API.Persistence.Entities;
using UserService.API.Persistence.Repositories;

namespace UserService.API.Services;

public class UserService
{
    private readonly UserRepository _userRepository;
    private readonly IMemoryCache _cache;
    private const string CacheKey = "user";
    
    public UserService(UserRepository userRepository, IMemoryCache cache)
    {
        _userRepository = userRepository;
        _cache = cache;
    }
    
    public async Task<IEnumerable<User>> GetAllUsersAsync(CancellationToken cancellationToken) // TODO: Pagination
    {
        return await _userRepository.GetAllAsync(cancellationToken);
    }
    
    public async Task<User?> GetUserByIdAsync(int id, CancellationToken cancellationToken)
    {
        string cacheKey = $"{CacheKey}:{id}";

        if (!_cache.TryGetValue(cacheKey, out User? user))
        {
            user = await _userRepository.GetByIdAsync(id, cancellationToken);

            if (user != null)
            {
                _cache.Set(cacheKey, user, TimeSpan.FromMinutes(10));
            }
        }

        return user;
    }
    
    public async Task<int?> AddUserAsync(User user, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
    
        string cacheKey = $"{CacheKey}:{user.Email}";

        try
        {
            var newUserId = await _userRepository.AddAsync(user, cancellationToken);

            if (newUserId != null)
            {
                _cache.Set(cacheKey, user, TimeSpan.FromMinutes(10));
            }

            return newUserId;
        }
        catch (DuplicateEntryException)
        {
            return null; 
        }
    }
    
    public async Task<bool> UpdateUserAsync(User user, CancellationToken cancellationToken)
    {
        string cacheKey = $"{CacheKey}:{user.Id}";
        var success = await _userRepository.UpdateAsync(user, cancellationToken);

        if (success)
        {
            _cache.Remove(cacheKey);
        }
        
        return success;
    }
    
    public async Task<bool> DeleteUserAsync(int id, CancellationToken cancellationToken)
    {
        string cacheKey = $"{CacheKey}:{id}";
        var success = await _userRepository.DeleteAsync(id, cancellationToken);

        if (success)
        {
            _cache.Remove(cacheKey);
        }
        
        return success;
    }
}