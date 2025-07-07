using Game.API.AsyncService;
using Game.API.Dtos;
using Game.API.Dtos.Game;
using Game.API.Interfaces;
using Game.API.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Game.API.Controllers;

[Route("/game")]
[ApiController]
public class GameController : Controller
{
    private readonly IGameRepository _gameRepo;
    private readonly IMassTransitService _massTransitService;

    public GameController( IGameRepository gameRepo, IMassTransitService massTransitService)
    {
        _gameRepo = gameRepo;
        _massTransitService = massTransitService;
    }

    //  === GET ====

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        List<GameModel> games = await _gameRepo.getAllAsync();

        // Select :  convert list of GameModel to IEnumerable of GetGameDto
        IEnumerable<GetGameDto> gamesDTO = games.Select(s => s.ToGetGameDto());

        await RabbitMQService.PublishAsync();
        await _massTransitService.PublishMessage("Hello RabbitMQ");
        
        return Ok(gamesDTO);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        await _massTransitService.SendRequestForResponseAsync(1);
        GameModel? game = await _gameRepo.getByIdAsync(id);

        if (game == null)
        {
            return NotFound();
        }

        return Ok(game.ToGetGameDto());
    }

    [HttpPost]
    [Authorize(Policy = "OnlyAdmin")]
    public async Task<IActionResult> PostSingleGame([FromBody] PostGameDto postGameDto)
    {
        // create new Game from postGameDto
        GameModel newGameModel = postGameDto.PostGameDto_to_GameModel();

        await _gameRepo.createAsync(newGameModel);

        return Ok(newGameModel);
    }

    [HttpPut]
    [Route("{id:int}")]
    public async Task<IActionResult> UpdateSingleGame([FromRoute] int id, [FromBody] PutGameDto putGameDto)
    {

        GameModel? gameModel = await _gameRepo.updateAsync(id, putGameDto);

        if (gameModel == null)
        {
            return NotFound();
        }

        return Ok(gameModel.ToGetGameDto());
    }
    
    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> DeleteSingleGame([FromRoute] int id)
    {

        GameModel? gameModel = await _gameRepo.deleteAsync(id);

        if (gameModel == null)
        {
            return NotFound();
        }

        return NoContent();
    }
}
