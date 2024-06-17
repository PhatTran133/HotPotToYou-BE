using Repository.Entity.ConfigTable;
using Repository.HotPotType;
using Repository.Models.RequestModels;
using Repository.Models.RequestModels.HotPotType;
using Repository.Models.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.HotPotType
{
    public class HotPotTypeService : IHotPotTypeService
    {
        private readonly IHotPotTypeRepository _hotPotTypeRepository;
        public HotPotTypeService(IHotPotTypeRepository hotPotTypeRepository)
        {
            _hotPotTypeRepository = hotPotTypeRepository;
        }

        public async Task<string> AddHotPotTypeAsync(HotPotTypeModel hotPotType)
        {
           return await _hotPotTypeRepository.AddAsync(hotPotType);
        }

        public async Task<string> DeleteHotPotType(int id)
        {
            return await _hotPotTypeRepository.DeleteAsync(id);
        }

        public async Task<HotPotTypeResponseModel> GetHotPotTypeByID(int id)
        {
            return await _hotPotTypeRepository.GetHotPotTypeByID(id);
        }

        public async Task<List<HotPotTypeResponseModel>> GetHotPotTypes(string? search, string? sortBy, int pageIndex, int pageSize)
        {
            return await _hotPotTypeRepository.GetHotPotTypes(search, sortBy, pageIndex, pageSize);
        }

        public async Task<string> UpdateHotPotType(HotPotTypeRequest hotPotType)
        {
       
            // Proceed with the update if the ID exists
            return await _hotPotTypeRepository.UpdateAsync(hotPotType);
        }
    }
}
