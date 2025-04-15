using Application.DTOs.Users;
using Application.Interfaces;
using Application.Wrrappers;
using MediatR;

namespace Application.Features.Users.Commands.AuthenticationCommand
{
    public class AuthenticateCommand : IRequest<Response<AuthenticationResponse>>
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string ipAdress { get; set; }
    }

    public class AuthenticateCommandHandler : IRequestHandler<AuthenticateCommand, Response<AuthenticationResponse>>
    {
        private readonly IAccountService _accountService;

        public AuthenticateCommandHandler(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public async Task<Response<AuthenticationResponse>> Handle(AuthenticateCommand request, CancellationToken cancellationToken)
        {
            return await _accountService.AuthenticateAsync(new AuthenticateRequest
            {
                Email = request.Email,
                Password = request.Password,
            }, request.ipAdress);
        }
    }
}
