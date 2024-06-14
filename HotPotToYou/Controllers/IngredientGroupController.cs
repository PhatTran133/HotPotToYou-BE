using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.Models.RequestModels.IngredientGroup;
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
    }
}
