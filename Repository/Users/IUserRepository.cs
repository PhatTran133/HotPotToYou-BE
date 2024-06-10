using Repository.Models.RequestModels;
using Repository.Models.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Users
{
    public interface IUserRepository
    {
        Task<LoginResponseModel> Login(LoginRequestModel loginRequest);
        Task<string> CreateUser(UserRequestModel user);
    }
}
