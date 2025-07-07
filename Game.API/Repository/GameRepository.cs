using Game.API.Data;
using Game.API.Dtos.Game;
using Game.API.Interfaces;
using Game.API.Model;
using Microsoft.EntityFrameworkCore;

namespace Game.API.Repository;

public class GameRepository : IGameRepository
{
    private readonly GameDBContext _context;

    public GameRepository(GameDBContext context)
    {
        _context = context;
    }


    public async Task<List<GameModel>> getAllAsync()
    {
        return await _context.Games.Include(c => c.Comments).ToListAsync();
    }

    public async Task<GameModel?> getByIdAsync(int id)
    {
        var gameModel = await _context.Games.Include(c => c.Comments).FirstOrDefaultAsync(c => c.Id == id);
        if (gameModel == null)
        {
            return null;
        }
        return gameModel;
    }

    public async Task<GameModel> createAsync(GameModel gameModel)
    {
        await _context.Games.AddAsync(gameModel);
        await _context.SaveChangesAsync();

        return gameModel;
    }

    public async Task<GameModel?> updateAsync(int id, PutGameDto putGameDto)
    {
        var gameModel = await _context.Games.FirstOrDefaultAsync(g => g.Id == id);
        if (gameModel == null)
        {
            return null;
        }

        gameModel.Name = putGameDto.Name;
        gameModel.Genre = putGameDto.Genre;

        await _context.SaveChangesAsync();

        return gameModel;
    }
    public async Task<GameModel?> deleteAsync(int id)
    {
        var gameModel = await _context.Games.FirstOrDefaultAsync(g => g.Id == id);
        if (gameModel == null)
        {
            return null;
        }

        // Note: remove is not an async function, leave it like this
        _context.Games.Remove(gameModel);
        await _context.SaveChangesAsync();

        return gameModel;
    }

    public Task<bool> gameExistAsync(int id)
    {
        return _context.Games.AnyAsync(g => g.Id == id);
    }
}
