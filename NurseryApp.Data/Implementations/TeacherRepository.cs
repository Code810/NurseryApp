using NurseryApp.Core.Entities;
using NurseryApp.Core.Repositories;
using NurseryApp.Data.Data;

namespace NurseryApp.Data.Implementations
{
    public class TeacherRepository : Repository<Teacher>, ITeacherRepository
    {
        public TeacherRepository(NurseryAppContext context) : base(context)
        {
        }
    }
}
