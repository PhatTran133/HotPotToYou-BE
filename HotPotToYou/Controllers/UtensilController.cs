using HotPotToYou.Controllers.ResponseType;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.Models.RequestModels;
using Repository.Models.ResponseModels;
using Service.Utensils;

namespace HotPotToYou.Controllers
{
    [Route("api/v1")]
    [ApiController]
    public class UtensilController : ControllerBase
    {
        private readonly IUtensilService _utensilService;

        public UtensilController(IUtensilService utensilService)
        {
            _utensilService = utensilService;
        }

        [HttpPost("utensil")]
        public async Task<ActionResult<JsonResponse<Guid>>> CreateUtensil(
            [FromBody] CreateUtensilRequestModel utensil)
        {
            try
            {
                var result = await _utensilService.CreateUtensil(utensil);
                return Ok(new JsonResponse<string>(result));
            }
            catch (Exception ex)
            {
                return BadRequest(new JsonResponse<string>(ex.Message));
            }
        }

        [HttpPost("utensil/update")]
        public async Task<ActionResult<JsonResponse<Guid>>> UpdateUtensil(
            [FromBody] UpdateUtensilRequestModel utensil)
        {
            try
            {
                var result = await _utensilService.UpdateUtensil(utensil);
                return Ok(new JsonResponse<string>(result));
            }
            catch (Exception ex)
            {
                return BadRequest(new JsonResponse<string>(ex.Message));
            }
        }

        [HttpGet("utensil")]
        public async Task<ActionResult<List<JsonResponse<UtensilResponseModel>>>> GetUtensils(string? name, string? sortBy,
            decimal? fromPrice, decimal? toPrice,
            string? size, string? type,
            int pageIndex, int pageSize)
        {
            try
            {
                var result = await _utensilService.GetUtensils(name, sortBy, fromPrice, toPrice, size, type, pageIndex, pageSize);
                return Ok(new JsonResponse<List<UtensilResponseModel>>(result));
            }
            catch (Exception ex)
            {
                return BadRequest(new JsonResponse<string>(ex.Message));
            }
        }

        [HttpDelete("utensil")]
        public async Task<ActionResult<JsonResponse<string>>> DeleteUtensil(int id)
        {
            try
            {
                var result = await _utensilService.DeleteUtensil(id);
                return Ok(new JsonResponse<string>(result));
            }
            catch (Exception ex)
            {
                return BadRequest(new JsonResponse<string>(ex.Message));
            }
        }

        [HttpGet("utensil/get-utensil-by-id")]
        public async Task<ActionResult<JsonResponse<UtensilResponseModel>>> GetUtensilByID(int id)
        {
            try
            {
                var result = await _utensilService.GetUtensilByID(id);
                return Ok(new JsonResponse<UtensilResponseModel>(result));
            }
            catch (Exception ex)
            {
                return BadRequest(new JsonResponse<string>(ex.Message));
            }

        }
    }
}
