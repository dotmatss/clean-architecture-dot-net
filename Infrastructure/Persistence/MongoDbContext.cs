using Domain.Entities;
using Infrastructure.Entities;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _db;

        public MongoDbContext(IConfiguration config)
        {
            var client = new MongoClient(config["Mongo:Connection"]);
            _db = client.GetDatabase(config["Mongo:Database"]);
        }

        public IMongoCollection<Book> Books => _db.GetCollection<Book>("Books");
        public IMongoCollection<UserDocument> Users => _db.GetCollection<UserDocument>("Users");
        public IMongoCollection<Order> Orders => _db.GetCollection<Order>("Orders");

    }
}
