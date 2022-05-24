using ApiPostgresDapper.Domain.Entities;
using ApirPostgresDapper.Infraestructure.Interfaces;
using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApirPostgresDapper.Infraestructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly string _connection;
        protected NpgsqlConnection DbConnection() => new(_connection);
        public UserRepository(IConfiguration configuration) => _connection = configuration.GetConnectionString("postgres");
        public async Task<IEnumerable<User>> GetAll()
        {
            var db = DbConnection();
            var sql = @"SELECT id, username, password, register_date 
                        FROM online_store2.user";
            var result = await db.QueryAsync<User>(sql);
            return result;
        }

        public async Task<User> GetById(int id)
        {
            var db = DbConnection();
            var sql = @"SELECT id, username, password, register_date 
                        FROM online_store2.user
                        WHERE id=@id";
            DynamicParameters parameters = new();
            parameters.Add("id", id);
            var result = await db.QueryFirstOrDefaultAsync<User>(sql, parameters);
            return result;
        }
        public async Task<bool> Add(User entity)
        {
            var db = DbConnection();
            var sql = @"INSERT INTO online_store2.user (username, password, register_date)
                        VALUES (@username, @password, @register_date)";
            var result = await db.ExecuteAsync(sql, entity);
            return result > 0;
        }
        public async Task<bool> Update(User entity)
        {
            var db = DbConnection();
            var sql = @"UPDATE online_store2.user 
                        SET username=@username, password=@password
                        WHERE id=@id";
            var result = await db.ExecuteAsync(sql, entity);
            return result > 0;
        }
        public async Task<bool> Delete(int id)
        {
            var db = DbConnection();
            var sql = @"DELETE FROM public.user 
                        WHERE id=@id";
            DynamicParameters parameters = new();
            parameters.Add("id", id);
            var result = await db.ExecuteAsync(sql, parameters);
            return result > 0;
        }
    }
}
