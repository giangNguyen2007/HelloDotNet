using Game.API.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Game.API.Data;

public class GameDBContext : IdentityDbContext<AppUserModel>
{
    public GameDBContext(DbContextOptions<GameDBContext> options) : base(options)
    {

    }
    public DbSet<GameModel> Games {get; set;}
    public DbSet<CommentModel> Comments  {get; set;}
}
