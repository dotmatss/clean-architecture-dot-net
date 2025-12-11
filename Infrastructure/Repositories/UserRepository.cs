using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Entities;
using Infrastructure.Persistence;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoCollection<UserDocument> _users;

        public UserRepository(MongoDbContext context)
        {
            _users = context.Users; // Mongo collection of UserDocument
        }

        // Add a new user
        public async Task<User> AddUserAsync(User user)
        {
            var userDoc = UserDocument.FromDomain(user);
            await _users.InsertOneAsync(userDoc);
            return userDoc.ToDomain(); // return Domain entity
        }


        // Get user by email
        public async Task<User> GetUserByEmailAsync(string email)
        {
            var userDoc = await _users
                .Find(u => u.Email == email)
                .FirstOrDefaultAsync();

            return userDoc?.ToDomain(); // returns null if not found
        }
    }
}
