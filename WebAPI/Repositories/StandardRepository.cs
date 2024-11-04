using Npgsql;

namespace WebAPI.Repositories;

public class StandardRepository : IStandardRepository
{
    private readonly NpgsqlConnection connection;
    public StandardRepository(IConfiguration configuration)
    {
        string? connectionString = configuration.GetConnectionString("POSTGRESQL_CONNECTION_STRING");
        if (string.IsNullOrEmpty(connectionString)) throw new Exception("PostgreSQL connection string is not defined.");
        NpgsqlDataSource dataSource = NpgsqlDataSource.Create(connectionString);
        this.connection = dataSource.OpenConnection();
    }

    public void AddStandard(int standard)
    {
        if (!Enumerable.Range(1, 12).Contains(standard)) throw new Exception("Standard must be between 1 to 12.");
        if (IsStandardExists(standard)) throw new Exception($"Standard {standard} already exists.");
        NpgsqlCommand addStandardCommand = new("INSERT INTO t_standards VALUES(@standard)", connection);
        addStandardCommand.Parameters.AddWithValue("standard", standard.ToString());
        addStandardCommand.ExecuteNonQuery();
    }

    public List<string> GetStandards()
    {
        List<string> standards = [];
        NpgsqlCommand getStandardsCommand = new("SELECT c_standard from t_standards ORDER BY CAST(c_standard as int)", connection);
        using NpgsqlDataReader reader = getStandardsCommand.ExecuteReader();
        while (reader.Read())
        {
            standards.Add(reader.GetString(0));
        }
        return standards;
    }

    private bool IsStandardExists(int standard)
    {
        NpgsqlCommand getStandardCommand = new("SELECT * from t_standards WHERE c_standard = @standard", connection);
        getStandardCommand.Parameters.AddWithValue("standard", standard.ToString());
        using NpgsqlDataReader reader = getStandardCommand.ExecuteReader();
        return reader.HasRows;
    }

    public void RemoveStandard(int standard)
    {
        if (!IsStandardExists(standard)) throw new Exception($"Standard {standard} doesn't exists.");
        NpgsqlCommand deleteStandardCommand = new("DELETE FROM t_standards WHERE c_standard = @standard", connection);
        deleteStandardCommand.Parameters.AddWithValue("standard", standard.ToString());
        deleteStandardCommand.ExecuteNonQuery();
    }
}