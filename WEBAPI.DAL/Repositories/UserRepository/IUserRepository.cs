using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WEBAPI.DAL.models;

namespace WEBAPI.DAL.Repositories.UserRepository
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllUsersAsync();
        Task<User> AddUserAsync(User user);
    }
}
