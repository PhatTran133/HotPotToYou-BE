using HotPotToYou.Controllers.ResponseType;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.Models.RequestModels.IngredientGroup;
using Repository.Models.ResponseModels;
using Service.IngredientGroup;

namespace HotPotToYou.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IngredientGroupController : ControllerBase
    {
        private readonly IIngredientGroupService _ingredientGroupService;

        public IngredientGroupController(IIngredientGroupService ingredientGroupService)
        {
            _ingredientGroupService = ingredientGroupService;
        }

        [HttpPost]
        public async Task<IActionResult> AddIngredientGroup([FromBody] IngredientGroupModel model)
        {
            try
            {
                var result = await _ingredientGroupService.AddAsync(model);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateIngredientGroup(int id, [FromBody] IngredientGroupModel model)
        {
            try
            {
              
                var result = await _ingredientGroupService.UpdateAsync(model, id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIngredientGroup(int id)
        {
            try
            {
                var result = await _ingredientGroupService.DeleteAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("ingredient-group")]
        public async Task<ActionResult<List<JsonResponse<IngredientGroupResponseModel>>>> GetIngredientGroups(string? search, string? sortBy,
            int pageIndex, int pageSize)
        {
            try
            {
                var result = await _ingredientGroupService.GetIngredientGroups(search, sortBy, pageIndex, pageSize);
                return Ok(new JsonResponse<List<IngredientGroupResponseModel>>(result));
            }
            catch (Exception ex)
            {
                return BadRequest(new JsonResponse<string>(ex.Message));
            }
        }

        [HttpGet("ingredient-group/get-ingredient-group-by-id")]
        public async Task<ActionResult<JsonResponse<IngredientGroupResponseModel>>> GetIngredientGroupByID(int id)
        {
            try
            {
                var result = await _ingredientGroupService.GetIngredientGroupByID(id);
                return Ok(new JsonResponse<IngredientGroupResponseModel>(result));
            }
            catch (Exception ex)
            {
                return BadRequest(new JsonResponse<string>(ex.Message));
            }

        }
    }
}
