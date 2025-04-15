using Application.Interfaces;
using Application.Wrrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Books.Commands.CreateBookCommands
{
    public class CreateBookCommand : IRequest<Response<int>>
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }
        public string? Description { get; set; }
        public double Price { get; set; }
        public int InitialStock { get; set; }
    }

    public class CreateBookCommandHandler : IRequestHandler<CreateBookCommand, Response<int>>
    {
        private readonly IBookService _bookService;
        private readonly ILoggerService<Book> _logger;
        private readonly IMapper _mapper;
        public CreateBookCommandHandler(ILoggerService<Book> logger, IMapper mapper, IBookService bookService)
        {
            _logger = logger;
            _mapper = mapper;
            _bookService = bookService;
        }

        public async Task<Response<int>> Handle(CreateBookCommand request, CancellationToken cancellationToken)
        {
            var newBook = _mapper.Map<Book>(request);
            newBook.InitializeStock(request.InitialStock);

            try
            {
                await _bookService.AddBookAsync(newBook);
                return new Response<int>(newBook.Id, "Book created succesfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new Response<int>(ex.Message);
            }
        }
    }
}
