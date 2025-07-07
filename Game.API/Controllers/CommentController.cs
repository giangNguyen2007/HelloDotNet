using Game.API.Dtos;
using Game.API.Dtos.Comment;
using Game.API.Interfaces;
using Game.API.Model;
using Microsoft.AspNetCore.Mvc;

namespace Game.API.Controllers;

[Route("/comment")]
[ApiController]
public class CommentController : Controller
{
    private readonly ICommentRepository _commentRepo;
    private readonly IGameRepository _gameRepo;

    public CommentController(ICommentRepository commentRepository, IGameRepository gameRepository)
    {
        _commentRepo = commentRepository;
        _gameRepo = gameRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var comments = await _commentRepo.getAllAsync();

        // Select :  convert list of GameModel to IEnumerable of GetGameDto
        IEnumerable<GetCommentDto> commentDto = comments.Select(s => s.ToGetCommentDto());

        return Ok(commentDto);
    }

    [HttpPost]
    public async Task<IActionResult> PostSingleComment([FromBody] PostCommentDto postCommentDto)
    {
        // create new Game from postGameDto
        bool gameExist = await _gameRepo.gameExistAsync(postCommentDto.GameId);
        if (!gameExist)
        {
            return BadRequest("Game does not exist");
        }
        CommentModel commentModel = postCommentDto.ToCommentModel();

        await _commentRepo.createAsync(commentModel);

        return Ok(commentModel.ToGetCommentDto);
    }

    [HttpPut]
    [Route("{id:int}")]
    public async Task<IActionResult> UpdateSingleComment([FromRoute] int commentId, [FromBody] PutCommentDto putCommentDto)
    {

        CommentModel? commentModel = await _commentRepo.updateAsync(commentId, putCommentDto);

        if (commentModel == null)
        {
            return NotFound();
        }

        return Ok(commentModel.ToGetCommentDto());
    }

    [HttpDelete]
    [Route("{id:int}")]
    public async Task<IActionResult> DeleteSingleComment([FromRoute] int id)
    {

        CommentModel? commentModel = await _commentRepo.deleteAsync(id);

        if (commentModel == null)
        {
            return NotFound();
        }

        return NoContent();
    }


}
