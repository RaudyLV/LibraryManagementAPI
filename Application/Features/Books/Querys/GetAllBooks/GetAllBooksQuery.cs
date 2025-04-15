using Application.DTOs;
using Application.Interfaces;
using Application.Specifications.Books;
using Application.Wrrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Books.Querys.GetBookById
{
    public class GetAllBooksQuery : IRequest<PagedResponse<BookDTO>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string? Filter { get; set; }  // Filtrar por Titulo o Autor

        public GetAllBooksQuery(int pageNumber, int pageSize, string? filter = null)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            Filter = filter;
        }

    }
    public class GetAllBookIHandler : IRequestHandler<GetAllBooksQuery, PagedResponse<BookDTO>>
    {
        private readonly IRepositoryAsync<Book> _repositoryAsync;
        private readonly IMapper _mapper;
        public GetAllBookIHandler(IRepositoryAsync<Book> repositoryAsync, IMapper mapper)
        {
            _repositoryAsync = repositoryAsync;
            _mapper = mapper;
        }

        public async Task<PagedResponse<BookDTO>> Handle(GetAllBooksQuery request, CancellationToken cancellationToken)
        {
            var spec = new GetBooksSpec(request.PageNumber, request.PageSize, request.Filter);
            var books = await _repositoryAsync.ListAsync(spec, cancellationToken);

            var totalCount = await _repositoryAsync.CountAsync(new GetBooksSpec(1, int.MaxValue, request.Filter));

            var bookDtos = _mapper.Map<List<BookDTO>>(books);
            return new PagedResponse<BookDTO>(bookDtos, totalCount, request.PageNumber, request.PageSize);
        }
    }
}

