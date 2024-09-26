using NurseryApp.Application.Dtos.Contact;

namespace NurseryApp.Application.Interfaces
{
    public interface IContactService
    {
        Task<int> Create(ContactCreateDto contactCreateDto);
        Task<ContactReturnDto> Get(int? id);
        Task<int> Delete(int? id);
        Task<ContactListDto> GetAllWithSearch(string? text, int page);
    }
}
