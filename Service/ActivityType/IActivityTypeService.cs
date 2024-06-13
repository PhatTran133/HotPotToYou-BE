using Repository.Models.RequestModels.ActivityType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ActivityType
{
    public interface IActivityTypeService
    {
        Task<string> AddActivityTypeAsync(ActivityTypeModel model);
        Task<string> UpdateActivityTypeAsync(ActivityTypeRequest model);
        Task<string> DeleteActivityTypeAsync(int id);
    }
}
