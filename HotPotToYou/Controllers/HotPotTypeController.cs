using HotPotToYou.Controllers.ResponseType;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.Entity.ConfigTable;
using Repository.Models.RequestModels;
using Repository.Models.RequestModels.HotPotType;
using Service.HotPotType;

namespace HotPotToYou.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotPotTypeController : ControllerBase
    {
        private readonly IHotPotTypeService _hotPotTypeService;
        public HotPotTypeController(IHotPotTypeService hotPotTypeService)
        {
            _hotPotTypeService = hotPotTypeService;
        }

        [HttpPost("addHotPotType")]
        public async Task<IActionResult> AddHotPotType([FromBody] HotPotTypeModel hotPotType)
        {
            try
            {
               var result = await _hotPotTypeService.AddHotPotTypeAsync(hotPotType);
                return Ok(new JsonResponse<string>(result));
            }
            catch (Exception ex)
            {
                // Log therror or handle exception as needed
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while adding HotPotType.");
            }
        }

        [HttpPut("UpdateHotPotType")]
        public async Task<IActionResult> UpdateHotPotType([FromBody] HotPotTypeRequest hotPotType)
        {
            try
            {
                var result = await _hotPotTypeService.UpdateHotPotType(hotPotType);
                return Ok(new JsonResponse<string>(result));
            }
            catch (Exception ex)
            {
                // Log therror or handle exception as needed
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while adding HotPotType.");
            }
        }

        [HttpPut("DeleteHotPotType")]
        public async Task<IActionResult> DeleteHotPotType([FromBody] int id)
        {
            try
            {
                var result = await _hotPotTypeService.DeleteHotPotType(id);
                return Ok(new JsonResponse<string>(result));
            }
            catch (Exception ex)
            {
                // Log therror or handle exception as needed
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while adding HotPotType.");
            }
        }
    }
}
