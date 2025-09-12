using BackendTraining.Models;

namespace BackendTraining.Repositories
{
    public interface IContactsRepository
    {
        Task<Guid> AddAsync(ContactsModel contact);
        Task<ContactsModel?> GetByIdAsync(Guid id);
        Task<IEnumerable<ContactsModel>> GetAllAsync();
    }
}
                                                                                                                                                                                      