using Application.DTOs.Users;
using Application.Interfaces;
using Application.Wrrappers;
using AutoMapper;
using MediatR;

namespace Application.Features.Users.Querys.GetUserById
{
    public class GetUserByIdQuery : IRequest<Response<UserDTO>>
    {
        public string UserId { get; set; }

        public class GetUserByIdHandler : IRequestHandler<GetUserByIdQuery, Response<UserDTO>>
        {
            private readonly IUserService _userService;
            private readonly IMapper _mapper;
            public GetUserByIdHandler(IUserService userService, IMapper mapper)
            {
                _userService = userService;
                _mapper = mapper;
            }

            public async Task<Response<UserDTO>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
            {
                var user= await _userService.GetByIdAsync(request.UserId);

                if (user == null)
                {
                    throw new KeyNotFoundException("The user was not found");
                }

                var userDTO = _mapper.Map<UserDTO>(user);

                return new Response<UserDTO>(userDTO);
            }
        }
    }
}
