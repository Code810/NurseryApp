using NurseryApp.Application.Dtos.Blog;

namespace NurseryApp.Application.Interfaces
{
    public interface IBlogService
    {
        Task<int> Create(BlogCreateDto blogCreateDto);
        Task<int> Update(int? id, BlogUpdateDto blogUpdateDto);
        Task<IEnumerable<BlogReturnDto>> GetAll(int? count);
        Task<BlogReturnDto> Get(int? id);
        Task<int> Delete(int? id);
    }
}
