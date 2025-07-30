using ProductAPI;
using ProductAPI.Data;
using ProductAPI.Model;
using HelloTest.Test.Auth.API;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace HelloTest;

public class CustomWebApplicationFactory : WebApplicationFactory<GameApiProgram>
{
    private string _connectionString = "Data Source=GameStore.db";

    public CustomWebApplicationFactory(PostgresFixture postgresFixture)
    {
        _connectionString = postgresFixture._connectionString;
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            // Remove default DbContext
            services.RemoveAll<DbContextOptions<GameDBContext>>();

            // Register with your existing DB
            services.AddDbContext<GameDBContext>(options =>
                options.UseNpgsql(_connectionString));
            
            // Build service provider to run migrations and seed data
            using var sp = services.BuildServiceProvider();
            using var scope = sp.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<GameDBContext>();

            db.Database.Migrate(); // apply migrations
            if (!db.Games.Any())
            {
                db.Games.AddRange(
                    new GameModel {Id = 3, Name = "MariBros", Genre = "Adventure"},
                    new GameModel {Id = 2, Name = "StarCraft", Genre = "Strategy"}
                );
                db.SaveChanges();
            }
        }); // optional
    }
}