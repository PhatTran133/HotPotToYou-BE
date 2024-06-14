using Repository.Models.RequestModels.ActivityType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.ActivityTypeRepository
{
    public interface IActivityTypeRepository
    {
        Task<string> AddAsync(ActivityTypeModel model);
        Task<string> UpdateAsync(ActivityTypeRequest entity);
        Task<string> DeleteAsync(int id);
    }
}
