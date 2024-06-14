using Repository.Models.RequestModels.IngredientGroup;
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
    }
}
