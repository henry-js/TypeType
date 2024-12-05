namespace TypeType.Lib.Data;

public class Quote
{
    public int Id { get; set; }
    public string Text { get; set; }
    public string Author { get; set; }
    public string PrimaryTag { get; set; }
    public IEnumerable<string> OtherTags { get; set; }

}
