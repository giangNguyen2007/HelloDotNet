using Game.API.Dtos.Game;
using Game.API.Model;

namespace Game.API.Dtos;

public static class GameDtoMapper
{
    // convert from GameModel to GameDto
    public static GetGameDto ToGetGameDto(this GameModel gameModel)
    {
        return new GetGameDto
        {
            Id = gameModel.Id,
            Name = gameModel.Name,
            Genre = gameModel.Genre,
            DtoText = "GetDTO",
            comments = gameModel.Comments.Select(c => c.ToGetCommentDto()).ToList()
        };
    }

    public static GameModel PostGameDto_to_GameModel(this PostGameDto postGameDto)
    {
        return new GameModel
        {
            Name = postGameDto.Name,
            Genre = postGameDto.Genre
        };
    }
}
