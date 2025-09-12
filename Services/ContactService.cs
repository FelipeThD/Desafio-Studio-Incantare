using BackendTraining.Dtos;
using BackendTraining.Models;
using BackendTraining.Repositories;

namespace BackendTraining.Services
{
    public class ContactService
    {
        private readonly IContactsRepository _repository;
        public ContactService(IContactsRepository repository)
        {
            _repository = repository;
        }

        public async Task<Guid> AddAsync(CreateContactDto dto)
        {
            var contact = new ContactsModel
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                Email = dto.Email,
                Message = dto.Message,
                CreatedAt = DateTime.UtcNow
            };

            return await _repository.AddAsync(contact);
        }

        public async Task<ResponseContactDto?> GetByIdAsync(Guid id)
        {
            var contact = await _repository.GetByIdAsync(id);
            if (contact == null) return null;

            return new ResponseContactDto
            {
                Id = contact.Id,
                Name = contact.Name,
                Email = contact.Email,
                Message = contact.Message,
                CreatedAt = contact.CreatedAt
            };
        }

        public async Task<IEnumerable<ResponseContactDto>> GetAllAsync()
        {
            var contacts = await _repository.GetAllAsync();

            return contacts.Select(c => new ResponseContactDto
            {
                Id = c.Id,
                Name = c.Name,
                Email = c.Email,
                Message = c.Message,
                CreatedAt = c.CreatedAt
            });
        }
    }
}
