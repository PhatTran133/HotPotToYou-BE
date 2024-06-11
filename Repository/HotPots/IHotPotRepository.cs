using Microsoft.EntityFrameworkCore;
using Repository.Models.RequestModels;
using Repository.Models.ResponseModels;
using Service.CurrentUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.HotPots
{
    public interface IHotPotRepository
    {
        Task<string> CreateHotPot(CreateHotPotRequestModel hotPot);
        Task<string> UpdateHotPot(UpdateHotPotRequestModel hotPot);
        Task<string> DeleteHotPot(int id);
        Task<List<HotPotResponseModel>> GetHotPots(string? search, string? sortBy,
            decimal? fromPrice, decimal? toPrice,
            string? size,
            int pageIndex, int pageSize);
        Task<HotPotResponseModel> GetHotPotByID(int id);
    }
}
