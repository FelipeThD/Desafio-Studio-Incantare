using BackendTraining.Dtos;
using BackendTraining.Models;
using BackendTraining.Repositories.Interfaces;

namespace BackendTraining.Services
{
    public class UsersService
    {
        private readonly IUserRepository _userRepository;
        private readonly TokenService _tokenService;

        public UsersService(IUserRepository repository, TokenService tokenService)
        {
            _userRepository = repository;
            _tokenService = tokenService;
        }

        public async Task<string?> Authentication(LoginDto loginDto)
        {
            var user = await _userRepository.GetByUsernameAsync(loginDto.Username);

            if (user == null) return null;

            if (!BCrypt.Net.BCrypt.Verify(loginDto.Password, user.Password_Hash)) return null;

            return _tokenService.GenerateToken(loginDto.Password);
        }

        public async Task<Guid> CreateUserAsync(LoginDto dto)
        {
            var hashPassword = BCrypt.Net.BCrypt.HashPassword(dto.Password);

            var user = new UsersModel
            {
                Username = dto.Username,
                Password_Hash = hashPassword,
            };

            return await _userRepository.CreateAsync(user);
        }
    }
}
