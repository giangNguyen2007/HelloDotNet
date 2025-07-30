using Auth.API;
using Auth.API.Model;
using ProductAPI.Data;
using ProductAPI.Model;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace HelloTest.Test.Auth.API;

public class AuthWebAppFactory :  WebApplicationFactory<AuthApiProgram>
{
    private string _connectionString =  "Host=localhost;Port=5432;Database=mydb;Username=myuser;Password=mypassword;";

    public AuthWebAppFactory()
    {
        //_connectionString = postgresFixture._connectionString;
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            // Remove default DbContext
            services.RemoveAll<DbContextOptions<UserDbContext>>();

            // Register with your existing DB
            services.AddDbContext<UserDbContext>(options =>
                options.UseNpgsql(_connectionString));
            
            // Build service provider to run migrations and seed data
            using var sp = services.BuildServiceProvider();
            using var scope = sp.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<UserDbContext>();

            db.Database.Migrate(); // apply migrations
            if (!db.Users.Any())
            {
                db.Users.AddRange(
                    new UserModel { Email = "admin@gmail.com", Password = "admin", Role = "admin"},
                    new UserModel { Email = "rairacer@gmail.com", Password = "rairacer", Role = "user"}
                  
                );
                db.SaveChanges();
            }
        }); // optional
    }
}