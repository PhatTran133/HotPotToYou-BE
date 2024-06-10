using Repository.Models.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Roles
{
    public interface IRoleRepository
    {
        Task<string> CreateRole(RoleRequestModel role);
    }
}
