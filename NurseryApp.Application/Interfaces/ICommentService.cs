using NurseryApp.Application.Dtos.Comment;

namespace NurseryApp.Application.Interfaces
{
    public interface ICommentService
    {
        Task<int> Create(CommentCreateDto commentCreateDto);
        Task<int> Update(int? id, CommentUpdateDto commentUpdateDto);
        Task<IEnumerable<CommentReturnDto>> GetAll(int? blogId);
        Task<CommentReturnDto> Get(int? id);
        Task<int> Delete(int? id);
    }
}
