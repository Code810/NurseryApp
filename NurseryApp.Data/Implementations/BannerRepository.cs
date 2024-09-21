using Microsoft.EntityFrameworkCore;
using NurseryApp.Core.Entities;
using NurseryApp.Core.Repositories;
using NurseryApp.Data.Data;

namespace NurseryApp.Data.Implementations
{
    public class BannerRepository : Repository<Banner>, IBannerRepository
    {
        public BannerRepository(NurseryAppContext context) : base(context)
        {
        }

        public async Task<Banner> GetOne() => await _context.Banners.FirstOrDefaultAsync();
    }
}
