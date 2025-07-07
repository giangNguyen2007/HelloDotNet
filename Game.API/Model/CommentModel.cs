namespace Game.API.Model;

public class CommentModel
{
    public int Id { get; set; }

    public string Content { get; set; } = string.Empty;

    // Validate by annotation
    // 
    public int? GameId { get; set; }
    
    public GameModel? Game { get; set; }
}
