using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IUserRepository
    {
        // get user by email 
        Task<User> GetUserByEmailAsync(string email);
        // add new user
        Task<User> AddUserAsync(User user);
    }
}
