using AutoMapper;
using NurseryApp.Application.Dtos.HomeWork;
using NurseryApp.Application.Exceptions;
using NurseryApp.Application.Interfaces;
using NurseryApp.Core.Entities;
using NurseryApp.Data.Implementations;

namespace NurseryApp.Application.Implementations
{
    public class HomeWorkService : IHomeWorkService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public HomeWorkService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<int> Create(HomeWorkCreateDto homeWorkCreateDto)
        {
            var existHomeWork = await _unitOfWork.homeWorkRepository.IsExist(h => h.CreatedDate.Date == DateTime.Now.Date && h.GroupId == homeWorkCreateDto.GroupId);
            if (existHomeWork) throw new CustomException(400, "Today homework for this group is already created");
            var existGroup = await _unitOfWork.groupRepository.IsExist(g => g.Id == homeWorkCreateDto.GroupId);
            if (!existGroup) throw new CustomException(400, "Selected group not found");
            var homeWork = _mapper.Map<HomeWork>(homeWorkCreateDto);
            homeWork.CreatedDate = DateTime.Now;
            await _unitOfWork.homeWorkRepository.AddAsync(homeWork);
            await _unitOfWork.SaveChangesAsync();
            return homeWork.Id;
        }

        public async Task<HomeWorkReturnDto> Get(int? id)
        {
            if (id == null) throw new CustomException(400, "Homework not selected");
            var homeWork = await _unitOfWork.homeWorkRepository.GetByWithIncludesAsync(h => h.Id == id, h => h.Group);
            if (homeWork == null) throw new CustomException(400, "Homework not found");
            return _mapper.Map<HomeWorkReturnDto>(homeWork);
        }

        public async Task<IEnumerable<HomeWorkReturnDto>> GetAll()
        {
            var homeWorks = await _unitOfWork.homeWorkRepository.GetAllWithIncludesAsync(h => h.Group);
            if (homeWorks.Count() <= 0) throw new CustomException(400, "Empty list");
            return _mapper.Map<IEnumerable<HomeWorkReturnDto>>(homeWorks);
        }

        public async Task<IEnumerable<HomeWorkReturnDto>> GetAll(int? groupId)
        {
            if (groupId == null) throw new CustomException(400, "group not selected");
            var homeWorks = await _unitOfWork.homeWorkRepository.FindWithIncludesAsync(h => h.GroupId == groupId, h => h.Group);
            if (homeWorks.Count() <= 0) throw new CustomException(400, "Empty list");
            return _mapper.Map<IEnumerable<HomeWorkReturnDto>>(homeWorks);
        }

        public async Task<int> Update(int? id, HomeWorkUpdateDto homeWorkUpdateDto)
        {
            if (id == null) throw new CustomException(400, "Homework not selected");
            var existHomeWork = await _unitOfWork.homeWorkRepository.GetAsync(h => h.Id == id);
            if (existHomeWork == null) throw new CustomException(400, "Homework not found");
            var updatedHomeWork = _mapper.Map(homeWorkUpdateDto, existHomeWork);
            _unitOfWork.homeWorkRepository.Update(updatedHomeWork);
            _unitOfWork.SaveChangesAsync();
            return existHomeWork.Id;
        }
    }
}
