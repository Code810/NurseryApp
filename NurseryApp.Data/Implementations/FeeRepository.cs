using NurseryApp.Core.Entities;
using NurseryApp.Core.Repositories;
using NurseryApp.Data.Data;

namespace NurseryApp.Data.Implementations
{
    public class FeeRepository : Repository<Fee>, IFeeRepository
    {
        public FeeRepository(NurseryAppContext context) : base(context)
        {
        }
    }
}
