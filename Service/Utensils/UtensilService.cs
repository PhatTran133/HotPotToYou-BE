using Repository.Models.RequestModels;
using Repository.Models.ResponseModels;
using Repository.Utensils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Utensils
{
    public class UtensilService : IUtensilService
    {
        private readonly IUtensilRepository _repository;

        public UtensilService(IUtensilRepository repository)
        {
            _repository = repository;
        }

        public async Task<string> CreateUtensil(CreateUtensilRequestModel utensil)
        {
            return await _repository.CreateUtensil(utensil);
        }

        public async Task<string> DeleteUtensil(int id)
        {
            return await _repository.DeleteUtensil(id);
        }

        public async Task<UtensilResponseModel> GetPotByID(int id)
        {
            return await _repository.GetPotByID(id);
        }

        public async Task<List<UtensilResponseModel>> GetPots(string? name, string? sortBy, decimal? fromPrice, decimal? toPrice, string? size, int pageIndex, int pageSize)
        {
            return await _repository.GetPots(name, sortBy, fromPrice, toPrice, size, pageIndex, pageSize);
        }

        public async Task<UtensilResponseModel> GetUtensilByID(int id)
        {
            return await _repository.GetUtensilByID(id);
        }

        public async Task<List<UtensilResponseModel>> GetUtensils(string? name, string? sortBy, decimal? fromPrice, decimal? toPrice, string? size, int pageIndex, int pageSize)
        {
            return await _repository.GetUtensils(name, sortBy, fromPrice, toPrice, size, pageIndex, pageSize);
        }

        public async Task<string> UpdateUtensil(UpdateUtensilRequestModel utensil)
        {
            return await _repository.UpdateUtensil(utensil);
        }
    }
}
