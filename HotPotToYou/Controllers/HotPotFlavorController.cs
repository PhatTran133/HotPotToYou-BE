using HotPotToYou.Controllers.ResponseType;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.Models.RequestModels;
using Repository.Models.ResponseModels;
using Service.HotPotFlavors;

namespace HotPotToYou.Controllers
{
    [Route("api/v1")]
    [ApiController]
    public class HotPotFlavorController : ControllerBase
    {
        private readonly IHotPotFlavorService _hotPotFlavorService;

        public HotPotFlavorController(IHotPotFlavorService hotPotFlavorService)
        {
            _hotPotFlavorService = hotPotFlavorService;
        }

        [HttpPost("hotpot-flavor")]
        public async Task<IActionResult> CreateHotPotFlavor([FromBody] CreateHotPotFlavorRequestModel hotPotFlavor)
        {
            try
            {
                var result = await _hotPotFlavorService.CreateHotPotFlavor(hotPotFlavor);
                return Ok(new JsonResponse<string>(result));
            }
            catch (Exception ex)
            {
                return BadRequest(new JsonResponse<string>(ex.Message));
            }
        }

        [HttpGet("hotpot-flavor")]
        public async Task<ActionResult<List<JsonResponse<HotPotFlavorResponseModel>>>> GetHotPotFlavors(string? search, string? sortBy,
           int pageIndex, int pageSize)
        {
            try
            {
                var result = await _hotPotFlavorService.GetHotPotFlavors(search, sortBy, pageIndex, pageSize);
                return Ok(new JsonResponse<List<HotPotFlavorResponseModel>>(result));
            }
            catch (Exception ex)
            {
                return BadRequest(new JsonResponse<string>(ex.Message));
            }
        }
    }
}
