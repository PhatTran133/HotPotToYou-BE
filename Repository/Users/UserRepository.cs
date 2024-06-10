using AutoMapper;
using Azure.Core;
using Microsoft.EntityFrameworkCore;
using Repository.DbContexts;
using Repository.Entity;
using Repository.Models.RequestModels;
using Repository.Models.ResponseModels;
using Service.CurrentUser;
using Service.Password;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Repository.Users
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;
        private readonly IPasswordService _passwordService;

        public UserRepository(ApplicationDbContext context, IMapper mapper, ICurrentUserService currentUserService, IPasswordService passwordService)
        {
            _context = context;
            _mapper = mapper;
            _currentUserService = currentUserService;
            _passwordService = passwordService;
        }

        public async Task<LoginResponseModel> Login(LoginRequestModel loginRequest)
        {
            var user = await _context.User.SingleOrDefaultAsync(x => x.Email == loginRequest.Email && x.DeleteDate == null);
            if (user == null)
            {
                throw new InvalidDataException($"Email is not found - {loginRequest.Email}");
            }

            if (user != null)
            {
                var chucvu = await _context.Role.SingleOrDefaultAsync(x => x.ID == user.RoleID && x.DeleteDate == null);
                if (chucvu == null)
                {
                    throw new Exception($"Role is not found - {loginRequest.Email}");
                }

                var samePassword = _passwordService.VerifyPassword(loginRequest.Password, user.Password);
                if (samePassword)
                {
                    return _mapper.Map<LoginResponseModel>(user);
                }
            }

            throw new Exception("Wrong Email Or Password");
        }
        public async Task<string> CreateUser(UserRequestModel user)
        {
            var checkEmail = await _context.User.SingleOrDefaultAsync(x => x.Email == user.Email && x.DeleteDate == null);
            if (checkEmail != null)
                throw new InvalidDataException("Email is existing");
            var checkPhone = await _context.User.SingleOrDefaultAsync(x => x.Phone == user.Phone && x.DeleteDate == null);
            if (checkPhone != null)
                throw new InvalidDataException("Phone is existing");

            var newUser = new UserEntity()
            {
                Name = user.Name,
                Email = user.Email,
                Password = _passwordService.HashPassword(user.Password),
                Gender = user.Gender,
                Phone = user.Phone,
                Status = "Active",
                RoleID = 2,
                CreateByID = _currentUserService.UserId,
                CreateDate = DateTime.Now
            };
            _context.User.Add(newUser);
            if (await _context.SaveChangesAsync() > 0)
                return "Create Successfully";
            else
                return "Create Failed";
        }
    }
}
