using System;
using System.Collections.Generic;
using System.Management;
using SharpGen.Runtime;
using Vortice.DXGI;

namespace MakuTweakerNew
{
    public class GpuInfo
    {
        public string Name { get; set; } = string.Empty;

        public ulong VRamBytes { get; set; }

        public string VRamFormatted => FormatBytes(VRamBytes);

        public string LHMName { get; set; } = string.Empty;

        private static string FormatBytes(ulong bytes)
        {
            string[] sizes = new string[] { "B", "KB", "MB", "GB", "TB" };
            double len = bytes;
            int order = 0;
            while (len >= 1024.0 && order < sizes.Length - 1)
            {
                order++;
                len /= 1024.0;
            }
            return $"{len:0.##} {sizes[order]}";
        }
    }

    public static class GpuHelper
    {
        public static List<GpuInfo> GetAllGpus()
        {
            List<GpuInfo> gpus = new List<GpuInfo>();
            try
            {
                using IDXGIFactory1 factory = DXGI.CreateDXGIFactory1<IDXGIFactory1>();
                int i = 0;
                while (true)
                {
                    try
                    {
                        factory.EnumAdapters1((uint)i, out var adapter);
                        if (adapter == null)
                        {
                            break;
                        }

                        using (adapter)
                        {
                            AdapterDescription1 desc = adapter.Description1;
                            string name = desc.Description?.Trim() ?? "";
                            if (name.Contains("Microsoft Basic Render Driver", StringComparison.OrdinalIgnoreCase))
                            {
                                i++;
                                continue;
                            }
                            if (string.IsNullOrWhiteSpace(name) || name.Equals("Null", StringComparison.OrdinalIgnoreCase))
                            {
                                i++;
                                continue;
                            }
                            gpus.Add(new GpuInfo
                            {
                                Name = name,
                                VRamBytes = desc.DedicatedVideoMemory
                            });
                        }
                        i++;
                        continue;
                    }
                    catch (SharpGenException) { }
                    break;
                }
            }
            catch (Exception)
            {
                return FallbackToWmi();
            }
            return (gpus.Count > 0) ? gpus : FallbackToWmi();
        }

        private static List<GpuInfo> FallbackToWmi()
        {
            List<GpuInfo> gpus = new List<GpuInfo>();
            try
            {
                using ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT Name, AdapterRAM FROM Win32_VideoController");
                foreach (ManagementBaseObject obj in searcher.Get())
                {
                    string name = obj["Name"]?.ToString() ?? "Unknown GPU";
                    ulong vram = ((obj["AdapterRAM"] != null) ? Convert.ToUInt64(obj["AdapterRAM"]) : 0);
                    gpus.Add(new GpuInfo
                    {
                        Name = name,
                        VRamBytes = vram
                    });
                }
            }
            catch { }

            return gpus;
        }
    }
}
