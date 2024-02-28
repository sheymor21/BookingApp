using Microsoft.EntityFrameworkCore;

namespace test.DatabaseContext;

public class DatabaseContextFixture
{
    public readonly Infrastructure.Context.DatabaseAppContext AppContext;

    public DatabaseContextFixture()
    {
        var builder = new DbContextOptionsBuilder<Infrastructure.Context.DatabaseAppContext>()
            .UseInMemoryDatabase("Database");
        var options = builder.Options;
        AppContext = new Infrastructure.Context.DatabaseAppContext(options);
        AppContext.Database.EnsureCreated();
        AppContext.Database.EnsureDeleted();
    }
}