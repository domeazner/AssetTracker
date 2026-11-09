using AssetTracker.Data;
using AssetTracker.Models;

namespace AssetTracker.Services;

/// <summary>
/// Business logic for assets. Validates input before it is passed to the repository.
/// </summary>
public class AssetService
{
    private readonly IAssetRepository _repository;

    // The repository is passed in (dependency injection), so tests can use a fake one.
    public AssetService(IAssetRepository repository) => _repository = repository;

    public void AddAsset(Asset asset)
    {
        if (string.IsNullOrWhiteSpace(asset.SystemName))
            throw new ArgumentException("System name is required.");

        if (string.IsNullOrWhiteSpace(asset.Model) || string.IsNullOrWhiteSpace(asset.Manufacturer))
            throw new ArgumentException("Model and manufacturer are required.");

        if (asset.EmployeeId <= 0)
            throw new ArgumentException("Please select the employee responsible for this asset.");

        if (asset.PurchaseDate.HasValue && asset.PurchaseDate.Value.Date > DateTime.Today)
            throw new ArgumentException("Purchase date cannot be in the future.");

        _repository.Add(asset);
    }
}