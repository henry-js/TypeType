namespace TypeType.Lib.Data;

public static class LiteDbOptions
{
    static LiteDbOptions()
    {
        var configFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".config", "typetype");
        string fileName = "typetype.db";
        ConnectionString = $"Filename={Path.Combine(configFolder, fileName)}";
    }
    public static string ConnectionString { get; }
}
