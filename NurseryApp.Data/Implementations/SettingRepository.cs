using NurseryApp.Core.Entities;
using NurseryApp.Core.Repositories;
using NurseryApp.Data.Data;

namespace NurseryApp.Data.Implementations
{
    public class SettingRepository : Repository<Settings>, ISettingRepository
    {
        public SettingRepository(NurseryAppContext context) : base(context)
        {
        }
    }
}
