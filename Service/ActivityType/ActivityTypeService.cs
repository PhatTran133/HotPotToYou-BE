using Repository.ActivityTypeRepository;
using Repository.Models.RequestModels.ActivityType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ActivityType
{
    public class ActivityTypeService : IActivityTypeService
    {
        private readonly IActivityTypeRepository _activityTypeRepository;

        public ActivityTypeService(IActivityTypeRepository activityTypeRepository)
        {
            _activityTypeRepository = activityTypeRepository;
        }

        public async Task<string> AddActivityTypeAsync(ActivityTypeModel model)
        {
            return await _activityTypeRepository.AddAsync(model);
        }

        public async Task<string> UpdateActivityTypeAsync(ActivityTypeRequest model)
        {
            return await _activityTypeRepository.UpdateAsync(model);
        }

        public async Task<string> DeleteActivityTypeAsync(int id)
        {
            return await _activityTypeRepository.DeleteAsync(id);
        }
    }
}
