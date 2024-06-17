using Microsoft.EntityFrameworkCore;
using Repository.Entity.ConfigTable;
using Repository.Models.RequestModels;
using Repository.Models.RequestModels.HotPotType;
using Repository.Models.ResponseModels;
using Repository.Service.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.HotPotType
{
    public interface IHotPotTypeRepository
    {
      
       
        Task<string> AddAsync(HotPotTypeModel model);
        Task<string> UpdateAsync(HotPotTypeRequest entity);
        Task<string> DeleteAsync(int id);
        Task<List<HotPotTypeResponseModel>> GetHotPotTypes(string? search, string? sortBy,
            int pageIndex, int pageSize);
        Task<HotPotTypeResponseModel> GetHotPotTypeByID(int id);

    }
}
