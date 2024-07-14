using AutoMapper;
using Azure.Core;
using Microsoft.EntityFrameworkCore;
using Repository.DbContexts;
using Repository.Entity;
using Repository.Models.RequestModels;
using Repository.Models.ResponseModels;
using Repository.Service.Paging;
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
        public async Task<string> CreateUser(CreateUserRequestModel user)
        {
            var checkEmail = await _context.User.AnyAsync(x => x.Email == user.Email && x.DeleteDate == null);
            if (checkEmail)
                throw new InvalidDataException("Email is existing");
            var checkPhone = await _context.User.AnyAsync(x => x.Phone == user.Phone && x.DeleteDate == null);
            if (checkPhone)
                throw new InvalidDataException("Phone is existing");

            var role = await _context.Role.SingleOrDefaultAsync(x => x.Name.Equals("staff") && x.DeleteDate == null);
            if (role == null)
                throw new InvalidDataException("Role staff is not found");

            var newUser = new UserEntity()
            {
                Name = user.Name,
                Email = user.Email,
                Password = _passwordService.HashPassword(user.Password),
                Gender = user.Gender,
                Phone = user.Phone,
                Status = "Active",
                RoleID = role.ID,
                CreateByID = _currentUserService.UserId,
                CreateDate = DateTime.Now
            };
            _context.User.Add(newUser);
            if (await _context.SaveChangesAsync() > 0)
                return "Create Successfully";
            else
                return "Create Failed";
        }
        public async Task<List<UserResponseModel>> GetUsers(string? search,string? gender, string? sortBy, int pageIndex, int pageSize)
        {
            //NHỚ CHECK DELETEDATE
            IQueryable<UserEntity> users = _context.User.Include(x => x.Role).Where(x => x.DeleteDate == null);


            //SEARCH THEO NAME
            if (!string.IsNullOrEmpty(search))
            {
                users = users.Where(x => x.Name.Contains(search));
            }

            //FILTER THEO GENDER
            if (!string.IsNullOrEmpty(gender))
            {
                users = users.Where(x => x.Gender.Equals(gender));
            }

            //SORT THEO NAME
            if (!string.IsNullOrEmpty(sortBy))
            {
                if (sortBy.Equals("asc"))
                {
                    users = users.OrderBy(x => x.Name);
                }
                else if (sortBy.Equals("desc"))
                {
                    users = users.OrderByDescending(x => x.Name);
                }
            }

            var paginatedUsers = PaginatedList<UserEntity>.Create(users, pageIndex, pageSize);

            return _mapper.Map<List<UserResponseModel>>(paginatedUsers);
        }
        public async Task<UserResponseModel> GetUserByEmail(string email)
        {
            var user = await _context.User.Include(x => x.Role).SingleOrDefaultAsync(x => x.Email == email && x.DeleteDate == null);
            if (user == null)
                throw new Exception("User is not found");

            return _mapper.Map<UserResponseModel>(user);
        }
        public async Task<string> UpdateUser(UpdateUserRequestModel user)
        {
            var checkUser = await _context.User.SingleOrDefaultAsync(x => x.ID == user.ID && x.DeleteDate == null);
            if (checkUser == null)
                throw new InvalidDataException("User is not found");

            var checkEmail = await _context.User.AnyAsync(x => x.Email == user.Email && x.ID != user.ID && x.DeleteDate == null);
            if (checkEmail)
                throw new InvalidDataException("Email is existing");

            var checkPhone = await _context.User.AnyAsync(x => x.Phone == user.Phone && x.ID != user.ID && x.DeleteDate == null);
            if (checkPhone)
                throw new InvalidDataException("Phone is existing");

            checkUser.Name = user.Name;
            checkUser.Email = user.Email;
            checkUser.Gender = user.Gender;
            checkUser.Phone = user.Phone;
            checkUser.Status = user.Status;
            checkUser.RoleID = user.RoleID;
            checkUser.UpdateByID = _currentUserService.UserId;
            checkUser.UpdateDate = DateTime.Now;
            

            _context.User.Update(checkUser);
            if (await _context.SaveChangesAsync() > 0)
                return "Update Successfully";
            else
                return "Update Failed";

        }
        public async Task<string> DeleteUser(int id)
        {
            var user = await _context.User.SingleOrDefaultAsync(x => x.ID == id && x.DeleteDate == null);
            if (user == null)
                throw new Exception("User is not found");

            user.DeleteByID = _currentUserService.UserId;
            user.DeleteDate = DateTime.Now;

            _context.Update(user);
            if (await _context.SaveChangesAsync() > 0)
                return "Delete Successfully";
            else
                return "Delete Failed";
        }
    }
}
