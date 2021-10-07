using System.Collections.Generic;
using System.Threading.Tasks;
using SBTestTask.Common.Models;

namespace SBTestTask.Common.Infrastructure
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllAsync();
        Task AddAsync(User user);
    }
}