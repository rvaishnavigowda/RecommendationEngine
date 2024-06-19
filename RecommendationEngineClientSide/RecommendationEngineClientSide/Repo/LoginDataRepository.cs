using Microsoft.Data.Sqlite;
using System;
using System.Threading.Tasks;

public class LoginDateRepository
{
    private readonly string _connectionString;

    public LoginDateRepository(string databasePath)
    {
        _connectionString = $"Data Source={databasePath}";
        InitializeDatabase();
    }

    // Method to initialize the database
    private void InitializeDatabase()
    {
        using (var connection = new SqliteConnection(_connectionString))
        {
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText =
            @"
                CREATE TABLE IF NOT EXISTS ChefLoginDates (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Username TEXT NOT NULL,
                    LoginDate TEXT NOT NULL
                );
            ";
            command.ExecuteNonQuery();
        }
    }

    // Method to save the login date
    public async Task SaveLoginDateAsync(string username, DateTime loginDate)
    {
        using (var connection = new SqliteConnection(_connectionString))
        {
            await connection.OpenAsync();
            var command = connection.CreateCommand();
            command.CommandText =
            @"
                INSERT INTO ChefLoginDates (Username, LoginDate)
                VALUES ($username, $loginDate);
            ";
            command.Parameters.AddWithValue("$username", username);
            command.Parameters.AddWithValue("$loginDate", loginDate.ToString("yyyy-MM-dd HH:mm:ss"));
            await command.ExecuteNonQueryAsync();
        }
    }

    // Method to get the last login date
    public async Task<DateTime?> GetLastLoginDateAsync(string username)
    {
        using (var connection = new SqliteConnection(_connectionString))
        {
            await connection.OpenAsync();
            var command = connection.CreateCommand();
            command.CommandText =
            @"
                SELECT LoginDate
                FROM ChefLoginDates
                WHERE Username = $username
                ORDER BY Id DESC
                LIMIT 1;
            ";
            command.Parameters.AddWithValue("$username", username);
            var result = await command.ExecuteScalarAsync();
            if (result != null)
            {
                return DateTime.Parse(result.ToString());
            }
            return null;
        }
    }
}
