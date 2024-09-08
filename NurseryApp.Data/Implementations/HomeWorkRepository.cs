using NurseryApp.Core.Entities;
using NurseryApp.Core.Repositories;
using NurseryApp.Data.Data;

namespace NurseryApp.Data.Implementations
{
    public class HomeWorkRepository : Repository<HomeWork>, IHomeWorkRepository
    {
        public HomeWorkRepository(NurseryAppContext context) : base(context)
        {
        }
    }
}
