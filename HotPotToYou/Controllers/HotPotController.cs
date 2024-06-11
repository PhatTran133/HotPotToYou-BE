using HotPotToYou.Controllers.ResponseType;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.Models.RequestModels;
using Repository.Models.ResponseModels;
using Service.HotPots;

namespace HotPotToYou.Controllers
{
    [Route("api/v1")]
    [ApiController]
    public class HotPotController : ControllerBase
    {
        private readonly IHotPotService _hotPotService;

        public HotPotController(IHotPotService hotPotService)
        {
            _hotPotService = hotPotService;
        }

        [HttpPost("hotpot")]
        public async Task<ActionResult<JsonResponse<Guid>>> CreateHotPot(
            [FromBody] CreateHotPotRequestModel hotpot)
        {
            try
            {
                var result = await _hotPotService.CreateHotPot(hotpot);
                return Ok(new JsonResponse<string>(result));
            }
            catch (Exception ex)
            {
                return BadRequest(new JsonResponse<string>(ex.Message));
            }
        }

        [HttpPost("hotpot/update")]
        public async Task<ActionResult<JsonResponse<Guid>>> UpdateHotPot(
            [FromBody] UpdateHotPotRequestModel hotpot)
        {
            try
            {
                var result = await _hotPotService.UpdateHotPot(hotpot);
                return Ok(new JsonResponse<string>(result));
            }
            catch (Exception ex)
            {
                return BadRequest(new JsonResponse<string>(ex.Message));
            }
        }

        [HttpGet("hotpot")]
        public async Task<ActionResult<List<JsonResponse<HotPotResponseModel>>>> GetHotPots(string? search, string? sortBy,
            decimal? fromPrice, decimal? toPrice,
            string? size,
            int pageIndex, int pageSize)
        {
            try
            {
                var result = await _hotPotService.GetHotPots(search, sortBy, fromPrice, toPrice, size, pageIndex, pageSize);
                return Ok(new JsonResponse<List<HotPotResponseModel>>(result));
            }
            catch (Exception ex)
            {
                return BadRequest(new JsonResponse<string>(ex.Message));
            }
        }

        [HttpDelete("hotpot")]
        public async Task<ActionResult<JsonResponse<string>>> DeleteHotPot(int id)
        {
            try
            {
                var result = await _hotPotService.DeleteHotPot(id);
                return Ok(new JsonResponse<string>(result));
            }
            catch (Exception ex)
            {
                return BadRequest(new JsonResponse<string>(ex.Message));
            }
        }

        [HttpGet("hotpot/get-hotpot-by-id")]
        public async Task<ActionResult<JsonResponse<HotPotResponseModel>>> GetHotPotByID(int id)
        {
            try
            {
                var result = await _hotPotService.GetHotPotByID(id);
                return Ok(new JsonResponse<HotPotResponseModel>(result));
            }
            catch (Exception ex)
            {
                return BadRequest(new JsonResponse<string>(ex.Message));
            }

        }
    }
}
