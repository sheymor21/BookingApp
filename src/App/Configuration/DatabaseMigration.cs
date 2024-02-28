using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace BookingApp.Configuration;

public static class DatabaseInitialization
{
    public static void Migrations(this IApplicationBuilder app)
    {
        using var serviceScope = app.ApplicationServices.CreateScope();
        serviceScope.ServiceProvider.GetService<DatabaseAppContext>()?.Database.Migrate();
    }
}