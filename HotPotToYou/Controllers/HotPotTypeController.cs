using HotPotToYou.Controllers.ResponseType;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.Entity.ConfigTable;
using Repository.Models.RequestModels;
using Repository.Models.RequestModels.HotPotType;
using Repository.Models.ResponseModels;
using Service.HotPotType;

namespace HotPotToYou.Controllers
{
    [Route("api/v1")]
    [ApiController]
    public class HotPotTypeController : ControllerBase
    {
        private readonly IHotPotTypeService _hotPotTypeService;
        public HotPotTypeController(IHotPotTypeService hotPotTypeService)
        {
            _hotPotTypeService = hotPotTypeService;
        }

        [HttpPost("hotpot-type")]
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

        [HttpPut("hotpot-type")]
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

        [HttpDelete("hotpot-type")]
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

        [HttpGet("hotpot-type")]
        public async Task<ActionResult<List<JsonResponse<HotPotTypeResponseModel>>>> GetHotPotTypes(string? search, string? sortBy,
            int pageIndex, int pageSize)
        {
            try
            {
                var result = await _hotPotTypeService.GetHotPotTypes(search, sortBy, pageIndex, pageSize);
                return Ok(new JsonResponse<List<HotPotTypeResponseModel>>(result));
            }
            catch (Exception ex)
            {
                return BadRequest(new JsonResponse<string>(ex.Message));
            }
        }

        [HttpGet("hotpot-type/get-hotpot-type-by-id")]
        public async Task<ActionResult<JsonResponse<HotPotTypeResponseModel>>> GetHotPotTypeByID(int id)
        {
            try
            {
                var result = await _hotPotTypeService.GetHotPotTypeByID(id);
                return Ok(new JsonResponse<HotPotTypeResponseModel>(result));
            }
            catch (Exception ex)
            {
                return BadRequest(new JsonResponse<string>(ex.Message));
            }

        }
    }
}
