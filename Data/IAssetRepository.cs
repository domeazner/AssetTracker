using AssetTracker.Models;

namespace AssetTracker.Data;

/// <summary>
/// Contract for saving assets in the repository pattern.
/// AssetService is dependant on this interface not the SQL Server, so storage can be swapped without changing any business logic.
/// Because only addition is needed for the Part 2 prototype, view, edit & delete will be added here in Part 3 construction.
/// </summary>

public interface IAssetRepository
{ 
	
	/// Stores a new asset, implementation decide where & how it is persisted.
	void Add(Asset asset);
}