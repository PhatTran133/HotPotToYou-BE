﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Repository.DbContexts;
using Repository.Entity;
using Repository.Models.RequestModels;
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


    }
}
