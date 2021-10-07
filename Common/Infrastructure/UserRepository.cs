using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using SBTestTask.Common.Infrastructure.Mongo;
using SBTestTask.Common.Models;

namespace SBTestTask.Common.Infrastructure
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoCollection<User> _collection;

        public UserRepository(IMongoDbContext mongoDbContext)
        {
            _collection = mongoDbContext.GetCollection<User>(Constants.MongoUserCollectionName) ??
                          throw new ArgumentNullException(nameof(_collection));
        }

        public async Task AddAsync(User user)
        {
            await _collection.InsertOneAsync(user);
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _collection.AsQueryable().ToListAsync();
        }
    }
}