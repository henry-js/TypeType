using System.Reflection;
using System.Runtime.InteropServices;
using static System.Environment.SpecialFolder;

namespace TypeType.Lib;

public static class BaseDirectories
{
    static BaseDirectories()
    {
        var assemblyName = Path.GetFileNameWithoutExtension(Assembly.GetEntryAssembly()!.ManifestModule.Name);
        var xdgCache = Environment.GetEnvironmentVariable("XDG_CACHE_HOME");
        var xdgConfig = Environment.GetEnvironmentVariable("XDG_CONFIG_HOME");
        var xdgData = Environment.GetEnvironmentVariable("XDG_DATA_HOME");

        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            HomeDir = Environment.GetFolderPath(UserProfile);
            CacheDir = xdgCache is null ? Path.Combine(HomeDir, ".cache", assemblyName) : Path.Combine(xdgCache, assemblyName);
            ConfigDir = xdgConfig is null ? Path.Combine(HomeDir, ".config", assemblyName) : Path.Combine(xdgConfig, assemblyName);
            DataDir = xdgData is null ? Path.Combine(HomeDir, ".local", "share", assemblyName) : Path.Combine(xdgData, assemblyName);
        }
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            HomeDir = Environment.GetFolderPath(LocalApplicationData);
            CacheDir = xdgCache is null ? Path.Combine(HomeDir, assemblyName, ".cache") : Path.Combine(xdgCache, assemblyName);
            ConfigDir = xdgConfig is null ? Path.Combine(HomeDir, assemblyName, ".config") : Path.Combine(xdgConfig, assemblyName);
            DataDir = xdgData is null ? Path.Combine(HomeDir, assemblyName, ".data") : Path.Combine(xdgData, assemblyName);
        }
        else
        {
            throw new PlatformNotSupportedException();
        }

        if (!Directory.Exists(DataDir)) Directory.CreateDirectory(DataDir);
        if (!Directory.Exists(ConfigDir)) Directory.CreateDirectory(ConfigDir);
        if (!Directory.Exists(CacheDir)) Directory.CreateDirectory(CacheDir);
    }

    private static string HomeDir { get; }
    public static string CacheDir { get; }
    public static string ConfigDir { get; }
    public static string DataDir { get; }
}
