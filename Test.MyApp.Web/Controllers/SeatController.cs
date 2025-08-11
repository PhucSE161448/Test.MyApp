using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Test.MyApp.Application.Services;
using Test.MyApp.Domain.Common;
using Test.MyApp.Domain.DTO.Request;
using Test.MyApp.Domain.DTO.Response;

namespace Test.MyApp.Web.Controllers
{
    [Authorize]
    public class SeatController(ISeatService seatService) : BaseController
    {
        [HttpGet]
        [ProducesResponseType(typeof(PagingResult<GetSeatResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllPagination([FromQuery] int page = 1, [FromQuery] int size = 100)
        {
            PagingResult<GetSeatResponse> result = await seatService.GetPagination(x => false, page, size);
            return StatusCode((int)result.StatusCode, result);
        }
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Result<GetSeatResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            Result<GetSeatResponse> result = await seatService.GetById(id);
            return StatusCode((int)result.StatusCode, result);
        }
        [HttpPost]
        [ProducesResponseType(typeof(Result<GetSeatResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] CreateSeatRequest request)
        {
            Result<GetSeatResponse> result = await seatService.Create(request);
            return StatusCode((int)result.StatusCode, result);
        }
        [HttpPut("{id:int}")]
        [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update([FromRoute] int id, [FromForm] UpdateSeatRequest request)
        {
            Result<bool> result = await seatService.Update(id, request);
            return StatusCode((int)result.StatusCode, result);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            Result<bool> result = await seatService.Delete(id);
            return StatusCode((int)result.StatusCode, result);
        }
    }
}
