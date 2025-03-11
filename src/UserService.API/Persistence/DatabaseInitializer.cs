using Dapper;
using MySqlConnector;

namespace UserService.API.Persistence;

public class DatabaseInitializer
{
    private readonly string _connectionString;
    private readonly string _adminConnectionString;
    private readonly string _databaseName;
    private readonly ILogger<DatabaseInitializer> _logger;

    public DatabaseInitializer(IConfiguration configuration, ILogger<DatabaseInitializer> logger)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection") 
            ?? throw new Exception("Database connection string is missing in appsettings.json.");
        _logger = logger;

        var builder = new MySqlConnectionStringBuilder(_connectionString);
        _databaseName = builder.Database;

        builder.Database = "";  // This allows admin-level queries
        _adminConnectionString = builder.ToString();
    }

    public async Task InitializeDatabaseAsync()
    {
        try
        {
            _logger.LogInformation("Checking if database '{Database}' exists...", _databaseName);

            await using var adminConnection = new MySqlConnection(_adminConnectionString);
            await adminConnection.OpenAsync();

            var existsQuery = "SELECT SCHEMA_NAME FROM INFORMATION_SCHEMA.SCHEMATA WHERE SCHEMA_NAME = @DatabaseName;";
            var databaseExists = await adminConnection.ExecuteScalarAsync<string>(existsQuery, new { DatabaseName = _databaseName });

            if (databaseExists == null)
            {
                _logger.LogInformation("Database '{Database}' does not exist. Creating now...", _databaseName);
                await adminConnection.ExecuteAsync($"CREATE DATABASE `{_databaseName}`;");
                _logger.LogInformation("Database '{Database}' created successfully.", _databaseName);
            }
            else
            {
                _logger.LogInformation("Database '{Database}' already exists.", _databaseName);
            }
            
            // Now connect to the actual microservice database and ensure tables exist
            await using var connection = new MySqlConnection(_connectionString);
            await connection.OpenAsync();
            
            const string createRoleTableQuery = @"
                CREATE TABLE IF NOT EXISTS Roles (
                    Id INT AUTO_INCREMENT PRIMARY KEY,
                    Name VARCHAR(20) NOT NULL,
                    Description VARCHAR(50) NOT NULL,
                    CreatedOn TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
                    UpdatedOn TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP
                );";

            const string createTableQuery = @"
                CREATE TABLE IF NOT EXISTS Users (
                    Id INT AUTO_INCREMENT PRIMARY KEY,
                    RoleId INT NOT NULL,
                    Username VARCHAR(20) NOT NULL,
                    FirstName VARCHAR(20) NOT NULL,
                    LastName VARCHAR(20) NOT NULL,
                    Email VARCHAR(50) NOT NULL,
                    Verified BOOLEAN DEFAULT FALSE,
                    State VARCHAR(20) NOT NULL,
                    PhoneNumber VARCHAR(20) NOT NULL,
                    Sex VARCHAR(10) NOT NULL,
                    PasswordHash VARCHAR(100) NOT NULL,
                    Weight DECIMAL(5, 2) NOT NULL,
                    Height DECIMAL(5, 2) NOT NULL,
                    DateOfBirth DATE NOT NULL,
                    LastLoggedIn TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
                    CreatedOn TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
                    UpdatedOn TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
                    CONSTRAINT FK_Users_Role FOREIGN KEY (RoleId) REFERENCES Roles(Id) ON DELETE CASCADE
                );";
            
            await connection.ExecuteAsync(createRoleTableQuery);
            _logger.LogInformation("'Roles' table checked/created successfully.");
            
            await connection.ExecuteAsync(createTableQuery);
            _logger.LogInformation("'Users' table checked/created successfully.");
        }
        catch (Exception ex)
        {
            _logger.LogError("Error initializing database: {Message}", ex.Message);
            throw;
        }
    }
}