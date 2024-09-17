using NurseryApp.Core.Entities;
using NurseryApp.Core.Repositories;
using NurseryApp.Data.Data;

namespace NurseryApp.Data.Implementations
{
    public class BlogRepository : Repository<Blog>, IBlogRepository
    {
        public BlogRepository(NurseryAppContext context) : base(context)
        {
        }
    }
}
