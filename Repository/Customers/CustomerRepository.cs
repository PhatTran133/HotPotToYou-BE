using AutoMapper;
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
using System.Threading.Tasks;

namespace Repository.Customers
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;
        private readonly IPasswordService _passwordService;

        public CustomerRepository(ApplicationDbContext context, IMapper mapper, ICurrentUserService currentUserService, IPasswordService passwordService)
        {
            _context = context;
            _mapper = mapper;
            _currentUserService = currentUserService;
            _passwordService = passwordService;
        }

        public async Task<string> CreateCustomer(CreateCustomerRequestModel customer)
        {
            var checkEmail = await _context.Customer.SingleOrDefaultAsync(x => x.Email == customer.Email && x.DeleteDate == null);
            if (checkEmail != null)
                throw new InvalidDataException("Email is existed");

            var checkPhone = await _context.Customer.SingleOrDefaultAsync(x => x.Phone == customer.Phone && x.DeleteDate == null);
            if (checkPhone != null)
                throw new InvalidDataException("Phone is existed");

            var role = await _context.Role.SingleOrDefaultAsync(x => x.Name.Equals("customer"));
            if (role == null)
                throw new InvalidDataException("Role customer is not found");

            var newUser = new UserEntity()
            {
                Name = customer.Name,
                Email = customer.Email,
                Password = _passwordService.HashPassword(customer.Password),
                Gender = customer.Gender,
                Phone = customer.Phone,
                Status = "Active",
                RoleID = role.ID,
                CreateDate = DateTime.Now
            };

            _context.User.Add(newUser);

            if (await _context.SaveChangesAsync() <= 0)
                return "Create User Failed";

            var newCustomer = new CustomerEntity()
            {
                Name = customer.Name,
                Address = customer.Address,
                Email = customer.Email,
                Phone = customer.Phone,
                AvatarUrl = customer.AvatarUrl,
                YearOfBirth = customer.YearOfBirth,
                Gender = customer.Gender,
                Status = true,
                UserID = newUser.ID,
            };

            _context.Customer.Add(newCustomer);
            if (await _context.SaveChangesAsync() > 0)
                return "Create Customer Successfully";
            else
                return "Create Customer Failed";
        }
        public async Task<CustomerResponseModel> GetCustomerByID(int id)
        {
            var customer = await _context.Customer.SingleOrDefaultAsync(x => x.ID == id && x.DeleteDate == null);
            if (customer == null)
                throw new Exception("User is not found");

            return _mapper.Map<CustomerResponseModel>(customer);
        }
        public async Task<string> UpdateCustomer(UpdateCustomerRequestModel customer)
        {
            var customerEntity = await _context.Customer.SingleOrDefaultAsync(x => x.ID == customer.ID && x.DeleteDate == null);
            if (customerEntity == null)
                throw new Exception("Customer is not found");

            var checkEmail = await _context.Customer.SingleOrDefaultAsync(x => x.Email == customer.Email && x.ID != customer.ID && x.DeleteDate == null);
            if (checkEmail != null)
                throw new InvalidDataException("Email is existed");

            var checkPhone = await _context.Customer.SingleOrDefaultAsync(x => x.Phone == customer.Phone && x.ID != customer.ID && x.DeleteDate == null);
            if (checkPhone != null)
                throw new InvalidDataException("Phone is existed");

            customer.Name = customerEntity.Name;
            customer.Address = customerEntity.Address;
            customer.Email = customerEntity.Email;
            customer.Phone = customerEntity.Phone;
            customer.YearOfBirth = customerEntity.YearOfBirth;
            customer.Gender = customerEntity.Gender;
            customer.AvatarUrl = customerEntity.AvatarUrl;

            _context.Customer.Update(customerEntity);
            if (await _context.SaveChangesAsync() > 0)
                return "Update Customer Successfully";
            else
                return "Update Customer Failed";
        }
    }
}
