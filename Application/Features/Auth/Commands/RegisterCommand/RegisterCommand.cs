using Application.DTOs.Users;
using Application.Interfaces;
using Application.Wrrappers;
using MediatR;
using System.Net;

namespace Application.Features.Users.Commands.RegisterCommand
{
    public class RegisterCommand : IRequest<Response<string>>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address {  get; set; }
        public DateTime BirthDate { get; set; }
        public string ContactNumber { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Origin { get; set; }
    }

    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, Response<string>>
    {
        private readonly IAccountService _accountService;

        public RegisterCommandHandler(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public async Task<Response<string>> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            return await _accountService.RegisterAsync(new RegisterRequest
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Password = request.Password,
                ConfirmPassword = request.ConfirmPassword,
                Address = request.Address,
                BirthDate = request.BirthDate,
                ContactNumber = request.ContactNumber
            }, request.Origin); // OJO AQUI
        }
    }
}
