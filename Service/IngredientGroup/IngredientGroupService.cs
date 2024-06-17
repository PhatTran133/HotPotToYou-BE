using AutoMapper;
using Repository.IngredientGroupRepository;
using Repository.Models.RequestModels.IngredientGroup;
using Repository.Models.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.IngredientGroup
{
    public class IngredientGroupService : IIngredientGroupService
    {
        private readonly IIngredientGroupRepository _repository;
        private readonly IMapper _mapper;

        public IngredientGroupService(IIngredientGroupRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<string> AddAsync(IngredientGroupModel model)
        {
            // Additional business logic can be added here if needed
            return await _repository.AddAsync(model);
        }

        public async Task<string> UpdateAsync(IngredientGroupModel model,int id)
        {
            // Additional business logic can be added here if needed
            return await _repository.UpdateAsync(model,id);
        }

        public async Task<string> DeleteAsync(int id)
        {
            // Additional business logic can be added here if needed
            return await _repository.DeleteAsync(id);
        }

        public async Task<List<IngredientGroupResponseModel>> GetIngredientGroups(string? search, string? sortBy, int pageIndex, int pageSize)
        {
            return await _repository.GetIngredientGroups(search, sortBy, pageIndex, pageSize);
        }

        public async Task<IngredientGroupResponseModel> GetIngredientGroupByID(int id)
        {
            return await _repository.GetIngredientGroupByID(id);
        }
    }
}
