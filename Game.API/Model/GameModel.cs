namespace Game.API.Model;

public class GameModel
{
    public int Id { get; set; }

    public required string Name { get; set; }

    // Validate by annotation
    // 
    public required string Genre { get; set; }

    public List<CommentModel> Comments { get; set; } = new List<CommentModel>();
}
