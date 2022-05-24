using ApiPostgresDapper.Domain.Entities;
using ApirPostgresDapper.Infraestructure.Interfaces;
using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApirPostgresDapper.Infraestructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly string _connection;
        protected NpgsqlConnection DbConnection() => new(_connection);
        public ProductRepository(IConfiguration configuration) => _connection = configuration.GetConnectionString("postgres");
        public async Task<IEnumerable<Product>> GetAll()
        {
            var db = DbConnection();
            var sql = @"SELECT id, code, name, price, create_date 
                        FROM online_store2.product";
            var result = await db.QueryAsync<Product>(sql);
            return result;
        }
        public async Task<Product> GetById(int id)
        {
            var db = DbConnection();
            var sql = @"SELECT id, code, name, price, create_date 
                        FROM online_store2.product
                        WHERE id=@id";
            DynamicParameters parameters = new();
            parameters.Add("id", id);
            var result = await db.QueryFirstOrDefaultAsync<Product>(sql, parameters);
            return result;
        }
        public async Task<bool> Add(Product entity)
        {
            var db = DbConnection();
            var sql = @"INSERT INTO online_store2.product (code, name, price, create_date)
                        VALUES (@code, @name, @price, @create_date)";
            var result = await db.ExecuteAsync(sql, entity);
            return result > 0;
        }
        public async Task<bool> Update(Product entity)
        {
            var db = DbConnection();
            var sql = @"UPDATE online_store2.product 
                        SET code=@code, name=@name, price=@price
                        WHERE id=@id";
            var result = await db.ExecuteAsync(sql, entity);
            return result > 0;
        }
        public async Task<bool> Delete(int id)
        {
            var db = DbConnection();
            var sql = @"DELETE FROM public.product 
                        WHERE id=@id";
            DynamicParameters parameters = new();
            parameters.Add("id", id);
            var result = await db.ExecuteAsync(sql, parameters);
            return result > 0;
        }
    }
}
