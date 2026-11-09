using AssetTracker.Models;

namespace AssetTracker.Data;

public interface IAssetRepository
{
	void Add(Asset asset);
}