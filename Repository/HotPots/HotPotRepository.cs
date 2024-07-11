using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Repository.DbContexts;
using Repository.Entity;
using Repository.Models.RequestModels;
using Repository.Models.ResponseModels;
using Repository.Service.Paging;
using Service.CurrentUser;
using Service.Password;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.HotPots
{
    public class HotPotRepository : IHotPotRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;
        private readonly IPasswordService _passwordService;

        public HotPotRepository(ApplicationDbContext context, IMapper mapper, ICurrentUserService currentUserService, IPasswordService passwordService)
        {
            _context = context;
            _mapper = mapper;
            _currentUserService = currentUserService;
            _passwordService = passwordService;
        }

        public async Task<string> CreateHotPot(CreateHotPotRequestModel hotPot)
        {
            var checkType = await _context.HotPotType.AnyAsync(x => x.ID == hotPot.TypeID && x.DeleteDate == null);
            if (!checkType)
                throw new InvalidDataException("Hot Pot Type is not found");

            var checkFlavor = await _context.HotPotFlavor.AnyAsync(x => x.ID == hotPot.FlavorID && x.DeleteDate == null);
            if (!checkFlavor)
                throw new InvalidDataException("Hot Pot Flavor is not found");

            var newHotPot = new HotPotEntity()
            {
                Name = hotPot.Name,
                Size = hotPot.Size,
                ImageUrl = hotPot.ImageUrl,
                Description = hotPot.Description,
                Quantity = hotPot.Quantity,
                Price = hotPot.Price,
                FlavorID = hotPot.FlavorID,
                TypeID = hotPot.TypeID,
                CreateByID = _currentUserService.UserId,
                CreateDate = DateTime.Now,
            };

            _context.HotPot.Add(newHotPot);
            try
            {
                await _context.SaveChangesAsync();
                return "Create HotPot Successfully";
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("Error saving changes", ex);
            }
        }

        public async Task<string> UpdateHotPot(UpdateHotPotRequestModel hotPot)
        {
            var hotPotEntity = await _context.HotPot.SingleOrDefaultAsync(x => x.ID == hotPot.ID && x.DeleteDate == null);
            if (hotPotEntity == null)
                throw new InvalidDataException("Hot Pot is not found");

            var checkType = await _context.HotPotType.AnyAsync(x => x.ID == hotPot.TypeID && x.DeleteDate == null);
            if (checkType)
                throw new InvalidDataException("Hot Pot Type is not found");

            hotPotEntity.Name = hotPot.Name;
            hotPotEntity.Size = hotPot.Size;
            hotPotEntity.ImageUrl = hotPot.ImageUrl;
            hotPotEntity.Description = hotPot.Description;
            hotPotEntity.Quantity = hotPot.Quantity;
            hotPotEntity.Price = hotPot.Price;
            hotPotEntity.FlavorID = hotPot.FlavorID;
            hotPotEntity.TypeID = hotPot.TypeID;
            hotPotEntity.UpdateByID = _currentUserService.UserId;
            hotPotEntity.UpdateDate = DateTime.Now;

            _context.HotPot.Update(hotPotEntity);
            if (await _context.SaveChangesAsync() > 0)
                return "Update HotPot Successfully";
            else
                return "Update HotPot Failed";
        }

        public async Task<string> DeleteHotPot(int id)
        {
            var hotPot = await _context.HotPot.SingleOrDefaultAsync(x => x.ID == id && x.DeleteDate == null);
            if (hotPot == null)
                throw new InvalidDataException("Hot Pot is not found");

            hotPot.DeleteByID = _currentUserService.UserId;
            hotPot.DeleteDate = DateTime.Now;

            _context.HotPot.Update(hotPot);
            if (await _context.SaveChangesAsync() > 0)
                return "Delete HotPot Successfully";
            else
                return "Delete HotPot Failed";
        }

        public async Task<List<HotPotResponseModel>> GetHotPots(string? search, string? sortBy, 
            decimal? fromPrice, decimal? toPrice,
            int? flavorID, string? size, int? typeID,
            int pageIndex, int pageSize)
        {
            IQueryable<HotPotEntity> hotPots = _context.HotPot.Include(x => x.HotPotType).Include(x => x.HotPotFlavor).Where(x => x.DeleteDate == null);

            //TÌM THEO TÊN
            if (!string.IsNullOrEmpty(search))
            {
                hotPots = hotPots.Where(x => x.Name.Contains(search));
            }

            //TÌM THEO VỊ
            if (flavorID.HasValue)
            {
                hotPots = hotPots.Where(x => x.FlavorID == flavorID);
            }

            //FILTER THEO SIZE
            if (!string.IsNullOrEmpty(size))
            {
                hotPots = hotPots.Where(x => x.Size.Equals(size));
            }

            //TÌM THEO TÊN
            if (typeID.HasValue)
            {
                hotPots = hotPots.Where(x => x.TypeID == typeID);
            }

            // FILTER THEO GIÁ
            if (fromPrice.HasValue)
            {
                hotPots = hotPots.Where(x => x.Price >= fromPrice.Value);
            }   

            if (toPrice.HasValue)
            {
                hotPots = hotPots.Where(x => x.Price <= toPrice.Value);
            }

            //SORT THEO TÊN
            if (!string.IsNullOrEmpty(sortBy))
            {
                if (sortBy.Equals("ascName"))
                {
                    hotPots = hotPots.OrderBy(x => x.Name);
                }
                else if (sortBy.Equals("descName"))
                {
                    hotPots = hotPots.OrderByDescending(x => x.Name);
                }
            }

            //SORT THEO GIÁ
            if (!string.IsNullOrEmpty(sortBy))
            {
                if (sortBy.Equals("ascPrice"))
                {
                    hotPots = hotPots.OrderBy(x => x.Price);
                }
                else if (sortBy.Equals("descPrice"))
                {
                    hotPots = hotPots.OrderByDescending(x => x.Price);
                }
            }

            var paginatedUsers = PaginatedList<HotPotEntity>.Create(hotPots, pageIndex, pageSize);

            return _mapper.Map<List<HotPotResponseModel>>(paginatedUsers);
        }

        public async Task<HotPotResponseModel> GetHotPotByID(int id)
        {
            var hotpot = await _context.HotPot.Include(x => x.HotPotType).SingleOrDefaultAsync(x => x.ID == id && x.DeleteDate == null);
            if (hotpot == null)
                throw new Exception("Hot Pot is not found");

            return _mapper.Map<HotPotResponseModel>(hotpot);
        }
    }
}
