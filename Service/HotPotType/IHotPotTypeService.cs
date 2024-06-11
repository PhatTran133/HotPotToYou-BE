using Repository.Entity.ConfigTable;
using Repository.Models.RequestModels;
using Repository.Models.RequestModels.HotPotType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.HotPotType
{
    public interface IHotPotTypeService
    {
        Task<string> AddHotPotTypeAsync(HotPotTypeModel hotPotType);
        Task<string> UpdateHotPotType(HotPotTypeRequest hotPotType);
        Task<string> DeleteHotPotType(int id);
    }
}
