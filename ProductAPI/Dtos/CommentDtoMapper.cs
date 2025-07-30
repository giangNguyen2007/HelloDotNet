using ProductAPI.Dtos.Comment;
using ProductAPI.Model;

namespace ProductAPI.Dtos;

public static class CommentDtoMapper
{
    public static GetCommentDto ToGetCommentDto(this CommentModel commentModel)
    {
        return new GetCommentDto
        {
            Id = commentModel.Id,
            Content = commentModel.Content,
            GameId = commentModel.GameId
        };
    }

    public static CommentModel ToCommentModel(this PostCommentDto postCommentDto)
    {
        return new CommentModel
        {
            Content = postCommentDto.Content,
            GameId = postCommentDto.GameId
        };
    }
}
