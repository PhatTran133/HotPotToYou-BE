using Repository.Models.RequestModels;
using Repository.Models.ResponseModels;
using Repository.Roles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Roles
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;

        public RoleService(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }
        public async Task<string> CreateRole(RoleRequestModel role)
        {
            return await _roleRepository.CreateRole(role);
        }

        public async Task<List<RoleResponseModel>> GetRoles()
        {
            return await _roleRepository.GetRoles();
        }
    }
}
