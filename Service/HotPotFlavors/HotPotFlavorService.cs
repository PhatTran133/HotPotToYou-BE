using Repository.HotPotFlavors;
using Repository.Models.RequestModels;
using Repository.Models.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.HotPotFlavors
{
    public class HotPotFlavorService : IHotPotFlavorService
    {
        private readonly IHotPotFlavorRepository _repository;

        public HotPotFlavorService(IHotPotFlavorRepository repository)
        {
            _repository = repository;
        }

        public async Task<string> CreateHotPotFlavor(CreateHotPotFlavorRequestModel hotPotFlavor)
        {
            return await _repository.CreateHotPotFlavor(hotPotFlavor);
        }

        public async Task<List<HotPotFlavorResponseModel>> GetHotPotFlavors(string? search, string? sortBy, int pageIndex, int pageSize)
        {
            return await _repository.GetHotPotFlavors(search, sortBy, pageIndex, pageSize);
        }
    }
}
