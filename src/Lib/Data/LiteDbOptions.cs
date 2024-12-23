﻿namespace TypeType.Lib.Data;

public static class LiteDbOptions
{
    static LiteDbOptions()
    {
        var filePath = Path.Combine(BaseDirectories.DataDir, "typetype.db");

        ConnectionString = $"Filename={filePath}";
    }
    public static string ConnectionString { get; }
}
