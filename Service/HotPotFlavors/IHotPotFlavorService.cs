using Repository.Models.RequestModels;
using Repository.Models.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.HotPotFlavors
{
    public interface IHotPotFlavorService
    {
        Task<string> CreateHotPotFlavor(CreateHotPotFlavorRequestModel hotPotFlavor);
        Task<List<HotPotFlavorResponseModel>> GetHotPotFlavors(string? search, string? sortBy,
            int pageIndex, int pageSize);
    }
}
