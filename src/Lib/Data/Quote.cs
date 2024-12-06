using System.Text.Json.Serialization;

namespace TypeType.Lib.Data;

public class Quote
{
    public int Id { get; set; }
    public required string Text { get; set; }
    public required string Author { get; set; }
    public IEnumerable<string> Tags { get; set; } = [];
    public int WordCount { get; set; }
    public int CharCount { get; set; }
}
