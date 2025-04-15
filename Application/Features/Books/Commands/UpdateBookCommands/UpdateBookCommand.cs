using Application.Interfaces;
using Application.Wrrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Books.Commands.UpdateBookCommands
{
    public class UpdateBookCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }
        public string? Description { get; set; }
        public double Price { get; set; }
        public int Stock { get; set; }
    }

    public class UpdateBookCommandHandler : IRequestHandler<UpdateBookCommand, Response<int>>
    {
        private readonly IRepositoryAsync<Book> _repositoryAsync;
        private readonly IMapper _mapper;
        private readonly ILoggerService<Book> _logger;

        public UpdateBookCommandHandler(IRepositoryAsync<Book> repositoryAsync, IMapper mapper, ILoggerService<Book> logger)
        {
            _repositoryAsync = repositoryAsync;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Response<int>> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
        {
            var updatedBook = await _repositoryAsync.GetByIdAsync(request.Id);
            if (updatedBook == null)
            {
                throw new KeyNotFoundException($"The book with Id {request.Id} was not found.");
            }

            try
            {
                updatedBook = _mapper.Map(request, updatedBook);
                await _repositoryAsync.UpdateAsync(updatedBook);
                await _repositoryAsync.SaveChangesAsync();

                return new Response<int>(updatedBook.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new Exception(ex.Message);
            }
        }
    }
}
