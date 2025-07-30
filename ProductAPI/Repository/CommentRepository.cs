using Microsoft.EntityFrameworkCore;
using ProductAPI.Data;
using ProductAPI.Dtos.Comment;
using ProductAPI.Interfaces;
using ProductAPI.Model;

namespace ProductAPI.Repository;

public class CommentRepository : ICommentRepository
{
    private readonly GameDBContext _context;

     public CommentRepository(GameDBContext context)
    {
        _context = context;
    }

    public async Task<CommentModel> createAsync(CommentModel commentModel)
    {
        await _context.Comments.AddAsync(commentModel);
        await _context.SaveChangesAsync();

        return commentModel;
    }

    public async Task<CommentModel?> deleteAsync(int id)
    {
        CommentModel? commentModel = await _context.Comments.FirstOrDefaultAsync(g => g.Id == id);
        if (commentModel == null)
        {
            return null;
        }

        // Note: remove is not an async function, leave it like this
        _context.Comments.Remove(commentModel);
        await _context.SaveChangesAsync();

        return commentModel;
    }

    public async Task<List<CommentModel>> getAllAsync()
    {
        return await _context.Comments.ToListAsync();
    }

    public async Task<CommentModel?> getByIdAsync(int id)
    {
        CommentModel? commentModel = await _context.Comments.FindAsync(id);
        if (commentModel == null)
        {
            return null;
        }
        return commentModel;
    }

    public async Task<CommentModel?> updateAsync(int commentId, PutCommentDto putCommentDto)
    {
        CommentModel? commentModel = await _context.Comments.FirstOrDefaultAsync(g => g.Id == commentId);
        if (commentModel == null)
        {
            return null;
        }

        commentModel.Content = putCommentDto.Content;

        await _context.SaveChangesAsync();

        return commentModel;
    }
}
