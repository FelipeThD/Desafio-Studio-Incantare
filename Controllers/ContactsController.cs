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

        [HttpPost]
        public async Task<IActionResult> Post(ContactsModel contact)
        {
            await _contactRepository.AddAsync(contact);
            return Ok("Contato Inserido com Sucesso!");
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var contacts = await _contactRepository.GetAllAsync();
            return Ok(contacts);
        }
    }
}
