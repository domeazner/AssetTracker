using AssetTracker.Models;
using Microsoft.Data.SqlClient;

namespace AssetTracker.Data;

/// <summary>
/// Azure SQL implementation of IAssetRepository, this is the only class that knows how assets are written to the database, so the SQL assets are all in one place.
/// Addition workflow:
/// 1) Define the INSERT statement with named parameters
/// 2) Open a connection Azure SQL
/// 3) Fill in the parameters from the Asset object
/// 4) Execute the command, which insets one row into the Asset table
/// <summary>

public class SqlAssetRepository : IAssetRepository
{
    /// Connection string received from Program.cs (constructor injection), it's not hard coded here.
    private readonly string _connectionString;

    public SqlAssetRepository(string connectionString) => _connectionString = connectionString;

    public void Add(Asset asset)
    {
        /// Parameterised SQL values are sent separately from the query text using @placeholders. Looking for future security issues this prevents SQL injections due to the user input e.g. the Notes field can never be run as SQL code.

        const string sql = @"
            INSERT INTO Asset (AssetId, SystemName, Model, Manufacturer, AssetType,
                               IpAddress, PurchaseDate, Notes, EmployeeId)
            VALUES (@AssetId, @SystemName, @Model, @Manufacturer, @AssetType,
                    @IpAddress, @PurchaseDate, @Notes, @EmployeeId)";

        /// "using" discards the connection & command automatically, even if an exception is thrown so connections are always released.
        
        using var conn = new SqlConnection(_connectionString);
        using var cmd = new SqlCommand(sql, conn);
        
        /// Map each property of the Asset object to its matching SQL parameter.
        
        cmd.Parameters.AddWithValue("@AssetId", asset.AssetId);
        cmd.Parameters.AddWithValue("@SystemName", asset.SystemName);
        cmd.Parameters.AddWithValue("@Model", asset.Model);
        cmd.Parameters.AddWithValue("@Manufacturer", asset.Manufacturer);
        cmd.Parameters.AddWithValue("@AssetType", asset.AssetType);
        cmd.Parameters.AddWithValue("@IpAddress", asset.IpAddress);

        /// Notes and PurchaseDate are optional, A C# null cannot be sent to SQL directly so it is converted into DBNull.Value, which will be stored as NULL.

        cmd.Parameters.AddWithValue("@PurchaseDate", (object?)asset.PurchaseDate ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@Notes", (object?)asset.Notes ?? DBNull.Value);

        /// Foreign key linking the asset to its employee.

        cmd.Parameters.AddWithValue("@EmployeeId", asset.EmployeeId);

        /// The connection is opened as late as it is possible and just before it is needed. The Azure free/student tier is where the paused database 'wakes up' so the first call is almost always slow and can and does often throw an error that the form handles.

        conn.Open();

        /// ExecuteNonQuery runs a statement that returns no rows (INSERT/UPDATE/DELETE).

        cmd.ExecuteNonQuery();
    }
}