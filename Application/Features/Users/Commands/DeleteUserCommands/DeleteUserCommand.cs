using Application.Interfaces;
using Application.Wrrappers;
using MediatR;

namespace Application.Features.Users.Commands.DeleteUserCommands
{
    public class DeleteUserCommand : IRequest<Response<int>>
    {
        public string id { get; set; }
    }

    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, Response<int>>
    {
        private readonly IUserService _userService;
        public DeleteUserCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<Response<int>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userService.GetByIdAsync(request.id);
            if (user == null) return new Response<int>("The user was not found.");

            await _userService.DeleteUserAsync(user.Id);

            return new Response<int>("The user was deleted succesfully");
        }
    }
}
