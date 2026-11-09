using System.Management;
using System.Net;
using System.Net.Sockets;

namespace AssetTracker.Services;

public record HardwareInfo(string SystemName, string Model, string Manufacturer,
                           string AssetType, string IpAddress);

public class HardwareInfoCollector
{
    public HardwareInfo Collect()
    {
        string model = "Unknown", manufacturer = "Unknown";

        try
        {
            using var searcher = new ManagementObjectSearcher(
                "SELECT Manufacturer, Model FROM Win32_ComputerSystem");
            foreach (ManagementObject mo in searcher.Get())
            {
                manufacturer = mo["Manufacturer"]?.ToString() ?? manufacturer;
                model = mo["Model"]?.ToString() ?? model;
            }
        }
        catch (ManagementException) { /* keep "Unknown"; the user can edit the field */ }

        return new HardwareInfo(Environment.MachineName, model, manufacturer,
                                GetAssetType(), GetIpAddress());
    }

    private static string GetAssetType()
    {
        try
        {
            using var searcher = new ManagementObjectSearcher(
                "SELECT ChassisTypes FROM Win32_SystemEnclosure");
            foreach (ManagementObject mo in searcher.Get())
            {
                var types = (ushort[])mo["ChassisTypes"];
                return types[0] switch
                {
                    8 or 9 or 10 or 14 => "Laptop",
                    3 or 4 or 5 or 6 or 7 => "Desktop",
                    17 or 23 => "Server",
                    _ => "Other"
                };
            }
        }
        catch (Exception) { }
        return "Other";
    }

    private static string GetIpAddress()
    {
        try
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            return host.AddressList
                       .FirstOrDefault(a => a.AddressFamily == AddressFamily.InterNetwork)
                       ?.ToString() ?? "Unknown";
        }
        catch (SocketException) { return "Unknown"; }
    }
}