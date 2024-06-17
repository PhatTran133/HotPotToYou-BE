using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Repository.Entity.ConfigTable;
using Repository.Models.RequestModels;
using Repository.Models.RequestModels.IngredientGroup;
using Repository.Models.ResponseModels;
using Repository.Service.Paging;

namespace Repository.IngredientGroupRepository
{
    public interface IIngredientGroupRepository
    {
        Task<string> AddAsync(IngredientGroupModel model);
        Task<string> UpdateAsync(IngredientGroupModel model,int id);
        Task<string> DeleteAsync(int id);
        Task<List<IngredientGroupResponseModel>> GetIngredientGroups(string? search, string? sortBy,
            int pageIndex, int pageSize);
        Task<IngredientGroupResponseModel> GetIngredientGroupByID(int id);
    }
}
