using Repository.Models.RequestModels;
using Repository.Models.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Users
{
    public interface IUserService
    {
        Task<LoginResponseModel> Login(LoginRequestModel loginRequest);
        Task<string> CreateUser(CreateUserRequestModel user);
        Task<List<UserResponseModel>> GetUsers(string? search, string? gender, string? sortBy, int pageIndex, int pageSize);
        Task<string> UpdateUser(UpdateUserRequestModel user);
        Task<string> DeleteUser(int id);
    }
}
