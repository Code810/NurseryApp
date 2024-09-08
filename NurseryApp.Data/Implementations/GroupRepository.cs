using NurseryApp.Core.Entities;
using NurseryApp.Core.Repositories;
using NurseryApp.Data.Data;

namespace NurseryApp.Data.Implementations
{
    public class GroupRepository : Repository<Group>, IGroupRepository
    {
        public GroupRepository(NurseryAppContext context) : base(context)
        {
        }
    }
}
