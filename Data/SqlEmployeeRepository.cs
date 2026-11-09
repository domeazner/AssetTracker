using AssetTracker.Models;
using Microsoft.Data.SqlClient;

namespace AssetTracker.Data;

public class SqlEmployeeRepository
{
    private readonly string _connectionString;

    public SqlEmployeeRepository(string connectionString) => _connectionString = connectionString;

    public List<Employee> GetAll()
    {
        var employees = new List<Employee>();
        const string sql = @"SELECT EmployeeId, FirstName, LastName, EmailAddress, DepartmentId
                             FROM Employee ORDER BY LastName, FirstName";

        using var conn = new SqlConnection(_connectionString);
        using var cmd = new SqlCommand(sql, conn);
        conn.Open();
        using var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            employees.Add(new Employee
            {
                EmployeeId = reader.GetInt32(0),
                FirstName = reader.GetString(1),
                LastName = reader.GetString(2),
                EmailAddress = reader.GetString(3),
                DepartmentId = reader.GetInt32(4)
            });
        }
        return employees;
    }
}