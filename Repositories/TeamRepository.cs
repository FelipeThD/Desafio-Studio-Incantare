using BackendTraining.Models;
using BackendTraining.Repositories.Interfaces;
using Dapper;
using System.Data;

namespace BackendTraining.Repositories
{
    public class TeamRepository : ITeamRepository
    {
        private readonly IDbConnection _connection;

        public TeamRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<Guid> AddTeamAsync(TeamModel team)
        {
            const string sql = @"
            INSERT INTO team_members (id, name, role, bio, image_url, created_at)
            VALUES (@Id, @Name, @Role, @Bio, @ImageUrl, @CreatedAt)
            RETURNING 
                id,
                name,
                role,
                bio,
                image_url AS ""ImageUrl"",
                created_at AS ""CreatedAt"";
             ";

            if (team.Id == Guid.Empty) team.Id = Guid.NewGuid();
            if (team.CreatedAt == default) team.CreatedAt = DateTime.UtcNow;

            return await _connection.ExecuteScalarAsync<Guid>(sql, team);
        }

        public async Task<IEnumerable<TeamModel>> GetAllTeamAsync()
        {
            const string sql = @"
             SELECT 
                id,
                name,
                role,
                bio,
                image_url AS ""ImageUrl"",
                created_at AS ""CreatedAt""
             FROM team_members
             ORDER BY created_at DESC;
             ";

            return await _connection.QueryAsync<TeamModel>(sql);
        }

        public async Task<TeamModel?> GetTeamByIdAsync(Guid id)
        {
            const string sql = @"
            SELECT 
                id,
                name,
                role,
                bio,
                image_url AS ""ImageUrl"",
                created_at AS ""CreatedAt""
            FROM team_members
            WHERE id = @Id;
        ";

            return await _connection.QueryFirstOrDefaultAsync<TeamModel>(sql, new { Id = id });
        }

        public async Task<TeamModel?> UpdateTeamAsync(TeamModel team)
        {
            const string sql = @"
            UPDATE team_members
            SET 
                name = @Name,
                role = @Role,
                bio = @Bio,
                image_url = @ImageUrl
            WHERE id = @Id
            RETURNING 
                id,
                name,
                role,
                bio,
                image_url AS ""ImageUrl"",
                created_at AS ""CreatedAt"";
            ";

            return await _connection.QueryFirstOrDefaultAsync<TeamModel>(sql, team);
        }

        public async Task<Guid> DeleteTeamByIdAsync(Guid id)
        {
            const string sql = @"
            DELETE FROM team_members
            WHERE id = @Id
            RETURNING id";

            return await _connection.ExecuteScalarAsync<Guid>(sql, new { Id = id });
        }
    }
}
