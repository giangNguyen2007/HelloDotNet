using Game.API.Dtos.Game;
using Game.API.Model;

namespace Game.API.Interfaces;

public interface IGameRepository
{
    Task<List<GameModel>> getAllAsync();

    // note : "?" => meaning could be null, in case not found
    Task<GameModel?> getByIdAsync(int id);
    Task<GameModel> createAsync(GameModel gameModel);

    Task<GameModel?> updateAsync(int id, PutGameDto putGameDto);
    Task<GameModel?> deleteAsync(int id);

    Task<bool> gameExistAsync(int id);
}
