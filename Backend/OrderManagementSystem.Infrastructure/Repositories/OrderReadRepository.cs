using MongoDB.Driver;
using OrderManagementSystem.Application.Interfaces;
using OrderManagementSystem.Domain.Entities;
using OrderManagementSystem.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrderManagementSystem.Infrastructure.Repositories
{
    public class OrderReadRepository : IOrderReadRepository
    {
        private readonly IMongoCollection<Order> _orderCollection;

        public OrderReadRepository(MongoDbConfig mongoDbConfig)
        {
            _orderCollection = mongoDbConfig.GetCollection<Order>("Orders");
        }

        public async Task<IEnumerable<Order>> GetAllAsync()
        {
            return await _orderCollection.Find(_ => true).ToListAsync();
        }

        public async Task<IEnumerable<Order>> GetByStatusAsync(string status)
        {
            var filter = Builders<Order>.Filter.Eq(o => o.Status.ToString(), status);
            return await _orderCollection.Find(filter).ToListAsync();
        }

        public async Task<IEnumerable<Order>> GetByCustomerIdAsync(Guid customerId)
        {
            var filter = Builders<Order>.Filter.Eq(o => o.CustomerId, customerId);
            return await _orderCollection.Find(filter).ToListAsync();
        }

        public async Task<Order> GetByIdAsync(Guid id)
        {
            var filter = Builders<Order>.Filter.Eq(o => o.Id, id);
            return await _orderCollection.Find(filter).FirstOrDefaultAsync();
        }
    }
}
