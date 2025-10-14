using System;
using System.Collections.Generic;
using System.Management;
using System.Text.RegularExpressions;

namespace MakuTweakerNew
{
    public class StorageInfo
    {
        public string Name { get; set; } = string.Empty;

        public ulong CapacityBytes { get; set; }

        public string CapacityFormatted => FormatBytes(CapacityBytes);

        public string DevicePath { get; set; } = string.Empty;

        public ulong TotalBytesWritten { get; set; }

        public string TotalBytesWrittenFormatted => FormatBytes(TotalBytesWritten);

        private static string FormatBytes(ulong bytes)
        {
            string[] sizes = new string[] { "B", "KB", "MB", "GB", "TB" };
            double len = bytes;
            int order = 0;
            if (bytes == 0)
            {
                return "N/A";
            }
            while (len >= 1024.0 && order < sizes.Length - 1)
            {
                order++;
                len /= 1024.0;
            }
            return $"{len:0.##} {sizes[order]}";
        }
    }

    public static class StorageHelper
    {
        public static List<StorageInfo> GetAllStorageDevices()
        {
            List<StorageInfo> devices = new List<StorageInfo>();
            Dictionary<string, ulong> diskWrites = new Dictionary<string, ulong>();

            try
            {
                ManagementScope scope = new ManagementScope("\\\\.\\root\\microsoft\\windows\\storage");
                scope.Connect();
                using ManagementObjectSearcher searcher = new ManagementObjectSearcher(scope, new ObjectQuery("SELECT DeviceId, TotalBytesWritten FROM MSFT_PhysicalDisk"));
                foreach (ManagementBaseObject obj in searcher.Get())
                {
                    string deviceId = obj["DeviceId"]?.ToString() ?? string.Empty;
                    ulong bytesWritten = 0uL;
                    if (obj["TotalBytesWritten"] != null)
                    {
                        bytesWritten = Convert.ToUInt64(obj["TotalBytesWritten"]);
                    }
                    if (!string.IsNullOrEmpty(deviceId))
                    {
                        diskWrites[deviceId] = bytesWritten;
                    }
                }
            }
            catch (Exception) { }

            try
            {
                using ManagementObjectSearcher searcher2 = new ManagementObjectSearcher("SELECT Caption, Size, DeviceID FROM Win32_DiskDrive");
                foreach (ManagementBaseObject obj2 in searcher2.Get())
                {
                    string name = obj2["Caption"]?.ToString() ?? "Unknown Device";
                    ulong size = ((obj2["Size"] != null) ? Convert.ToUInt64(obj2["Size"]) : 0);
                    string deviceID = obj2["DeviceID"]?.ToString() ?? "";
                    if (size != 0L && !name.Contains("Virtual", StringComparison.OrdinalIgnoreCase) && !name.Contains("iSCSI", StringComparison.OrdinalIgnoreCase))
                    {
                        string diskIndex = Regex.Match(deviceID, "\\d+$").Value;
                        ulong totalWrites = 0uL;
                        if (!string.IsNullOrEmpty(diskIndex) && diskWrites.ContainsKey(diskIndex))
                        {
                            totalWrites = diskWrites[diskIndex];
                        }
                        string pathForCpp = deviceID.Replace("PHYSICALDRIVE", "PhysicalDrive", StringComparison.OrdinalIgnoreCase);
                        devices.Add(new StorageInfo
                        {
                            Name = name,
                            CapacityBytes = size,
                            DevicePath = pathForCpp,
                            TotalBytesWritten = totalWrites
                        });
                    }
                }
            }
            catch (Exception) { }

            return devices;
        }
    }
}
