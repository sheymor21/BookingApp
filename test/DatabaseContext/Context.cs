using Microsoft.EntityFrameworkCore;

namespace test.DatabaseContext;

public class DatabaseContextFixture
{
    public readonly DatabaseAppContext AppContext;

    public DatabaseContextFixture()
    {
        var builder = new DbContextOptionsBuilder<DatabaseAppContext>()
            .UseInMemoryDatabase("Database");
        var options = builder.Options;
        AppContext = new DatabaseAppContext(options);
        AppContext.Database.EnsureCreated();
        AppContext.Database.EnsureDeleted();
    }
}