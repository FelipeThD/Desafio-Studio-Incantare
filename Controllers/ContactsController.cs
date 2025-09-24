using BackendTraining.Dtos;
using BackendTraining.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

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
        [AllowAnonymous]
        [SwaggerOperation(Tags = new[] { "Público - Contatos" }, Summary = "Cria um novo contato.")]
        public async Task<IActionResult> Post(CreateContactDto contactDto)
        {

            var id = await _service.AddAsync(contactDto);

            return CreatedAtRoute("GetContactById", new { id }, new { id });
        }

        [HttpGet]
        [Authorize]
        [SwaggerOperation(Tags = new[] { "Protegido - Contatos" }, Summary = "Lista todos os contatos por ordem de criação.")]
        public async Task<IActionResult> GetAll()
        {
            var contacts = await _service.GetAllAsync();

            var result = contacts
                .OrderByDescending(x => x.CreatedAt)
                .Select(x => new ResponseContactDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Email = x.Email,
                    Message = x.Message,
                    CreatedAt = x.CreatedAt,
                });

            return Ok(result);
        }

        [HttpGet("{id}", Name = "GetContactById")]
        [AllowAnonymous]
        [SwaggerOperation(Tags = new[] { "Público - Contatos" }, Summary = "Busca contato por id.")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var contact = await _service.GetByIdAsync(id);

            if (contact == null) return NotFound();

            return Ok(contact);
        }
    }
}
