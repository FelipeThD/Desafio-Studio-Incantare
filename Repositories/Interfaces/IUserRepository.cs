using BackendTraining.Models;

namespace BackendTraining.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<UsersModel?> GetByUsernameAsync(string username);
        Task<Guid> CreateAsync(UsersModel user);
    }
}
