using ProductAPI.AsyncService;
using ProductAPI.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductAPI.Dtos.Game;
using ProductAPI.Interfaces;
using ProductAPI.Model;

namespace ProductAPI.Controllers;

[Route("/game")]
[ApiController]
public class GameController : Controller
{
    private readonly IGameRepository _gameRepo;
  

    public GameController( IGameRepository gameRepo)
    {
        _gameRepo = gameRepo;
 
    }

    //  === GET ====

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        List<GameModel> games = await _gameRepo.getAllAsync();

        // Select :  convert list of GameModel to IEnumerable of GetGameDto
        IEnumerable<GetGameDto> gamesDTO = games.Select(s => s.ToGetGameDto());
        
        return Ok(gamesDTO);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        //await _massTransitService.SendRequestForResponseAsync(1);
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
