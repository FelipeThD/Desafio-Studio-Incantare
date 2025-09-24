using BackendTraining.Dtos;
using BackendTraining.Models;
using BackendTraining.Repositories.Interfaces;

namespace BackendTraining.Services
{
    public class TeamService
    {
        private readonly ITeamRepository _repository;

        public TeamService(ITeamRepository repository)
        {
            _repository = repository;
        }

        public async Task<Guid> AddTeamAsync(CreateTeamDto dto)
        {
            var teamMember = new TeamModel
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                Bio = dto.Bio,
                Role = dto.Role,
                ImageUrl = dto.ImageUrl,
                CreatedAt = DateTime.UtcNow
            };

            return await _repository.AddTeamAsync(teamMember);
        }

        public async Task<ResponseTeamDto?> GetTeamByIdAsync(Guid id)
        {
            var team = await _repository.GetTeamByIdAsync(id);
            if (team == null) return null;

            return new ResponseTeamDto
            {
                Id = team.Id,
                Name = team.Name,
                Bio = team.Bio,
                Role = team.Role,
                ImageUrl = team.ImageUrl,
                CreatedAt = team.CreatedAt
            };
        }

        public async Task<IEnumerable<ResponseTeamDto>> GetAllTeamAsync()
        {
            var contacts = await _repository.GetAllTeamAsync();

            return contacts.Select(c => new ResponseTeamDto
            {
                Id = c.Id,
                Name = c.Name,
                Bio = c.Bio,
                Role = c.Role,
                ImageUrl = c.ImageUrl,
                CreatedAt = c.CreatedAt
            });
        }

        public async Task<ResponseTeamDto?> UpdateTeamAsync(Guid id, UpdateTeamDto teamDto)
        {
            var team = new TeamModel
            {
                Id = id,
                Name = teamDto.Name,
                Bio = teamDto.Bio,
                Role = teamDto.Role,
                ImageUrl = teamDto.ImageUrl,
            };

            var updateTeam = await _repository.UpdateTeamAsync(team);

            if (updateTeam == null) return null;

            return new ResponseTeamDto
            {
                Id = updateTeam.Id,
                Name = updateTeam.Name,
                Bio = updateTeam.Bio,
                Role = updateTeam.Role,
                ImageUrl = updateTeam.ImageUrl,
                CreatedAt = updateTeam.CreatedAt
            };
        }

        public async Task<Guid?> DeleteTeamByIdAsync(Guid id)
        {
            var deletedId = await _repository.DeleteTeamByIdAsync(id);

            return deletedId;
        }
    }
}
