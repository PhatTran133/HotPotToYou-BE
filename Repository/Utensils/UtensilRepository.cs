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
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Utensils
{
    public class UtensilRepository : IUtensilRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;
        private readonly IPasswordService _passwordService;

        public UtensilRepository(ApplicationDbContext context, IMapper mapper, ICurrentUserService currentUserService, IPasswordService passwordService)
        {
            _context = context;
            _mapper = mapper;
            _currentUserService = currentUserService;
            _passwordService = passwordService;
        }

        //tạo NỒI và DỤNG CỤ
        public async Task<string> CreateUtensil (CreateUtensilRequestModel utensil)
        {

            var newUtensil = new UtensilEntity()
            {
                Name = utensil.Name,
                Material = utensil.Material,
                Size = utensil.Size,
                Quantity = utensil.Quantity,
                Price = utensil.Price,
                Type = utensil.Type,
                CreateByID = _currentUserService.UserId,
                CreateDate = DateTime.Now
            };

            _context.Utensil.Add(newUtensil);
            if (await _context.SaveChangesAsync() > 0)
                return "Create Utensil Successfully";
            else
                return "Create Utensil Failed";
        }

        public async Task<string> UpdateUtensil(UpdateUtensilRequestModel utensil)
        {
            try
            {
                var checkUtensil = await _context.Utensil.SingleOrDefaultAsync(x => x.ID == utensil.ID && x.DeleteDate == null);
                if (checkUtensil == null)
                    throw new Exception("Utensil is not found");

                checkUtensil.Name = utensil.Name;
                checkUtensil.Material = utensil.Material;
                checkUtensil.Size = utensil.Size;
                checkUtensil.Quantity = utensil.Quantity;
                checkUtensil.Price = utensil.Price;
                checkUtensil.Type = utensil.Type;
                checkUtensil.UpdateByID = _currentUserService.UserId;
                checkUtensil.UpdateDate = DateTime.Now;

                _context.Utensil.Update(checkUtensil);
                if (await _context.SaveChangesAsync() > 0)
                    return "Update Utensil Successfully";
                else
                    return "Update Utensil Failed";
            }
            catch (Exception ex)
            {
                throw new Exception("Error");
            }
        }

        public async Task<string> DeleteUtensil(int id)
        {
            var utensil = await _context.Utensil.SingleOrDefaultAsync(x => x.ID == id && x.DeleteDate == null);
            if (utensil == null)
                throw new Exception("Utensil is not found");

            utensil.DeleteByID = _currentUserService.UserId;
            utensil.DeleteDate = DateTime.Now;

            _context.Utensil.Update(utensil);
            if (await _context.SaveChangesAsync() > 0)
                return "Delete Utensil Successfully";
            else
                return "Delete Utensil Failed";
        }

        public async Task<List<UtensilResponseModel>> GetUtensils(string? name, string? sortBy,
            decimal? fromPrice, decimal? toPrice,
            string? size,
            int pageIndex, int pageSize)
        {
            IQueryable<UtensilEntity> utensils = _context.Utensil.Where(x => x.Type.ToLower().Trim().Equals("utensil") && x.DeleteDate == null);

            //TÌM THEO TÊN
            if (!string.IsNullOrEmpty(name))
            {
                utensils = utensils.Where(x => x.Name.Contains(name));
            }


            //FILTER THEO SIZE
            if (!string.IsNullOrEmpty(size))
            {
                utensils = utensils.Where(x => x.Size.Equals(size));
            }

            // FILTER THEO GIÁ
            if (fromPrice.HasValue)
            {
                utensils = utensils.Where(x => x.Price >= fromPrice.Value);
            }

            if (toPrice.HasValue)
            {
                utensils = utensils.Where(x => x.Price <= toPrice.Value);
            }

            //SORT THEO TÊN
            if (!string.IsNullOrEmpty(sortBy))
            {
                if (sortBy.Equals("ascName"))
                {
                    utensils = utensils.OrderBy(x => x.Name);
                }
                else if (sortBy.Equals("descName"))
                {
                    utensils = utensils.OrderByDescending(x => x.Name);
                }
            }

            //SORT THEO QUANTITY
            if (!string.IsNullOrEmpty(sortBy))
            {
                if (sortBy.Equals("ascQuantity"))
                {
                    utensils = utensils.OrderBy(x => x.Quantity);
                }
                else if (sortBy.Equals("descQuantity"))
                {
                    utensils = utensils.OrderByDescending(x => x.Quantity);
                }
            }

            //SORT THEO GIÁ
            if (!string.IsNullOrEmpty(sortBy))
            {
                if (sortBy.Equals("ascPrice"))
                {
                    utensils = utensils.OrderBy(x => x.Price);
                }
                else if (sortBy.Equals("descPrice"))
                {
                    utensils = utensils.OrderByDescending(x => x.Price);
                }
            }

            var paginatedUtensils = PaginatedList<UtensilEntity>.Create(utensils, pageIndex, pageSize);

            return _mapper.Map<List<UtensilResponseModel>>(paginatedUtensils);
        }
        public async Task<List<UtensilResponseModel>> GetPots(string? name, string? sortBy,
            decimal? fromPrice, decimal? toPrice,
            string? size,
            int pageIndex, int pageSize)
        {
            IQueryable<UtensilEntity> pots = _context.Utensil.Where(x => x.Type.ToLower().Trim().Equals("pot") && x.DeleteDate == null);

            //TÌM THEO TÊN
            if (!string.IsNullOrEmpty(name))
            {
                pots = pots.Where(x => x.Name.Contains(name));
            }


            //FILTER THEO SIZE
            if (!string.IsNullOrEmpty(size))
            {
                pots = pots.Where(x => x.Size.Equals(size));
            }

            // FILTER THEO GIÁ
            if (fromPrice.HasValue)
            {
                pots = pots.Where(x => x.Price >= fromPrice.Value);
            }

            if (toPrice.HasValue)
            {
                pots = pots.Where(x => x.Price <= toPrice.Value);
            }

            //SORT THEO TÊN
            if (!string.IsNullOrEmpty(sortBy))
            {
                if (sortBy.Equals("ascName"))
                {
                    pots = pots.OrderBy(x => x.Name);
                }
                else if (sortBy.Equals("descName"))
                {
                    pots = pots.OrderByDescending(x => x.Name);
                }
            }

            //SORT THEO QUANTITY
            if (!string.IsNullOrEmpty(sortBy))
            {
                if (sortBy.Equals("ascQuantity"))
                {
                    pots = pots.OrderBy(x => x.Quantity);
                }
                else if (sortBy.Equals("descQuantity"))
                {
                    pots = pots.OrderByDescending(x => x.Quantity);
                }
            }

            //SORT THEO GIÁ
            if (!string.IsNullOrEmpty(sortBy))
            {
                if (sortBy.Equals("ascPrice"))
                {
                    pots = pots.OrderBy(x => x.Price);
                }
                else if (sortBy.Equals("descPrice"))
                {
                    pots = pots.OrderByDescending(x => x.Price);
                }
            }

            var paginatedPots = PaginatedList<UtensilEntity>.Create(pots, pageIndex, pageSize);

            return _mapper.Map<List<UtensilResponseModel>>(paginatedPots);
        }
        public async Task<UtensilResponseModel> GetUtensilByID(int id)
        {
            var utensil = await _context.Utensil.SingleOrDefaultAsync(x => x.Type.ToLower().Trim().Equals("utensil") && x.ID == id && x.DeleteDate == null);
            if (utensil == null)
                throw new Exception("Utensil is not found");

            return _mapper.Map<UtensilResponseModel>(utensil);
        }
        public async Task<UtensilResponseModel> GetPotByID(int id)
        {
            var pot = await _context.Utensil.SingleOrDefaultAsync(x => x.Type.ToLower().Trim().Equals("pot") && x.ID == id && x.DeleteDate == null);
            if (pot == null)
                throw new Exception("Pot is not found");

            return _mapper.Map<UtensilResponseModel>(pot);
        }
    }
}
