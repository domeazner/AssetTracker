namespace AssetTracker.Models;

/// <summary>
/// This represents one tracked device, it mirrors the Asset table in the database. The form builds one of these, AssetService validates it and SqlAssetRepository saves it. It holds data only and contains no logic.
/// </summary>

public class Asset
{

    /// Unique ID generated in C# as soon as object is created, so the ID is known before the database is contacted. Matches the UNIQUEIDENTIFIER column.

    public Guid AssetId { get; set; } = Guid.NewGuid();

    /// Hardware details auto filled from the machine by HardwareInfoCollector. Defaulting strings to "" avoids null reference errors before they are set.

    public string SystemName { get; set; } = "";
    public string Model { get; set; } = "";
    public string Manufacturer { get; set; } = "";
    public string AssetType { get; set; } = "";
    public string IpAddress { get; set; } = "";

    /// Optional fields, The ? makes the type nullable, so "no value" is allowed. The repository converts null to DBNull when saving, and the database stores it as NULL.

    public DateTime? PurchaseDate { get; set; }
    public string? Notes { get; set; }

    /// Foreign key: the employee this asset is assigned to. It stores only the ID, not the whole Employee object which keeps the model simple and matches how the database stores the relationship.

    public int EmployeeId { get; set; }
}