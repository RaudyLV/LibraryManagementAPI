using Application.Wrrappers;
using MediatR;

namespace Application.DTOs.Users
{
    public class AuthenticateRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
