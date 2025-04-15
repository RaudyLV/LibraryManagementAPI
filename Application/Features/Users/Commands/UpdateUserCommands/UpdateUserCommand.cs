using Application.Interfaces;
using Application.Wrrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Users.Commands.UpdateUserCommands
{
    public class UpdateUserCommand : IRequest<Response<int>>
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ContactNumber { get; set; }
        public string Address { get; set; }
        public DateTime BirthDate { get; set; }
    }

    public class UpdateUserCommandHanlder : IRequestHandler<UpdateUserCommand, Response<int>>
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryAsync<User> _userRepository;
        public UpdateUserCommandHanlder(IMapper mapper, IRepositoryAsync<User> repositoryAsync)
        {
            _mapper = mapper;
            _userRepository = repositoryAsync;
        }

        public async Task<Response<int>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.Id);

            if (user == null) return new Response<int>("The user was not found.");

            var updatedUser = _mapper.Map(request, user);

            await _userRepository.UpdateAsync(updatedUser);
            await _userRepository.SaveChangesAsync();

            return new Response<int>("The user was updated succesfully");
        }
    }
}
