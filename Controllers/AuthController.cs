using BackendTraining.Dtos;
using BackendTraining.Repositories.Interfaces;
using BackendTraining.Services;
using Microsoft.AspNetCore.Mvc;

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

        [HttpPost]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var token = await _service.Authentication(dto);

            if (token == null) return Unauthorized("Usuário ou senha inválidos");

            return Ok(new {token});
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(LoginDto dto)
        {
            var id = await _service.CreateUserAsync(dto);

            return CreatedAtAction(nameof(Register), new { id }, new { id });
        }
    }
}
