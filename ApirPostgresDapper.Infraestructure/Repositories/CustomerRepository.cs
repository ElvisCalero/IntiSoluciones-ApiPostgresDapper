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
    public class CustomerRepository : ICustomerRepository
    {
        private readonly string _connection;
        protected NpgsqlConnection DbConnection() => new(_connection);
        public CustomerRepository(IConfiguration configuration) => _connection = configuration.GetConnectionString("postgres");
        public async Task<IEnumerable<Customer>> GetAll()
        {
            var db = DbConnection();
            var sql = @"SELECT id, identification, first_name, last_name, email, user_id, create_date 
                        FROM online_store2.customer";
            var result = await db.QueryAsync<Customer>(sql);
            return result;
        }

        public async Task<Customer> GetById(int id)
        {
            var db = DbConnection();
            var sql = @"SELECT id, identification, first_name, last_name, email, user_id, create_date 
                        FROM online_store2.customer
                        WHERE id=@id";
            DynamicParameters parameters = new();
            parameters.Add("id", id);
            var result = await db.QueryFirstOrDefaultAsync<Customer>(sql, parameters);
            return result;
        }
        public async Task<bool> Add(Customer entity)
        {
            var db = DbConnection();
            var sql = @"INSERT INTO online_store2.customer (identification, first_name, last_name, email, user_id, create_date)
                        VALUES (@identification, @first_name, @last_name, @email, @user_id, @create_date)";
            var result = await db.ExecuteAsync(sql, entity);
            return result > 0;
        }
        public async Task<bool> Update(Customer entity)
        {
            var db = DbConnection();
            var sql = @"UPDATE online_store2.customer 
                        SET identification=@identification, first_name=@first_name, last_name=@last_name, email=@email, user_id=@user_id
                        WHERE id=@id";
            var result = await db.ExecuteAsync(sql, entity);
            return result > 0;
        }
        public async Task<bool> Delete(int id)
        {
            var db = DbConnection();
            var sql = @"DELETE FROM public.customer 
                        WHERE id=@id";
            DynamicParameters parameters = new();
            parameters.Add("id", id);
            var result = await db.ExecuteAsync(sql, parameters);
            return result > 0;
        }
    }
}
