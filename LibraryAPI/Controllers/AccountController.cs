using Application.DTOs.Users;
using Application.Features.Users.Commands.AuthenticationCommand;
using Application.Features.Users.Commands.RegisterCommand;
using Microsoft.AspNetCore.Mvc;

namespace LibraryAPI.Controllers
{
    public class AccountController : BaseApiController
    {
        [HttpPost("SignIn")]
        public async Task<IActionResult> Authenticate([FromBody] AuthenticateRequest request)
        {
            return Ok(await Mediator.Send(new AuthenticateCommand
            {
                Email = request.Email,
                Password = request.Password,
                ipAdress = GetIpAddress()
            }));
        }

        [HttpPost("SignUp")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            return Ok(await Mediator.Send(new RegisterCommand
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Password = request.Password,
                ConfirmPassword = request.ConfirmPassword,
                Address = request.Address,
                ContactNumber = request.ContactNumber,
                BirthDate = request.BirthDate,
                Origin = Request.Headers["Origin"].FirstOrDefault() ?? string.Empty
            }));
        }

        #region AuxMethods
        private string GetIpAddress()
        {
            if (Request.Headers.TryGetValue("X-Forwarded-For]", out var forwardedIp) && !string.IsNullOrEmpty(forwardedIp) )
            {
                return forwardedIp.ToString();
            }

            var remoteIp = Request.HttpContext.Connection.RemoteIpAddress;
            return remoteIp != null ? remoteIp.MapToIPv4().ToString() : "Ip not available";
        }

        #endregion
    }
}
