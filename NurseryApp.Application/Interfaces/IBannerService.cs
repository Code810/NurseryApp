using NurseryApp.Application.Dtos.Banner;

namespace NurseryApp.Application.Interfaces
{
    public interface IBannerService
    {
        Task<int> Create(BannerCreateDto bannerCreateDto);
        Task<int> Update(BannerUpdateDto bannerUpdateDto);
        Task<BannerReturnDto> Get();
    }
}
