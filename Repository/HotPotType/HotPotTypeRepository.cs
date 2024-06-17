using AutoMapper;
using IdentityModel;
using Microsoft.EntityFrameworkCore;
using Repository.DbContexts;
using Repository.Entity;
using Repository.Entity.ConfigTable;
using Repository.Models.RequestModels;
using Repository.Models.RequestModels.HotPotType;
using Repository.Models.ResponseModels;
using Repository.Service.Paging;
using Service.CurrentUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.HotPotType
{
    public class HotPotTypeRepository : IHotPotTypeRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMapper _mapper;
        public HotPotTypeRepository(ApplicationDbContext context, IMapper mapper, ICurrentUserService currentUserService)
        {
            _context = context;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }
        public async Task<string> AddAsync(HotPotTypeModel model)
        {
            var newHotPotType = new HotPotTypeEntity()
            {
                Name = model.Name,
                CreateByID = _currentUserService.UserId,
                CreateDate = DateTime.Now
            };
            _context.HotPotType.Add(newHotPotType);
            if (await _context.SaveChangesAsync() > 0)
                return "Create Successfully";
            else
                return "Create Failed";
        }

        public async Task<string> DeleteAsync(int id)
        {
            var hotpotType = await _context.HotPotType.SingleOrDefaultAsync(x => x.ID == id);

            if (hotpotType == null) return "HotPotType not existed";
            else
            {
                hotpotType.DeleteByID = _currentUserService.UserId;
                hotpotType.DeleteDate = DateTime.Now;
                _context.HotPotType.Update(hotpotType);
                if (await _context.SaveChangesAsync() > 0)
                    return "Update Successfully";
                else
                    return "Update Failed";
            } 
        }

        public async Task<string> UpdateAsync(HotPotTypeRequest entity)
        {
            var hotpotType = await _context.HotPotType.SingleOrDefaultAsync(x => x.ID == entity.ID && x.DeleteDate ==null);

            if (hotpotType == null) return "HotPot Type existed";



            hotpotType.Name = entity.Name;
            hotpotType.UpdateByID = _currentUserService.UserId;
            hotpotType.UpdateDate = DateTime.Now;

            _context.HotPotType.Update(hotpotType);
            if (await _context.SaveChangesAsync() > 0)
                return "Update Successfully";
            else
                return "Update Failed";
        }

        public async Task<List<HotPotTypeResponseModel>> GetHotPotTypes(string? search, string? sortBy,
            int pageIndex, int pageSize)
        {
            IQueryable<HotPotTypeEntity> hotPotTypes = _context.HotPotType.Where(x => x.DeleteDate == null);

            //TÌM THEO TÊN
            if (!string.IsNullOrEmpty(search))
            {
                hotPotTypes = hotPotTypes.Where(x => x.Name.Contains(search));
            }

           
            //SORT THEO TÊN
            if (!string.IsNullOrEmpty(sortBy))
            {
                if (sortBy.Equals("ascName"))
                {
                    hotPotTypes = hotPotTypes.OrderBy(x => x.Name);
                }
                else if (sortBy.Equals("descName"))
                {
                    hotPotTypes = hotPotTypes.OrderByDescending(x => x.Name);
                }
            }


            var paginatedHotPotTypes = PaginatedList<HotPotTypeEntity>.Create(hotPotTypes, pageIndex, pageSize);

            return _mapper.Map<List<HotPotTypeResponseModel>>(paginatedHotPotTypes);
        }

        public async Task<HotPotTypeResponseModel> GetHotPotTypeByID(int id)
        {
            var hotPotType = await _context.HotPotType.SingleOrDefaultAsync(x => x.ID == id && x.DeleteDate == null);
            if (hotPotType == null)
                throw new Exception("Hot Pot Type is not found");

            return _mapper.Map<HotPotTypeResponseModel>(hotPotType);
        }
    }
}
