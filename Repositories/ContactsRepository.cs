using BackendTraining.Models;
using BackendTraining.Repositories.Interfaces;
using Dapper;
using System.Data;

namespace BackendTraining.Repositories
{
    public class ContactsRepository : IContactsRepository
    {
        private readonly IDbConnection _connection;

        public ContactsRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<Guid> AddAsync(ContactsModel contact)
        {
            const string sql = @"
                INSERT INTO contacts (id, name, email, message, created_at)
                VALUES (@Id, @Name, @Email, @Message, @CreatedAt)
                RETURNING id;
            ";

            if (contact.Id == Guid.Empty) contact.Id = Guid.NewGuid();
            if (contact.CreatedAt == default) contact.CreatedAt = DateTime.UtcNow;

            return await _connection.ExecuteScalarAsync<Guid>(sql, contact);
        }

        public async Task<ContactsModel?> GetByIdAsync(Guid id)
        {
            const string sql = "SELECT id, name, email, message, created_at FROM contacts WHERE id = @Id";

            return await _connection.QueryFirstOrDefaultAsync<ContactsModel>(sql, new { Id = id });
        }

        public async Task<IEnumerable<ContactsModel>> GetAllAsync()
        {
            var sql = @"SELECT * FROM contacts";

            return await _connection.QueryAsync<ContactsModel>(sql);
        }
    }
}
