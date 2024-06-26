﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Repository.DbContexts;
using Repository.Entity.ConfigTable;
using Repository.Models.RequestModels.IngredientGroup;
using Repository.Models.ResponseModels;
using Repository.Service.Paging;
using Service.CurrentUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.IngredientGroupRepository
{
    public class IngredientGroupRepository :IIngredientGroupRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMapper _mapper;

        public IngredientGroupRepository(ApplicationDbContext context, IMapper mapper, ICurrentUserService currentUserService)
        {
            _context = context;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }

        public async Task<string> AddAsync(IngredientGroupModel model)
        {
            var newIngredientGroup = new IngredientGroupEntity()
            {
                Name = model.Name,
                CreateByID = _currentUserService.UserId,
                CreateDate = DateTime.Now
            };
            _context.IngredientGroup.Add(newIngredientGroup);
            if (await _context.SaveChangesAsync() > 0)
                return "Create Successfully";
            else
                return "Create Failed";
        }

        public async Task<string> UpdateAsync(IngredientGroupModel model,int id)
        {
            var ingredientGroup = await _context.IngredientGroup.SingleOrDefaultAsync(x => x.ID == id && x.DeleteDate == null);

            if (ingredientGroup == null) return "Ingredient Group not existed";

            ingredientGroup.Name = model.Name;
            ingredientGroup.UpdateByID = _currentUserService.UserId;
            ingredientGroup.UpdateDate = DateTime.Now;

            _context.IngredientGroup.Update(ingredientGroup);
            if (await _context.SaveChangesAsync() > 0)
                return "Update Successfully";
            else
                return "Update Failed";
        }

        public async Task<string> DeleteAsync(int id)
        {
            var ingredientGroup = await _context.IngredientGroup.SingleOrDefaultAsync(x => x.ID == id);

            if (ingredientGroup == null) return "Ingredient Group not existed";
            else
            {
                ingredientGroup.DeleteByID = _currentUserService.UserId;
                ingredientGroup.DeleteDate = DateTime.Now;
                _context.IngredientGroup.Update(ingredientGroup);
                if (await _context.SaveChangesAsync() > 0)
                    return "Delete Successfully";
                else
                    return "Delete Failed";
            }
        }

        public async Task<List<IngredientGroupResponseModel>> GetIngredientGroups(string? search, string? sortBy,
            int pageIndex, int pageSize)
        {
            IQueryable<IngredientGroupEntity> ingredientGroups = _context.IngredientGroup.Where(x => x.DeleteDate == null);

            //TÌM THEO TÊN
            if (!string.IsNullOrEmpty(search))
            {
                ingredientGroups = ingredientGroups.Where(x => x.Name.Contains(search));
            }


            //SORT THEO TÊN
            if (!string.IsNullOrEmpty(sortBy))
            {
                if (sortBy.Equals("ascName"))
                {
                    ingredientGroups = ingredientGroups.OrderBy(x => x.Name);
                }
                else if (sortBy.Equals("descName"))
                {
                    ingredientGroups = ingredientGroups.OrderByDescending(x => x.Name);
                }
            }


            var paginatedIngredientGroups = PaginatedList<IngredientGroupEntity>.Create(ingredientGroups, pageIndex, pageSize);

            return _mapper.Map<List<IngredientGroupResponseModel>>(paginatedIngredientGroups);
        }

        public async Task<IngredientGroupResponseModel> GetIngredientGroupByID(int id)
        {
            var ingredientGroup = await _context.IngredientGroup.SingleOrDefaultAsync(x => x.ID == id && x.DeleteDate == null);
            if (ingredientGroup == null)
                throw new Exception("Ingredient Group is not found");

            return _mapper.Map<IngredientGroupResponseModel>(ingredientGroup);
        }
    }
}
