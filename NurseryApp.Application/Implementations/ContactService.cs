using AutoMapper;
using NurseryApp.Application.Dtos.Contact;
using NurseryApp.Application.Exceptions;
using NurseryApp.Application.Interfaces;
using NurseryApp.Core.Entities;
using NurseryApp.Data.Implementations;

namespace NurseryApp.Application.Implementations
{
    public class ContactService : IContactService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ContactService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Create(ContactCreateDto contactCreateDto)
        {
            var contact = _mapper.Map<Contact>(contactCreateDto);
            contact.IsRead = false;
            await _unitOfWork.contatctRepository.AddAsync(contact);
            await _unitOfWork.SaveChangesAsync();
            return contact.Id;
        }

        public async Task<int> Delete(int? id)
        {
            if (id == null) throw new CustomException(400, "Contact ID cannot be null");
            var contact = await _unitOfWork.contatctRepository.GetAsync(c => c.Id == id && !c.IsDeleted);
            if (contact == null) throw new CustomException(404, "Contact not found");
            contact.IsDeleted = true;
            _unitOfWork.contatctRepository.Update(contact);
            await _unitOfWork.SaveChangesAsync();
            return contact.Id;
        }
        public async Task DeleteMultiple(List<int> ids)
        {
            if (ids == null || ids.Count == 0)
                throw new CustomException(400, "Contact ID list cannot be null or empty");

            var contacts = await _unitOfWork.contatctRepository.FindAllAsync(c => ids.Contains(c.Id) && !c.IsDeleted);

            if (contacts == null || !contacts.Any())
                throw new CustomException(404, "No contacts found for the given IDs");

            foreach (var contact in contacts)
            {
                contact.IsDeleted = true;
                _unitOfWork.contatctRepository.Update(contact);
            }

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<ContactReturnDto> Get(int? id)
        {
            if (id == null) throw new CustomException(400, "Contact ID cannot be null");
            var contact = await _unitOfWork.contatctRepository.GetAsync(c => c.Id == id && !c.IsDeleted);
            if (contact == null) throw new CustomException(404, "Contact not found");

            var contactDto = _mapper.Map<ContactReturnDto>(contact);
            return contactDto;
        }

        public async Task<ContactListDto> GetAllWithSearch(string? text, int page)
        {
            var contacts = await _unitOfWork.contatctRepository.GetAllWithSearch(text, page);

            if (contacts.Count() <= 0) throw new CustomException(404, "Empty contact List");
            var contactDtos = _mapper.Map<IEnumerable<ContactReturnDto>>(contacts);

            ContactListDto contactListDto = new();
            contactListDto.TotalCount = await _unitOfWork.contatctRepository.GetAllCount(text);
            contactListDto.IsReadCount = (await _unitOfWork.contatctRepository.FindAllAsync(c => !c.IsRead)).Count();
            contactListDto.Items = contactDtos;
            return contactListDto;
        }

        public async Task<int> Update(int? id)
        {
            if (id is null) throw new CustomException(400, "Not found");
            var contact = await _unitOfWork.contatctRepository.GetAsync(c => c.Id == id && !c.IsDeleted);
            if (contact == null) throw new CustomException(400, "Not found");
            contact.IsRead = true;

            _unitOfWork.contatctRepository.Update(contact);
            await _unitOfWork.SaveChangesAsync();
            return contact.Id;

        }
    }
}
