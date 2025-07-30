using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProductAPI.Model;

namespace ProductAPI.Data;

public class GameDBContext : DbContext
{
    public GameDBContext(DbContextOptions<GameDBContext> options) : base(options)
    {

    }
    public DbSet<GameModel> Games {get; set;}
    public DbSet<CommentModel> Comments  {get; set;}
}
