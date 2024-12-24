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

    public Quote GetRandom()
    {
        using var db = new LiteRepository(connectionString);

        var max = db.Query<Quote>().Count();

        return db.Query<Quote>().Where(x => x.Id == Random.Shared.Next(1, max)).First();

    }
    public void InsertQuotes(IEnumerable<Quote> quotes)
    {
        using var db = new LiteRepository(connectionString);

        db.Insert(quotes);
    }
}
