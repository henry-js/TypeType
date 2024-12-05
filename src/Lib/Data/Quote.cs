using System.Text.Json.Serialization;

namespace TypeType.Lib.Data;

public class Quote
{
    public int Id { get; set; }
    [JsonPropertyName("quote")]
    public string Text { get; set; }
    public string Author { get; set; }
    [JsonPropertyName("category")]
    public IEnumerable<string> Tags { get; set; }

}
