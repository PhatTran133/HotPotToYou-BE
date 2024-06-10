using Repository.Models.RequestModels;
using Repository.Models.ResponseModels;
using Repository.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Users
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<LoginResponseModel> Login(LoginRequestModel loginRequest)
        {
            return await _userRepository.Login(loginRequest);
        }

        public async Task<string> CreateUser(UserRequestModel user)
        {
            return await _userRepository.CreateUser(user);
        }

        
    }
}
