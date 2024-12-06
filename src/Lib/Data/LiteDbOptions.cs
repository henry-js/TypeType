namespace TypeType.Lib.Data;

public static class LiteDbOptions
{
    static LiteDbOptions()
    {

        var filePath = Path.Combine(Path.GetDirectoryName(typeof(LiteDbOptions).Assembly.Location)!, "typetype", "typetype.db");

        var dbFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".config", "typetype", "typetype.db");
        if (!Directory.Exists(Path.GetDirectoryName(dbFilePath))) Directory.CreateDirectory(Path.GetDirectoryName(dbFilePath)!);
        ConnectionString = $"Filename={dbFilePath}";

        File.Move(filePath, dbFilePath, true);
    }
    public static string ConnectionString { get; }
}
