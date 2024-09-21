using AutoMapper;
using NurseryApp.Application.Dtos.Banner;
using NurseryApp.Application.Exceptions;
using NurseryApp.Application.Helpers;
using NurseryApp.Application.Interfaces;
using NurseryApp.Core.Entities;
using NurseryApp.Data.Implementations;

namespace NurseryApp.Application.Implementations
{
    public class BannerService : IBannerService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;


        public BannerService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<int> Create(BannerCreateDto bannerCreateDto)
        {
            var existBanner = await _unitOfWork.bannerRepository.GetOne();
            if (existBanner != null) throw new CustomException(400, "Banner is already created");
            var banner = _mapper.Map<Banner>(bannerCreateDto);
            await _unitOfWork.bannerRepository.AddAsync(banner);
            await _unitOfWork.SaveChangesAsync();

            return banner.Id;
        }


        public async Task<BannerReturnDto> Get()
        {

            var banner = await _unitOfWork.bannerRepository.GetOne();
            if (banner == null) throw new CustomException(404, "Banner not found");
            var bannerDto = _mapper.Map<BannerReturnDto>(banner);
            return bannerDto;
        }


        public async Task<int> Update(BannerUpdateDto bannerUpdateDto)
        {
            var banner = await _unitOfWork.bannerRepository.GetOne();
            if (banner == null) throw new CustomException(404, "Blog not found");
            if (bannerUpdateDto.LeftFile != null) Helper.DeleteImage("banner", banner.LeftFileName);
            if (bannerUpdateDto.RightFile != null) Helper.DeleteImage("banner", banner.RightFileName);
            if (bannerUpdateDto.BottomFile != null) Helper.DeleteImage("banner", banner.BottomFileName);
            _mapper.Map(bannerUpdateDto, banner);
            _unitOfWork.bannerRepository.Update(banner);
            await _unitOfWork.SaveChangesAsync();
            return banner.Id;
        }
    }
}
