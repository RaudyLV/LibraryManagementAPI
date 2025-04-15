using Application.Interfaces;
using Application.Wrrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Books.Commands.DeleteBookCommands
{
    public class DeleteBookCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
    }

    public class DeleteBookCommandHandler : IRequestHandler<DeleteBookCommand, Response<int>>
    {
        private readonly IRepositoryAsync<Book> _repositoryAsync;
        private readonly ILoggerService<Book> _logger;
        private readonly IMapper _mapper;
        public DeleteBookCommandHandler(IRepositoryAsync<Book> repositoryAsync, ILoggerService<Book> logger, IMapper mapper)
        {
            _repositoryAsync = repositoryAsync;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<Response<int>> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
        {
            var book = await _repositoryAsync.GetByIdAsync(request.Id);
            if (book == null)
            {
                throw new KeyNotFoundException($"The book with Id {request.Id} was not found");
            }

            await _repositoryAsync.DeleteAsync(book);
            await _repositoryAsync.SaveChangesAsync();

            return new Response<int>(book.Id);
        }
    }
}
