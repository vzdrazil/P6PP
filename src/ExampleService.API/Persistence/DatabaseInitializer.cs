using Dapper;
using MySqlConnector;

namespace ExampleService.API.Persistence;

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

            const string createTableQuery = @"
                CREATE TABLE IF NOT EXISTS Examples (
                    Id INT AUTO_INCREMENT PRIMARY KEY,
                    Name VARCHAR(100) NOT NULL,
                    Description VARCHAR(100) NULL,
                    CreatedAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
                    UpdatedAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP
                );";

            await connection.ExecuteAsync(createTableQuery);

            _logger.LogInformation("'Examples' table checked/created successfully.");
        }
        catch (Exception ex)
        {
            _logger.LogError("Error initializing database: {Message}", ex.Message);
            throw;
        }
    }
}