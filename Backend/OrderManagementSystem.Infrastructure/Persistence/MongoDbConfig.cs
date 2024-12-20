using MongoDB.Bson;
using MongoDB.Driver;
using System;

namespace OrderManagementSystem.Infrastructure.Persistence
{
    public class MongoDbConfig
    {
        private readonly IMongoDatabase _database;

        public MongoDbConfig(string connectionString, string databaseName)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
                throw new ArgumentException("The connection string cannot be null or empty.", nameof(connectionString));

            if (string.IsNullOrWhiteSpace(databaseName))
                throw new ArgumentException("The database name cannot be null or empty.", nameof(databaseName));

            try
            {
                var client = new MongoClient(connectionString);
                _database = client.GetDatabase(databaseName);
            }
            catch (MongoConfigurationException ex)
            {
                throw new InvalidOperationException("Invalid MongoDB configuration.", ex);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Failed to connect to MongoDB.", ex);
            }
        }

        public IMongoCollection<T> GetCollection<T>(string collectionName)
        {
            if (string.IsNullOrWhiteSpace(collectionName))
                throw new ArgumentException("The collection name cannot be null or empty.", nameof(collectionName));

            return _database.GetCollection<T>(collectionName);
        }

        public bool TestConnection()
        {
            try
            {
                _database.RunCommandAsync((Command<BsonDocument>)"{ping:1}").Wait();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
