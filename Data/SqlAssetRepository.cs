using AssetTracker.Models;
using Microsoft.Data.SqlClient;

namespace AssetTracker.Data;

public class SqlAssetRepository : IAssetRepository
{
    private readonly string _connectionString;

    public SqlAssetRepository(string connectionString) => _connectionString = connectionString;

    public void Add(Asset asset)
    {
        const string sql = @"
            INSERT INTO Asset (AssetId, SystemName, Model, Manufacturer, AssetType,
                               IpAddress, PurchaseDate, Notes, EmployeeId)
            VALUES (@AssetId, @SystemName, @Model, @Manufacturer, @AssetType,
                    @IpAddress, @PurchaseDate, @Notes, @EmployeeId)";

        using var conn = new SqlConnection(_connectionString);
        using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@AssetId", asset.AssetId);
        cmd.Parameters.AddWithValue("@SystemName", asset.SystemName);
        cmd.Parameters.AddWithValue("@Model", asset.Model);
        cmd.Parameters.AddWithValue("@Manufacturer", asset.Manufacturer);
        cmd.Parameters.AddWithValue("@AssetType", asset.AssetType);
        cmd.Parameters.AddWithValue("@IpAddress", asset.IpAddress);
        cmd.Parameters.AddWithValue("@PurchaseDate", (object?)asset.PurchaseDate ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@Notes", (object?)asset.Notes ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@EmployeeId", asset.EmployeeId);

        conn.Open();
        cmd.ExecuteNonQuery();
    }
}