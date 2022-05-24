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
    public class OrderRepository : IOrderRepository
    {
        private readonly string _connection;
        protected NpgsqlConnection DbConnection() => new(_connection);
        public OrderRepository(IConfiguration configuration) => _connection = configuration.GetConnectionString("postgres");
        public async Task<IEnumerable<Order>> GetAll()
        {
            var db = DbConnection();
            var sql = @"SELECT id, customer_id, order_date 
                        FROM online_store2.order";
            var result = await db.QueryAsync<Order>(sql);
            return result;
        }
        public async Task<Order> GetById(int id)
        {
            var db = DbConnection();
            var sql = @"SELECT id, customer_id, order_date 
                        FROM online_store2.order
                        WHERE id=@id";
            DynamicParameters parameters = new();
            parameters.Add("id", id);
            var result = await db.QueryFirstOrDefaultAsync<Order>(sql, parameters);
            return result;
        }
        public async Task<int> Add(Order entity)
        {
            var db = DbConnection();
            var sql = @"INSERT INTO online_store2.order (customer_id, order_date)
                        VALUES (@customer_id, @order_date)";
            var result = await db.ExecuteAsync(sql, entity);
            if (result > 0)
                result = await GetLastOrder();
            return result;
        }
        public async Task<bool> Update(Order entity)
        {
            var db = DbConnection();
            var sql = @"UPDATE online_store2.order 
                        SET customer_id=@customer_id
                        WHERE id=@id";
            var result = await db.ExecuteAsync(sql, entity);
            return result > 0;
        }
        public async Task<bool> Delete(int id)
        {
            var db = DbConnection();
            var sql = @"DELETE FROM online_store2.order 
                        WHERE id=@id";
            DynamicParameters parameters = new();
            parameters.Add("id", id);
            var result = await db.ExecuteAsync(sql, parameters);
            return result > 0;
        }
        private async Task<int> GetLastOrder()
        {
            var db = DbConnection();
            var sql = @"SELECT id
                        FROM online_store2.order 
                        ORDER BY order_date DESC
                        LIMIT 1";
            var result = await db.QueryFirstOrDefaultAsync<int>(sql);
            return result;
        }
    }
}
