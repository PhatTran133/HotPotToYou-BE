using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Repository.DbContexts;
using Repository.Entity.ConfigTable;
using Repository.Models.RequestModels.ActivityType;
using Service.CurrentUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.ActivityTypeRepository
{
    public class ActivityTypeRepository : IActivityTypeRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMapper _mapper;

        public ActivityTypeRepository(ApplicationDbContext context, IMapper mapper, ICurrentUserService currentUserService)
        {
            _context = context;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }

        public async Task<string> AddAsync(ActivityTypeModel model)
        {
            var checkActivityType = await _context.ActivityType.AnyAsync(x => x.Name == model.Name && x.DeleteDate == null);
            if (checkActivityType) 
                throw new Exception("ActivityType already existed");

            var newActivityType = new ActivityTypeEntity()
            {
                Name = model.Name,
                CreateByID = _currentUserService.UserId,
                CreateDate = DateTime.Now
            };
            _context.ActivityType.Add(newActivityType);
            if (await _context.SaveChangesAsync() > 0)
                return "Create Successfully";
            else
                return "Create Failed";
        }

        public async Task<string> DeleteAsync(int id)
        {
            var activityType = await _context.ActivityType.SingleOrDefaultAsync(x => x.ID == id);

            if (activityType == null) return "ActivityType not existed";
            else
            {
                activityType.DeleteByID = _currentUserService.UserId;
                activityType.DeleteDate = DateTime.Now;
                _context.ActivityType.Update(activityType);
                if (await _context.SaveChangesAsync() > 0)
                    return "Delete Successfully";
                else
                    return "Delete Failed";
            }
        }

        public async Task<string> UpdateAsync(ActivityTypeRequest entity)
        {
            var activityType = await _context.ActivityType.SingleOrDefaultAsync(x => x.ID == entity.ID && x.DeleteDate == null);

            if (activityType == null) return "ActivityType not existed";

            activityType.Name = entity.Name;
            activityType.UpdateByID = _currentUserService.UserId;
            activityType.UpdateDate = DateTime.Now;

            _context.ActivityType.Update(activityType);
            if (await _context.SaveChangesAsync() > 0)
                return "Update Successfully";
            else
                return "Update Failed";
        }
    }
}
