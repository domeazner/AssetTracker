using System.Management;
using System.Net;
using System.Net.Sockets;

namespace AssetTracker.Services;

/// <summary>
/// A simple read only bundle of details collected from this computer. A "record" is a compact immutable class: the values are set once when it is created and cannot be changed, which suits data that is read and then displayed to the user.
/// </summary>

public record HardwareInfo(string SystemName, string Model, string Manufacturer,
                           string AssetType, string IpAddress);

/// <summary>
/// Reads details about the machine the application is running on so the user does not have to type them in. The form calls Collect() when it opens and uses the result to pre fill the relevant fields. It uses WMI  which lets the program query Windows for hardware information using SQL like queries. Every lookup is wrapped in try/catch and falls back to a default value so a failed lookup never stops the form from opening.
/// </summary>

public class HardwareInfoCollector
{

    /// Gathers all the details and returns them as one HardwareInfo object.

    public HardwareInfo Collect()
    {

        /// Defaults used if WMI fails or returns nothing.

        string model = "Unknown", manufacturer = "Unknown";

        try
        {

            /// Query the Win32_ComputerSystem WMI class for the maker and model. "using" releases the WMI resources when finished.

            using var searcher = new ManagementObjectSearcher(
                "SELECT Manufacturer, Model FROM Win32_ComputerSystem");

            /// The query returns a collection.

            foreach (ManagementObject mo in searcher.Get())
            {

                /// ?.ToString() ?? fallback: if the value is null it keep the default.

                manufacturer = mo["Manufacturer"]?.ToString() ?? manufacturer;
                model = mo["Model"]?.ToString() ?? model;
            }
        }
        catch (ManagementException) { /* keep "Unknown"; the user can edit the field */ }


        /// The system name comes straight from .NET (computer name). The asset type and IP address are worked out by the helper methods below.

        return new HardwareInfo(Environment.MachineName, model, manufacturer,
                                GetAssetType(), GetIpAddress());
    }


    /// Works out whether the current machine is a desktop, laptop or server.

    private static string GetAssetType()
    {
        try
        {

            /// Win32_SystemEnclosure describes the physical case. ChassisTypes is a list of numeric codes defined by the DMTF (Distributed Management Task Force) hardware standard.

            using var searcher = new ManagementObjectSearcher(
                "SELECT ChassisTypes FROM Win32_SystemEnclosure");
            foreach (ManagementObject mo in searcher.Get())
            {
                var types = (ushort[])mo["ChassisTypes"];

                /// Translate the first code into a readable type:
                /// 8, 9, 10, 14   = portable, laptop, notebook, sub-notebook
                /// 3, 4, 5, 6, 7  = desktop, low-profile, pizza box, mini tower, tower
                /// 17, 23         = server chassis, rack-mount
                /// Anything else (virtual machines, tablets, etc.) becomes "Other".

                return types[0] switch
                {
                    8 or 9 or 10 or 14 => "Laptop",
                    3 or 4 or 5 or 6 or 7 => "Desktop",
                    17 or 23 => "Server",
                    _ => "Other"
                };
            }
        }
        catch (Exception) { }  /// Any failure falls through to the default below.
        return "Other";
    }


    /// Finds the machine's IPv4 address.

    private static string GetIpAddress()
    {
        try
        {

            /// Ask DNS for all the addresses belonging to the computer's host name.

            var host = Dns.GetHostEntry(Dns.GetHostName());

            /// Take the first IPv4 address (AddressFamily.InterNetwork means IPv4). If none is found FirstOrDefault returns null and "Unknown" is used.

            return host.AddressList
                       .FirstOrDefault(a => a.AddressFamily == AddressFamily.InterNetwork)
                       ?.ToString() ?? "Unknown";
        }
        catch (SocketException) { return "Unknown"; }  /// e.g. no network adapter
    }
}