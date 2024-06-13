using Microsoft.EntityFrameworkCore;
using Repository.Entity;
using Repository.Models.RequestModels;
using Repository.Models.ResponseModels;
using Service.CurrentUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Utensils
{
    public interface IUtensilRepository
    {
        Task<string> CreateUtensil(CreateUtensilRequestModel utensil);
        Task<string> UpdateUtensil(UpdateUtensilRequestModel utensil);
        Task<string> DeleteUtensil(int id);
        Task<List<UtensilResponseModel>> GetUtensils(string? name, string? sortBy,
            decimal? fromPrice, decimal? toPrice,
            string? size, string? type,
            int pageIndex, int pageSize);
        Task<UtensilResponseModel> GetUtensilByID(int id);
    }
}
