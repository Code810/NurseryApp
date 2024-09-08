using NurseryApp.Core.Entities;
using NurseryApp.Core.Repositories;
using NurseryApp.Data.Data;

namespace NurseryApp.Data.Implementations
{
    public class AttenDanceRepository : Repository<AttenDance>, IAttenDanceRepository
    {
        public AttenDanceRepository(NurseryAppContext context) : base(context)
        {
        }
    }
}
