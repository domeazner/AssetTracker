using AssetTracker.Data;
using AssetTracker.Services;
using AssetTracker.UI;
using Microsoft.Extensions.Configuration;

namespace AssetTracker;

static class Program
{
    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();

        var config = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: false)
            .Build();
        string connectionString = config.GetConnectionString("AssetDb")!;

        var employeeRepository = new SqlEmployeeRepository(connectionString);
        var assetService = new AssetService(new SqlAssetRepository(connectionString));

        Application.Run(new AddAssetForm(assetService, new HardwareInfoCollector(), employeeRepository));
    }
}