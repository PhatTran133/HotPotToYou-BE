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
        Task<string> CreateUser(UserRequestModel user);
    }
}
