using Microsoft.EntityFrameworkCore;
using NurseryApp.Core.Entities;
using NurseryApp.Core.Repositories;
using NurseryApp.Data.Data;

namespace NurseryApp.Data.Implementations
{
    public class ContactRepository : Repository<Contact>, IContatctRepository
    {
        public ContactRepository(NurseryAppContext context) : base(context)
        {

        }
        public async Task<IEnumerable<Contact>> GetAllWithSearch(string? text, int page = 1)
        {
            IQueryable<Contact> query = _context.contacts.Where(b => !b.IsDeleted).OrderByDescending(b => b.CreatedDate);
            if (!string.IsNullOrEmpty(text)) query = query.Where(b => b.FullName.ToLower().Contains(text.ToLower()) || b.Email.ToLower().Contains(text.ToLower()) || b.Message.ToLower().Contains(text.ToLower()));
            IEnumerable<Contact> contacts = await query.Skip((page - 1) * 9).Take(9).ToListAsync();
            return contacts;
        }

        public async Task<int> GetAllCount(string? text)
        {
            IQueryable<Contact> query = _context.contacts.Where(b => !b.IsDeleted);
            if (!string.IsNullOrEmpty(text)) query = query.Where(b => b.FullName.ToLower().Contains(text.ToLower()) || b.Email.ToLower().Contains(text.ToLower()) || b.Message.ToLower().Contains(text.ToLower()));
            return query.Count();
        }
    }
}
