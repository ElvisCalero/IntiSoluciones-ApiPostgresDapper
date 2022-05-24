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
    public class OrderDetailRepository : IOrderDetailRepository
    {
        private readonly string _connection;
        protected NpgsqlConnection DbConnection() => new(_connection);
        public OrderDetailRepository(IConfiguration configuration) => _connection = configuration.GetConnectionString("postgres");
        public async Task<IEnumerable<OrderDetail>> GetAll()
        {
            var db = DbConnection();
            var sql = @"SELECT id, order_id, product_id, unit_price, quantity, total_price 
                        FROM online_store2.order_detail";
            var result = await db.QueryAsync<OrderDetail>(sql);
            return result;
        }

        public async Task<IEnumerable<OrderDetail>> GetById(int id)
        {
            var db = DbConnection();
            var sql = @"SELECT id, order_id, product_id, unit_price, quantity, total_price 
                        FROM online_store2.order_detail
                        WHERE order_id=@id";
            DynamicParameters parameters = new();
            parameters.Add("id", id);
            var result = await db.QueryAsync<OrderDetail>(sql, parameters);
            return result;
        }

        public async Task<bool> Add(OrderDetail entity)
        {
            var db = DbConnection();
            var sql = @"INSERT INTO online_store2.order_detail (order_id, product_id, unit_price, quantity, total_price)
                        VALUES (@order_id, @product_id, @unit_price, @quantity, @total_price)";
            var result = await db.ExecuteAsync(sql, entity);
            return result > 0;
        }

        public async Task<bool> Update(OrderDetail entity)
        {
            var db = DbConnection();
            var sql = @"UPDATE online_store2.order_detail 
                        SET order_id=@order_id, product_id=@product_id, unit_price=@unit_price, quantity=@quantity, total_price=@total_price
                        WHERE id=@id";
            var result = await db.ExecuteAsync(sql, entity);
            return result > 0;
        }

        public async Task<bool> Delete(int id)
        {
            var db = DbConnection();
            var sql = @"DELETE FROM public.order_detail 
                        WHERE id=@id";
            DynamicParameters parameters = new();
            parameters.Add("id", id);
            var result = await db.ExecuteAsync(sql, parameters);
            return result > 0;
        }       

    }
}
