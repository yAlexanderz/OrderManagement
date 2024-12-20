using Dapper;
using OrderManagementSystem.Application.Interfaces;
using OrderManagementSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace OrderManagementSystem.Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly IDbConnection _dbConnection;

        public OrderRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task AddAsync(Order order)
        {
            const string sql = "INSERT INTO Orders (Id, CustomerId, OrderDate, Status, TotalAmount) VALUES (@Id, @CustomerId, @OrderDate, @Status, @TotalAmount)";
            await _dbConnection.ExecuteAsync(sql, new
            {
                order.Id,
                order.CustomerId,
                order.OrderDate,
                order.Status,
                order.TotalAmount
            });
        }

        public async Task<IEnumerable<Order>> GetAllAsync()
        {
            const string sql = "SELECT * FROM Orders";
            return await _dbConnection.QueryAsync<Order>(sql);
        }

        public async Task<Order> GetByIdAsync(Guid id)
        {
            const string sql = "SELECT * FROM Orders WHERE Id = @Id";
            return await _dbConnection.QueryFirstOrDefaultAsync<Order>(sql, new { Id = id });
        }

        public async Task UpdateAsync(Order order)
        {
            const string sql = "UPDATE Orders SET CustomerId = @CustomerId, OrderDate = @OrderDate, Status = @Status, TotalAmount = @TotalAmount WHERE Id = @Id";
            await _dbConnection.ExecuteAsync(sql, new
            {
                order.Id,
                order.CustomerId,
                order.OrderDate,
                order.Status,
                order.TotalAmount
            });
        }

        public async Task DeleteAsync(Guid id)
        {
            const string sql = "DELETE FROM Orders WHERE Id = @Id";
            await _dbConnection.ExecuteAsync(sql, new { Id = id });
        }
    }
}
