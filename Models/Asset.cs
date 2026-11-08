namespace AssetTracker.Models;

public class Asset
{
    public Guid AssetId { get; set; } = Guid.NewGuid();
    public string SystemName { get; set; } = "";
    public string Model { get; set; } = "";
    public string Manufacturer { get; set; } = "";
    public string AssetType { get; set; } = "";
    public string IpAddress { get; set; } = "";
    public DateTime? PurchaseDate { get; set; }
    public string? Notes { get; set; }
    public int EmployeeId { get; set; }
}