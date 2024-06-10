using HotPotToYou.Controllers.ResponseType;
using HotPotToYou.Service.Jwt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.Models.RequestModels;
using Repository.Models.ResponseModels;
using Service.Users;
using System.Net.Mime;

namespace HotPotToYou.Controllers
{
    [ApiController]
    //[Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IJwtService _jwtService;

        public UserController(IUserService userService, IJwtService jwtService)
        {
            _userService = userService;
            _jwtService = jwtService;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("user/login")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(JsonResponse<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<JsonResponse<string>>> Login(
                       [FromBody] LoginRequestModel loginRequest)
        {
            var result = await _userService.Login(loginRequest);
            var token = _jwtService.CreateToken(result.ID, result.Role);
            return Ok(new JsonResponse<string>(token));
        }

        [AllowAnonymous]
        [HttpPost("user")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(JsonResponse<Guid>), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<JsonResponse<Guid>>> CreateUser(
            [FromBody] UserRequestModel user)
        {
            var result = await _userService.CreateUser(user);
            return Ok(new JsonResponse<string>(result));
        }

        [HttpGet("user")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(JsonResponse<Guid>), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<JsonResponse<UserResponseModel>>>> GetUsers(string? search, string? gender, string? sortBy, int pageIndex, int pageSize)
        {
            var result = await _userService.GetUsers(search, gender, sortBy, pageIndex, pageSize);
            return Ok(new JsonResponse<List<UserResponseModel>>(result));
        }

        [HttpDelete("user")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(JsonResponse<Guid>), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<JsonResponse<Guid>>> DeleteUser(int id)
        {
            var result = await _userService.DeleteUser(id);
            return Ok(new JsonResponse<string>(result));
        }
    }
}
