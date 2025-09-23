using BackendTraining.Dtos;
using BackendTraining.Repositories.Interfaces;
using BackendTraining.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BackendTraining.Controllers
{
    [ApiController]
    [Route("Api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UsersService _service;

        public AuthController(UsersService service)
        {
            _service = service;
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        [SwaggerOperation(Tags = new[] { "Público - Login" }, Summary = "Autenticação do usuário", Description = "Envia usuário e senha. Retorna JWT válido por 1 hora.")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var token = await _service.Authentication(dto);

            if (token == null) return Unauthorized("Usuário ou senha inválidos.");

            return Ok(new {token});
        }

        [HttpPost("Register")]
        [AllowAnonymous]
        [SwaggerOperation(Tags = new[] { "Público - Login" }, Summary = "Registrar novo usuário.")]
        public async Task<IActionResult> Register(LoginDto dto)
        {
            var id = await _service.CreateUserAsync(dto);

            return CreatedAtAction(nameof(Register), new { id }, new { id });
        }
    }
}
