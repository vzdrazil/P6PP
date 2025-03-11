using Dapper;
using UserService.API.Persistence.Entities;

namespace UserService.API.Persistence.Repositories;

public class UserRepository
{
    private readonly DapperContext _context;

    public UserRepository(DapperContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<User>> GetAllAsync(CancellationToken cancellationToken) // TODO: Pagination
    {
        cancellationToken.ThrowIfCancellationRequested(); 

        using var connection = await _context.CreateConnectionAsync();
        const string query = @"
            SELECT u.*, 
                   r.Id AS Role_Id, r.Name AS Role_Name, r.Description AS Role_Description, 
                   r.CreatedOn AS Role_CreatedOn, r.UpdatedOn AS Role_UpdatedOn
            FROM Users u
            JOIN Roles r ON u.RoleId = r.Id;";

        return await connection.QueryAsync<User, Role, User>(
            query,
            (user, role) =>
            {
                user.Role = role;
                return user;
            },
            splitOn: "Role_Id"
        );
    }

    public async Task<User?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested(); 

        using var connection = await _context.CreateConnectionAsync();
        const string query = @"
        SELECT u.*, 
               r.Id AS Role_Id, r.Name AS Role_Name, r.Description AS Role_Description, 
               r.CreatedOn AS Role_CreatedOn, r.UpdatedOn AS Role_UpdatedOn
        FROM Users u
        JOIN Roles r ON u.RoleId = r.Id
        WHERE u.Id = @Id;";

        var users = await connection.QueryAsync<User, Role, User>(
            query,
            (user, role) =>
            {
                user.Role = role;
                return user;
            },
            new { Id = id },
            splitOn: "Role_Id"
        );

        return users.FirstOrDefault();
    }
    
    public async Task<int?> AddAsync(User user, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested(); 

        using var connection = await _context.CreateConnectionAsync();
        const string query = @"
        INSERT INTO Users (RoleId, Username, FirstName, LastName, Email, Verified, State, PhoneNumber, Sex, PasswordHash, Weight, Height, DateOfBirth, CreatedOn, UpdatedOn) 
        VALUES (@RoleId, @Username, @FirstName, @LastName, @Email, @Verified, @State, @PhoneNumber, @Sex, @PasswordHash, @Weight, @Height, @DateOfBirth, NOW(), NOW());
        SELECT LAST_INSERT_ID();";

        return await connection.ExecuteScalarAsync<int>(query, user);
    }

    public async Task<bool> UpdateAsync(User user, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested(); 

        using var connection = await _context.CreateConnectionAsync();
        const string query = @"
        UPDATE Users 
        SET RoleId = @RoleId, Username = @Username, FirstName = @FirstName, LastName = @LastName, Email = @Email, 
            Verified = @Verified, State = @State, PhoneNumber = @PhoneNumber, Sex = @Sex, PasswordHash = @PasswordHash, 
            Weight = @Weight, Height = @Height, DateOfBirth = @DateOfBirth, UpdatedOn = NOW()
        WHERE Id = @Id;";

        int rowsAffected = await connection.ExecuteAsync(query, user);

        return rowsAffected > 0; 
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        using var connection = await _context.CreateConnectionAsync();
        const string query = "DELETE FROM Users WHERE Id = @Id;";
        
        int rowsAffected = await connection.ExecuteAsync(query, new { Id = id });

        return rowsAffected > 0; 
    }
}