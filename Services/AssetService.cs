using AssetTracker.Data;
using AssetTracker.Models;

namespace AssetTracker.Services;

/// <summary>
/// Business logic for assets, it validates input before it is passed to the repository. This is the middle layer: AddAssetForm calls it and it calls the repository. Keeping the rules here (not in the form or the SQL) which means they can be unit tested without a UI or a database.
/// 
/// Workflow of AddAsset:
/// 1) Check each validation rule in turn
/// 2) If any rule fails, throw an ArgumentException with a user-friendly message (the form catches it and displays the message)
/// 3) If all rules pass, hand the asset to the repository to be saved
/// </summary>
public class AssetService
{

    /// Depends on the interface not on SqlAssetRepository so the service never knows which database (or a fake) it is talking to.

    private readonly IAssetRepository _repository;

    /// The repository is passed in (Dependency Injection) so that tests can use a fake one.

    public AssetService(IAssetRepository repository) => _repository = repository;

    public void AddAsset(Asset asset)
    {

        /// Rule 1: the system name must be present. IsNullOrWhiteSpace also rejects empty text and text of only spaces.

        if (string.IsNullOrWhiteSpace(asset.SystemName))
            throw new ArgumentException("System name is required.");

        /// Rule 2: model and manufacturer must be present. These are normally auto-filled, but the user can edit them, so they are checked here too.

        if (string.IsNullOrWhiteSpace(asset.Model) || string.IsNullOrWhiteSpace(asset.Manufacturer))
            throw new ArgumentException("Model and manufacturer are required.");

        /// Rule 3: an employee must be selected. EmployeeId defaults to 0 when nothing is chosen, and real database IDs start at 1.

        if (asset.EmployeeId <= 0)
            throw new ArgumentException("Please select the employee responsible for this asset.");

        /// Rule 4: the purchase date is optional, but if given it cannot be in the future. HasValue checks that a date was entered; .Date ignores the time part so that today's date is allowed.

        if (asset.PurchaseDate.HasValue && asset.PurchaseDate.Value.Date > DateTime.Today)
            throw new ArgumentException("Purchase date cannot be in the future.");


        /// All rules passed, so save the asset. Database errors (for example no connection) are not caught here; they pass up to the form, which shows an error message.

        _repository.Add(asset);
    }
}