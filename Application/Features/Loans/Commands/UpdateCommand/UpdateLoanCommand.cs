using Application.Interfaces;
using Application.Wrrappers;
using AutoMapper;
using MediatR;

namespace Application.Features.Loans.Commands.UpdateCommand
{
    public class UpdateLoanCommand : IRequest<Response<int>>
    {
        public int LoanId { get; set; }
        public string UserId { get; set; }
        public bool isReturned { get; set; }
    }

    public class UpdateLoanCommandHandler : IRequestHandler<UpdateLoanCommand, Response<int>>
    {
        private readonly IMapper _mapper;
        private readonly ILoanService _loanService;

        public UpdateLoanCommandHandler(IMapper mapper, ILoanService loanService)
        {
            _mapper = mapper;
            _loanService = loanService;
        }

        public async Task<Response<int>> Handle(UpdateLoanCommand request, CancellationToken cancellationToken)
        {
            var loan = await _loanService.GetLoanAsync(request.LoanId);

            var updatedLoan = _mapper.Map(request, loan);

            await _loanService.UpdateLoanAsycn(updatedLoan);

            return new Response<int>("The book was returned succesfully.");
        }
    }
}
