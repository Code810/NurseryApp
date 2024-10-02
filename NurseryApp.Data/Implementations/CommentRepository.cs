using NurseryApp.Core.Entities;
using NurseryApp.Core.Repositories;
using NurseryApp.Data.Data;

namespace NurseryApp.Data.Implementations
{
    public class CommentRepository : Repository<Comment>, ICommentRepository
    {
        public CommentRepository(NurseryAppContext context) : base(context)
        {
        }
    }
}
