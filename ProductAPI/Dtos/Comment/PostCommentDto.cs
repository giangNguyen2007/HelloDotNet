namespace ProductAPI.Dtos.Comment;

public class PostCommentDto
{
    public string Content { get; set; } = string.Empty;

    public int GameId { get; set; }
}
