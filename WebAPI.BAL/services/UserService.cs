using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.BAL.interfaces;
using WEBAPI.DAL.models;
using WEBAPI.DAL.Repositories.UserRepository;

namespace WebAPI.BAL.services
{
    public  class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            return await _userRepository.GetAllUsersAsync();
        }

        public async Task<User> AddUserAsync(User newUser)
        {
            return await _userRepository.AddUserAsync(newUser) ;
        }
    }
}