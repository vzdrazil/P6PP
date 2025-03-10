using Dapper;

namespace ExampleService.API.Persistence.Repositories;

public class ExampleRepository
{
    private readonly DapperContext _context;

    public ExampleRepository(DapperContext context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<Example>> GetAllAsync()
    {
        using var connection = await _context.CreateConnectionAsync();
        const string query = "SELECT * FROM Examples;";
        return await connection.QueryAsync<Example>(query);
    }

    public async Task<Example?> GetByIdAsync(int id)
    {
        using var connection = await _context.CreateConnectionAsync();
        const string query = "SELECT * FROM Examples WHERE Id = @Id;";
        return await connection.QuerySingleOrDefaultAsync<Example>(query, new { Id = id });
    }

    public async Task<int?> AddAsync(Example entity, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        
        using var connection = await _context.CreateConnectionAsync();
        const string query = @"
        INSERT INTO Examples (Name, Description, CreatedAt, UpdatedAt) 
        VALUES (@Name, @Description, NOW(), NOW());
        SELECT LAST_INSERT_ID();";  // ✅ MySQL Syntax

        return await connection.ExecuteScalarAsync<int>(query, entity);
    }

    public async Task UpdateAsync(Example entity)
    {
        using var connection = await _context.CreateConnectionAsync();
        const string query = @"
            UPDATE Examples 
            SET Name = @Name, Description = @Description, UpdatedAt = NOW()
            WHERE Id = @Id;";
        await connection.ExecuteAsync(query, entity);
    }

    public async Task DeleteAsync(int id)
    {
        using var connection = await _context.CreateConnectionAsync();
        const string query = "DELETE FROM Examples WHERE Id = @Id;";
        await connection.ExecuteAsync(query, new { Id = id });
    }
}