using Application.DTOs;
using Application.Interfaces;
using Application.Specifications.Loans;
using Application.Wrrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Loans.Querys.GetAllLoans
{
    public class GetLoansWithPaginationQuery : IRequest<PagedResponse<LoanDTO>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string? Filter { get; set; }  // Filtrar por nombre de usuario o libro

        public GetLoansWithPaginationQuery(int pageNumber, int pageSize, string? filter = null)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            Filter = filter;
        }
    }

    public class GetLoansWithPaginationHandler : IRequestHandler<GetLoansWithPaginationQuery, PagedResponse<LoanDTO>>
    {
        private readonly IRepositoryAsync<Loan> _repository;
        private readonly IMapper _mapper;

        public GetLoansWithPaginationHandler(IRepositoryAsync<Loan> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<PagedResponse<LoanDTO>> Handle(GetLoansWithPaginationQuery request, CancellationToken cancellationToken)
        {
            var spec = new LoanWithUserAndBookSpec(request.PageNumber, request.PageSize, request.Filter);
            var loans = await _repository.ListAsync(spec, cancellationToken);

            var totalCount = await _repository.CountAsync(new LoanWithUserAndBookSpec(1, int.MaxValue, request.Filter));

            var loanDtos = _mapper.Map<List<LoanDTO>>(loans);

            return new PagedResponse<LoanDTO>(loanDtos, totalCount, request.PageNumber, request.PageSize);
        }
    }
}
