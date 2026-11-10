using AssetTracker.Models;
using Microsoft.Data.SqlClient;

namespace AssetTracker.Data;

/// <summary>
/// Reads employees from Azure SQL so the 'Add asset form' can offer them in the "assigned to" dropdown.  At this moment in development it is read only for the prototype submission, employee management (add, delete, edit) is planned for next stage. 
/// </summary>

public class SqlEmployeeRepository
{
    /// Connection string supplied by Program.cs (the conjuctor injection).
    
    private readonly string _connectionString;

    public SqlEmployeeRepository(string connectionString) => _connectionString = connectionString;

    /// Returns all employees, sorted alphabetically so it is easy to scan through.
    public List<Employee> GetAll()
    {
        var employees = new List<Employee>();

        /// No user input is involved here so no parameters are needed.
        /// ORDER BY sorts alphabetically by surname first then forename.

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