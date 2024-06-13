using System.Threading.Tasks;
using Repository.Models.RequestModels;
using Repository.Models.RequestModels.IngredientGroup;

namespace Repository.IngredientGroupRepository
{
    public interface IIngredientGroupRepository
    {
        Task<string> AddAsync(IngredientGroupModel model);
        Task<string> UpdateAsync(IngredientGroupModel model,int id);
        Task<string> DeleteAsync(int id);
    }
}
