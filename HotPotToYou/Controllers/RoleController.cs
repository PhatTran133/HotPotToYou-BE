using HotPotToYou.Controllers.ResponseType;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.Models.RequestModels;
using Repository.Models.ResponseModels;
using Service.Roles;
using System.Net.Mime;

namespace HotPotToYou.Controllers
{
    [Route("api/v1")]
    [ApiController]
    //[Authorize]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpPost("role")]
        public async Task<ActionResult<JsonResponse<string>>> CreateRole(
            [FromBody] RoleRequestModel role)
        {
            try
            {
                var result = await _roleService.CreateRole(role);
                return Ok(new JsonResponse<string>(result));
            }
            catch (Exception ex)
            {
                return BadRequest(new JsonResponse<string>(ex.Message));
            }
        }

        [HttpGet("role")]
        public async Task<ActionResult<List<JsonResponse<RoleResponseModel>>>> GetRoles()
        {
            try
            {
                var result = await _roleService.GetRoles();
                return Ok(new JsonResponse<List<RoleResponseModel>>(result));
            }
            catch (Exception ex)
            {
                return BadRequest(new JsonResponse<string>(ex.Message));
            }
        }
    }
}
