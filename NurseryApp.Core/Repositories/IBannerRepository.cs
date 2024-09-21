using NurseryApp.Core.Entities;

namespace NurseryApp.Core.Repositories
{
    public interface IBannerRepository : IRepository<Banner>
    {
        Task<Banner> GetOne();

    }
}
