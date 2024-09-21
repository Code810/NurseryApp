using AutoMapper;
using NurseryApp.Application.Dtos.SettingDto;
using NurseryApp.Application.Exceptions;
using NurseryApp.Application.Helpers;
using NurseryApp.Application.Interfaces;
using NurseryApp.Core.Entities;
using NurseryApp.Data.Implementations;

namespace NurseryApp.Application.Implementations
{
    public class SettingService : ISettingService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public SettingService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<int> Create(SettingCreateDto settingCreateDto)
        {
            try
            {
                var existSetting = await _unitOfWork.settingRepository.IsExist(s => s.Key == settingCreateDto.Key);
                if (existSetting) throw new CustomException(400, "this key has already been used");

                if (settingCreateDto.File == null && settingCreateDto.Value == null)
                {
                    throw new CustomException(400, "value and image cannot be empty at the same time");
                }
                if (settingCreateDto.File != null && settingCreateDto.Value != null)
                {
                    throw new CustomException(400, "value and image cannot be full at the same time");
                }
                var setting = _mapper.Map<Settings>(settingCreateDto);

                await _unitOfWork.settingRepository.AddAsync(setting);
                await _unitOfWork.SaveChangesAsync();

                return setting.Id;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public async Task<SettingReturnDto> Get(int? id)
        {
            if (id == null) throw new CustomException(400, "Setting ID cannot be null");
            var setting = await _unitOfWork.settingRepository.GetAsync(s => s.Id == id);
            if (setting == null) throw new CustomException(404, "setting not found");
            var settingDto = _mapper.Map<SettingReturnDto>(setting);
            return settingDto;
        }

        public async Task<IEnumerable<SettingReturnDto>> GetAll()
        {
            var settings = await _unitOfWork.settingRepository.GetAllAsync();
            if (settings.Count() <= 0) throw new CustomException(404, "Empty setting List");
            var settingsDtos = _mapper.Map<IEnumerable<SettingReturnDto>>(settings);

            return settingsDtos;
        }

        public async Task<int> Update(int? id, SettingUpdateDto settingUpdateDto)
        {
            if (id == null) throw new CustomException(400, "Setting ID cannot be null");
            var setting = await _unitOfWork.settingRepository.GetAsync(s => s.Id == id);
            if (setting == null) throw new CustomException(404, "Setting not found");
            var existSetting = await _unitOfWork.settingRepository.IsExist(s => s.Key == settingUpdateDto.Key && s.Id != id);
            if (existSetting) throw new CustomException(400, "this key has already been used");
            if (settingUpdateDto.File == null && settingUpdateDto.Value == null)
            {
                throw new CustomException(400, "value and image cannot be empty at the same time");
            }
            if (settingUpdateDto.File != null && settingUpdateDto.Value != null)
            {
                throw new CustomException(400, "value and image cannot be full at the same time");
            }
            if (settingUpdateDto.File != null && settingUpdateDto.Value == null)
            {
                Helper.DeleteImage("settings", setting.Value);
            }
            if (setting.Value.EndsWith(".jpg") || setting.Value.EndsWith(".png") || setting.Value.EndsWith(".jpeg") || setting.Value.EndsWith(".swg") && settingUpdateDto.Value != null)
            {
                Helper.DeleteImage("settings", setting.Value);
            }
            _mapper.Map(settingUpdateDto, setting);
            _unitOfWork.settingRepository.Update(setting);
            await _unitOfWork.SaveChangesAsync();
            return setting.Id;
        }
    }
}
