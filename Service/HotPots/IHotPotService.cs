using Repository.Models.RequestModels;
using Repository.Models.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.HotPots
{
    public interface IHotPotService
    {
        Task<string> CreateHotPot(CreateHotPotRequestModel hotPot);
        Task<string> UpdateHotPot(UpdateHotPotRequestModel hotPot);
        Task<string> DeleteHotPot(int id);
        Task<List<HotPotResponseModel>> GetHotPots(string? search, string? sortBy,
            decimal? fromPrice, decimal? toPrice,
            int? flavorID, string? size, int? typeID,
            int pageIndex, int pageSize);
        Task<HotPotResponseModel> GetHotPotByID(int id);
    }
}
