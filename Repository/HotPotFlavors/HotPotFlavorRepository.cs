using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Repository.DbContexts;
using Repository.Entity.ConfigTable;
using Repository.Models.RequestModels;
using Repository.Models.ResponseModels;
using Repository.Service.Paging;
using Service.CurrentUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.HotPotFlavors
{
    public class HotPotFlavorRepository : IHotPotFlavorRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMapper _mapper;
        public HotPotFlavorRepository(ApplicationDbContext context, IMapper mapper, ICurrentUserService currentUserService)
        {
            _context = context;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }

        public async Task<string> CreateHotPotFlavor(CreateHotPotFlavorRequestModel hotPotFlavor)
        {
            var checkHotPotFlavor = await _context.HotPotFlavor.AnyAsync(x => x.Name == hotPotFlavor.Name && x.DeleteDate == null);
            if (checkHotPotFlavor) 
                throw new Exception("HotPotFlavor already exists");

            var newHotPotFlavor = new HotPotFlavorEntity()
            {
                Name = hotPotFlavor.Name,
                CreateByID = _currentUserService.UserId,
                CreateDate = DateTime.Now
            };
            _context.HotPotFlavor.Add(newHotPotFlavor);
            if (await _context.SaveChangesAsync() > 0)
                return "Create Successfully";
            else
                return "Create Failed";
        }

        public async Task<List<HotPotFlavorResponseModel>> GetHotPotFlavors(string? search, string? sortBy,
            int pageIndex, int pageSize)
        {
            IQueryable<HotPotFlavorEntity> hotPotFlavors = _context.HotPotFlavor.Where(x => x.DeleteDate == null);

            //TÌM THEO TÊN
            if (!string.IsNullOrEmpty(search))
            {
                hotPotFlavors = hotPotFlavors.Where(x => x.Name.Contains(search));
            }


            //SORT THEO TÊN
            if (!string.IsNullOrEmpty(sortBy))
            {
                if (sortBy.Equals("ascName"))
                {
                    hotPotFlavors = hotPotFlavors.OrderBy(x => x.Name);
                }
                else if (sortBy.Equals("descName"))
                {
                    hotPotFlavors = hotPotFlavors.OrderByDescending(x => x.Name);
                }
            }

            var paginatedHotPotFlavors = PaginatedList<HotPotFlavorEntity>.Create(hotPotFlavors, pageIndex, pageSize);

            return _mapper.Map<List<HotPotFlavorResponseModel>>(paginatedHotPotFlavors);
        }
    }
}
