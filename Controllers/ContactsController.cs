using BackendTraining.Dtos;
using BackendTraining.Models;
using BackendTraining.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BackendTraining.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactsController : ControllerBase
    {
        private readonly ContactsRepository _contactRepository;
        public ContactsController(ContactsRepository repository)
        {
            _contactRepository = repository;
        }

        //[HttpPost]
        //public async Task<IActionResult> Post(ContactsModel contact)
        //{
        //    await _contactRepository.AddAsync(contact);

        //    return Ok("Contato Inserido com Sucesso!");
        //}

        [HttpPost]
        public async Task<IActionResult> PostDto(CreateContactDto contactdto)
        {
            var contact = new ContactsModel
            {
                Id = Guid.NewGuid(),
                Name = contactdto.Name,
                Email = contactdto.Email,
                Message = contactdto.Message,
                CreatedAt = DateTime.UtcNow
            };

            var id = await _contactRepository.AddAsync(contact);

            return CreatedAtRoute("GetContactById", new { id }, new { id });
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var contacts = await _contactRepository.GetAllAsync();

            return Ok(contacts);
        }

        [HttpGet("{id}", Name = "GetContactById")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var contact = await _contactRepository.GetByIdAsync(id);

            if (contact == null) return NotFound();

            return Ok(contact);
        }
    }
}
