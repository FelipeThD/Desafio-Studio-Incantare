using BackendTraining.Models;
using Dapper;
using System.Data;

namespace BackendTraining.Repositories
{
    public class ContactsRepository
    {
        private readonly IDbConnection _connection;

        public ContactsRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<int> AddAsync(ContactsModel contact)
        {
            var sql = @"INSERT INTO contacts (name, email, message) VALUES (@Name, @Email, @Message)";

            return await _connection.ExecuteAsync(sql, contact);
        }

        //public async Task<ContactModel?> GetbyId(int id)
        //{
        //    var sql = @"INSERT INTO contacts (name, email, message) VALUES (@Name, @Email, @Message)";

        //    return await _connection.QueryFirstOrDefaultAsync<ContactModel>(sql, id);
        //}

        public async Task<IEnumerable<ContactsModel>> GetAllAsync()
        {
            var sql = @"SELECT * FROM contacts";

            return await _connection.QueryAsync<ContactsModel>(sql);
        }
    }
}
