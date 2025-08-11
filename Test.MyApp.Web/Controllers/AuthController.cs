using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Test.MyApp.Application.Services;
using Test.MyApp.Domain.Common;
using Test.MyApp.Domain.DTO.Request;
using Test.MyApp.Domain.DTO.Response;

namespace Test.MyApp.Web.Controllers
{
    public class AccountController(IAuthenticateService authenService) : BaseController
    {
        [HttpPost("Login")]
        [ProducesResponseType(typeof(Result<TokenResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            Result<TokenResponse> result = await authenService.LoginAsync(request);
            return StatusCode((int)result.StatusCode, result);
        }
        [HttpPost("RefreshToken")]
        [ProducesResponseType(typeof(Result<TokenResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RefreshToken([FromBody] string requestToken)
        {
            Result<TokenResponse> result = await authenService.RefreshTokenAsync(requestToken);
            return StatusCode((int)result.StatusCode, result);
        }
    }
}
