using BackendTraining.Dtos;
using BackendTraining.Models;
using BackendTraining.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BackendTraining.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TeamController : ControllerBase
    {
        private readonly TeamService _service;
        public TeamController(TeamService service)
        {
            _service = service;
        }

        [HttpGet]
        [AllowAnonymous]
        [SwaggerOperation(Tags = new[] { "Público - Team Members" }, Summary = "Lista todos os membros do time.")]
        public async Task<IActionResult> ReadAll()
        {
            var team = await _service.GetAllTeamAsync();

            var result = team
                .Select(x => new ResponseTeamDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Role = x.Role,
                    Bio = x.Bio,
                    ImageUrl = x.ImageUrl,
                    CreatedAt = x.CreatedAt
                });

            return Ok(result);
        }

        [HttpGet("{id}", Name = "GetTeamById")]
        [AllowAnonymous]
        [SwaggerOperation(Tags = new[] { "Público - Team Members" }, Summary = "Busca membros do time por id.")]
        public async Task<IActionResult> GetTeamById(Guid id)
        {
            var team = await _service.GetTeamByIdAsync(id);

            if (team == null) return NotFound();

            return Ok(team);
        }

        [HttpPost]
        [Authorize]
        [SwaggerOperation(Tags = new[] { "Protegido - Team Members" }, Summary = "Cria um novo membro do time")]
        public async Task<IActionResult> Create(CreateTeamDto teamDto)
        {
            var id = await _service.AddTeamAsync(teamDto);

            return CreatedAtRoute("GetTeamById", new { id }, new { id });
        }

        [HttpPut("{id}")]
        [Authorize]
        [SwaggerOperation(Tags = new[] { "Protegido - Team Members" }, Summary = "Altera dados do membro do time.")]
        public async Task<IActionResult> Update(Guid id, UpdateTeamDto teamDto)
        {
            var updated = await _service.UpdateTeamAsync(id, teamDto);

            if (updated == null)
                return NotFound("Membro não encontrado.");

            return Ok(updated);
        }

        [HttpDelete("{id}")]
        [Authorize]
        [SwaggerOperation(Tags = new[] { "Protegido - Team Members" }, Summary = "Deleta o membro do time.")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deletedId = await _service.DeleteTeamByIdAsync(id);

            if (deletedId == null) return NotFound("Membro não encontrado ou inexistente.");

            return Ok(new { id = deletedId });
        }
    }
}
