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
    [Route("api/[controller]")]
    public class ActivityTypeController : ControllerBase
    {
        private readonly IActivityTypeService _activityTypeService;

        public ActivityTypeController(IActivityTypeService activityTypeService)
        {
            _activityTypeService = activityTypeService;
        }

        [HttpPost]
        public async Task<IActionResult> Add(ActivityTypeModel model)
        {
            var result = await _activityTypeService.AddActivityTypeAsync(model);
            if (result == "Create Successfully")
                return Ok(result);
            else
                return BadRequest(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update(ActivityTypeRequest model)
        {
            var result = await _activityTypeService.UpdateActivityTypeAsync(model);
            if (result == "Update Successfully")
                return Ok(result);
            else
                return BadRequest(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _activityTypeService.DeleteActivityTypeAsync(id);
            if (result == "Delete Successfully")
                return Ok(result);
            else
                return BadRequest(result);
        }
    }
}