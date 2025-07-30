using ProductAPI.Dtos.Comment;

namespace ProductAPI.Dtos.Game;

public record class GetGameDto
{
    public int Id { get; set; }

    public string Name { get; set; }
    // 
    public string Genre { get; set; }

    public string DtoText { get; set; }
    
    public List<GetCommentDto> comments { get; set; }
};
