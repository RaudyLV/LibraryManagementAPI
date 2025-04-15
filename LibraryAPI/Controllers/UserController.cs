using Application.Features.Users.Commands.DeleteUserCommands;
using Application.Features.Users.Commands.UpdateUserCommands;
using Application.Features.Users.Querys.GetUserById;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class UserController : BaseApiController
    {
        [HttpGet("{userid}")]
        public async Task<IActionResult> GetUserById(string userid)
        {
            return Ok(await Mediator.Send(new GetUserByIdQuery
            {
                UserId = userid
            }));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(string id, UpdateUserCommand request)
        {
            return Ok(await Mediator.Send(request));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            return Ok(await Mediator.Send(new DeleteUserCommand
            {
                id = id
            }));
        }

    }
}
