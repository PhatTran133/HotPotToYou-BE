﻿using AutoMapper;
using Azure.Core;
using Microsoft.EntityFrameworkCore;
using Repository.DbContexts;
using Repository.Entity.ConfigTable;
using Repository.Models.RequestModels;
using Repository.Models.ResponseModels;
using Service.CurrentUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Repository.Roles
{
    public class RoleRepository : IRoleRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;

        public RoleRepository(ApplicationDbContext context, IMapper mapper, ICurrentUserService currentUserService)
        {
            _context = context;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }

        public async Task<string> CreateRole(RoleRequestModel role)
        {
            var checkDuplication = await _context.Role.AnyAsync(x => x.Name == role.Name && x.DeleteDate == null);
            if (checkDuplication)
                throw new InvalidOperationException("Role is existing");

            var newRole = new RoleEntity()
            {
                Name = role.Name,
                CreateByID = _currentUserService.UserId,
                CreateDate = DateTime.Now
            };
            _context.Role.Add(newRole);

            if (await _context.SaveChangesAsync() > 0)
                return "Create Successfully";
            else
                return "Create Failed";
        }

        public async Task<List<RoleResponseModel>> GetRoles()
        {
            var role = await _context.Role.ToListAsync();
            if (role == null)
                throw new Exception("Role list is empty");

            return _mapper.Map<List<RoleResponseModel>>(role);
        }
    }
}
