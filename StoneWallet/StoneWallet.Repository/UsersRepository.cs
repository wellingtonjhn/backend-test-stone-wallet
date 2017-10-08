using Dapper;
using Microsoft.Extensions.Configuration;
using StoneWallet.Domain.Contracts;
using StoneWallet.Domain.Models.Entities;
using System;
using System.Threading.Tasks;

namespace StoneWallet.Repository
{
    public class UsersesRepository : Repository, IUsersRepository
    {
        public UsersesRepository(IConfiguration configuration)
            : base(configuration)
        {
        }

        public async Task<User> Get(Guid id)
        {
            User user;

            using (var connection = GetConnection())
            {
                const string sql = @"SELECT ID, NAME, EMAIL, CREATIONDATE FROM USERS WHERE ID = @ID";

                user = await connection.QueryFirstOrDefaultAsync<User>(sql, new
                {
                    id
                });
            }
            return user;
        }

        public async Task CreateUser(User user)
        {
            using (var connection = GetConnection())
            {
                const string sql = @"INSERT INTO USERS (ID, NAME, EMAIL, PASSWORD, CREATIONDATE) 
                                     VALUES (@ID, @NAME, @EMAIL, @PASSWORD, @CREATIONDATE)";

                await connection.ExecuteAsync(sql, new
                {
                    user.Id,
                    user.Name,
                    user.Email,
                    password = user.Password.Encoded,
                    user.CreationDate
                });
            }
        }

        public async Task<bool> ExistsUser(string email)
        {
            bool exists;

            using (var connection = GetConnection())
            {
                const string sql = "SELECT TRUE FROM USERS WHERE EMAIL = @Email";

                exists = await connection.ExecuteScalarAsync<bool>(sql, new { email });
            }
            return exists;
        }

        public async Task<User> Authenticate(string email, string password)
        {
            User user;

            using (var connection = GetConnection())
            {
                const string sql = @"SELECT ID, NAME, EMAIL, CREATIONDATE FROM USERS
                                     WHERE EMAIL = @Email AND PASSWORD = @Password";

                user = await connection.QueryFirstOrDefaultAsync<User>(sql, new
                {
                    email,
                    password
                });
            }
            return user;
        }
    }
}