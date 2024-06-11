﻿using Repository.Models.RequestModels;
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

        public async Task<string> CreateUser(CreateUserRequestModel user)
        {
            return await _userRepository.CreateUser(user);
        }

        public async Task<List<UserResponseModel>> GetUsers(string? search, string? gender, string? sortBy, int pageIndex, int pageSize)
        {
            return await _userRepository.GetUsers(search, gender, sortBy, pageIndex, pageSize);
        }
        public async Task<string> UpdateUser(UpdateUserRequestModel user)
        {
            return await _userRepository.UpdateUser(user);
        }
        public async Task<string> DeleteUser(int id)
        {
            return await _userRepository.DeleteUser(id);
        }

        
    }
}
