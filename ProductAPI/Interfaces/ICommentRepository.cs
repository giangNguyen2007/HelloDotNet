using ProductAPI.Dtos.Comment;
using ProductAPI.Model;

namespace ProductAPI.Interfaces;

public interface ICommentRepository
{
     Task<List<CommentModel>> getAllAsync();

    // note : "?" => meaning could be null, in case not found
    Task<CommentModel?> getByIdAsync(int id);
    Task<CommentModel> createAsync(CommentModel commentModel);
    
    Task<CommentModel?> updateAsync(int id, PutCommentDto putCommentDto);
    Task<CommentModel?> deleteAsync(int id);
}
