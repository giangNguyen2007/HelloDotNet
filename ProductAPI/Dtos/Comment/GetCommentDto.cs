namespace ProductAPI.Dtos.Comment;

public class GetCommentDto
{
    public int Id { get; set; }

    public string Content { get; set; } = string.Empty;

    public int? GameId { get; set; }
}
