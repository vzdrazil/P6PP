using ExampleService.API.Persistence;
using ExampleService.API.Persistence.Repositories;
using Microsoft.Extensions.Caching.Memory;

namespace ExampleService.API.Services;

public class ServiceExample 
{
    private readonly ExampleRepository _exampleRepository;
    private readonly IMemoryCache _memoryCache;
    private const string CacheKey = "examples";
    
    public ServiceExample(ExampleRepository exampleRepository, IMemoryCache memoryCache)
    {
        _exampleRepository = exampleRepository;
        _memoryCache = memoryCache;
    }

    public async Task<IEnumerable<Example>> GetAllExamplesAsync()
    {
        if (!_memoryCache.TryGetValue(CacheKey, out IEnumerable<Example>? examples))
        {
            examples = await _exampleRepository.GetAllAsync();

            _memoryCache.Set(CacheKey, examples, new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10)
            });
        }
        
        return examples!;
    }
    
    public async Task<int?> CreateExampleAsync(Example entity, CancellationToken cancellationToken)
    {
        var result = await _exampleRepository.AddAsync(entity, cancellationToken);
        
        _memoryCache.Remove(CacheKey);
        _memoryCache.Set("example:" + result, entity, new MemoryCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10)
        });
        
        return result;
    }
}