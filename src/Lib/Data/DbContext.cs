using LiteDB;

namespace TypeType.Lib.Data;

public class DbContext
{
    private readonly string connectionString;
    public DbContext(string connectionString)
    {
        this.connectionString = connectionString;
    }
    public IEnumerable<Quote> GetQuotes()
    {
        using var db = new LiteRepository(connectionString);

        return db.Query<Quote>()
            .ToList();
    }
    public void InsertQuotes(IEnumerable<Quote> quotes)
    {
        using var db = new LiteRepository(connectionString);

        db.Insert(quotes);
    }
}
