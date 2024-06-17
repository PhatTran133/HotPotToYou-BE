using Repository.Models.RequestModels.IngredientGroup;
using Repository.Models.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.IngredientGroup
{
    public interface IIngredientGroupService
    {
        Task<string> AddAsync(IngredientGroupModel model);
        Task<string> UpdateAsync(IngredientGroupModel model,int id);
        Task<string> DeleteAsync(int id);
        Task<List<IngredientGroupResponseModel>> GetIngredientGroups(string? search, string? sortBy,
            int pageIndex, int pageSize);
        Task<IngredientGroupResponseModel> GetIngredientGroupByID(int id);
    }
}
