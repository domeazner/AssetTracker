using AssetTracker.Data;
using AssetTracker.Models;
using AssetTracker.UI;
using Microsoft.Extensions.Configuration;

namespace AssetTracker;

static class Program
{
    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();

        Application.Run(new AddAssetForm());
    }
}