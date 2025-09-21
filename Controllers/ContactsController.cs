using BackendTraining.Dtos;
using BackendTraining.Models;
using BackendTraining.Repositories;
using BackendTraining.Services;
using Microsoft.AspNetCore.Mvc;

namespace BackendTraining.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactsController : ControllerBase
    {
        private readonly ContactsService _service;
        public ContactsController(ContactsService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateContactDto contactDto)
        {

            var id = await _service.AddAsync(contactDto);

            return CreatedAtRoute("GetContactById", new { id }, new { id });
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var contacts = await _service.GetAllAsync();

            return Ok(contacts);
        }

        [HttpGet("{id}", Name = "GetContactById")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var contact = await _service.GetByIdAsync(id);

            if (contact == null) return NotFound();

            return Ok(contact);
        }
    }
}
