using BackendTraining.Models;

namespace BackendTraining.Repositories.Interfaces
{
    public interface ITeamRepository
    {
        Task<Guid> AddTeamAsync(TeamModel team);
        Task<IEnumerable<TeamModel>> GetAllTeamAsync();
        Task<TeamModel?> GetTeamByIdAsync(Guid id);
        Task<TeamModel?> UpdateTeamAsync(TeamModel team);
        Task<Guid> DeleteTeamByIdAsync(Guid id);
    }
}
