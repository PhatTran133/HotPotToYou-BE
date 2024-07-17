using HotPotToYou.Controllers.ResponseType;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.Entity.ConfigTable;
using Repository.Models.RequestModels;
using Repository.Models.RequestModels.ActivityType;
using Repository.Models.RequestModels.HotPotType;
using Service.ActivityType;
using Service.HotPotType;

namespace HotPotToYou.Controllers
{
    [ApiController]
    [Route("api/v1")]
    public class ActivityTypeController : ControllerBase
    {
        private readonly IActivityTypeService _activityTypeService;

        public ActivityTypeController(IActivityTypeService activityTypeService)
        {
            _activityTypeService = activityTypeService;
        }

        [HttpPost("acivity-type")]
        public async Task<IActionResult> Add(ActivityTypeModel model)
        {
            try
            {
                var result = await _activityTypeService.AddActivityTypeAsync(model);
                return Ok(new JsonResponse<string>(result));
            }
            catch (Exception ex)
            {
                return BadRequest(new JsonResponse<string>(ex.Message));
            }
            
            
        }

        [HttpPut("acivity-type")]
        public async Task<IActionResult> Update(ActivityTypeRequest model)
        {
            try
            {
                var result = await _activityTypeService.UpdateActivityTypeAsync(model);
                return Ok(new JsonResponse<string>(result));
            }
            catch (Exception ex)
            {
                return BadRequest(new JsonResponse<string>(ex.Message));
            }
        }

        [HttpDelete("acivity-type")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await _activityTypeService.DeleteActivityTypeAsync(id);
                return Ok(new JsonResponse<string>(result));
            }
            catch (Exception ex)
            {
                return BadRequest(new JsonResponse<string>(ex.Message));
            }
        }
    }
}