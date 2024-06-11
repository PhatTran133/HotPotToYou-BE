using Repository.HotPots;
using Repository.Models.RequestModels;
using Repository.Models.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.HotPots
{
    public class HotPotService : IHotPotService
    {
        private readonly IHotPotRepository _hotPotRepository;

        public HotPotService(IHotPotRepository hotPotRepository)
        {
            _hotPotRepository = hotPotRepository;
        }

        public async Task<string> CreateHotPot(CreateHotPotRequestModel hotPot)
        {
            return await _hotPotRepository.CreateHotPot(hotPot);
        }

        public async Task<string> DeleteHotPot(int id)
        {
            return await _hotPotRepository.DeleteHotPot(id);
        }

        public async Task<HotPotResponseModel> GetHotPotByID(int id)
        {
            return await _hotPotRepository.GetHotPotByID(id);
        }

        public async Task<List<HotPotResponseModel>> GetHotPots(string? search, string? sortBy, 
            decimal? fromPrice, decimal? toPrice, 
            string? size, 
            int pageIndex, int pageSize)
        {
            return await _hotPotRepository.GetHotPots(search, sortBy, fromPrice, toPrice, size, pageIndex, pageSize);
        }

        public async Task<string> UpdateHotPot(UpdateHotPotRequestModel hotPot)
        {
            return await _hotPotRepository.UpdateHotPot(hotPot);
        }
    }
}
