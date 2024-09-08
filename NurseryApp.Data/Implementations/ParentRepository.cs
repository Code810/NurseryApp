using NurseryApp.Core.Entities;
using NurseryApp.Core.Repositories;
using NurseryApp.Data.Data;

namespace NurseryApp.Data.Implementations
{
    public class ParentRepository : Repository<Parent>, IParentRepository
    {
        public ParentRepository(NurseryAppContext context) : base(context)
        {
        }
    }
}
