using NurseryApp.Core.Entities;
using NurseryApp.Core.Repositories;
using NurseryApp.Data.Data;

namespace NurseryApp.Data.Implementations
{
    public class HomeWorkSubmissionRepository : Repository<HomeWorkSubmission>, IHomeWorkSubmissionRepository
    {
        public HomeWorkSubmissionRepository(NurseryAppContext context) : base(context)
        {
        }
    }
}
