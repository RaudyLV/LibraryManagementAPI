using Application.Interfaces;
using Application.Wrrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Loans.Commands.NewFolder
{
    public class CreateLoanCommand : IRequest<Response<int>>
    {
        public int BookId { get; set; }
        public string UserId { get; set; }

    }

    public class CreateLoanCommandHandler : IRequestHandler<CreateLoanCommand, Response<int>>
    {
        private readonly ILoanService _loanService;
        private readonly IRepositoryAsync<Book> _bookRepository;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        public CreateLoanCommandHandler(IMapper mapper, IUserService userService,
                                        IRepositoryAsync<Book> bookRepository, ILoanService loanService)
        {
            _mapper = mapper;
            _userService = userService;
            _bookRepository = bookRepository;
            _loanService = loanService;
        }

        public async Task<Response<int>> Handle(CreateLoanCommand request, CancellationToken cancellationToken)
        {

            var book = await _bookRepository.GetByIdAsync(request.BookId);

            if (book == null)
            {
                throw new KeyNotFoundException("The book was not found.");
            }

            var userDto = await _userService.GetByIdAsync(request.UserId);
            if (userDto == null)
            {
                throw new KeyNotFoundException("The userDto was not found.");
            }

            var loan = _mapper.Map<Loan>(request);
            loan.ReturnDate = DateTime.UtcNow.AddDays(7);

            await _loanService.AddLoanAsync(loan);


            return new Response<int>(loan.LoanId, "Loan created successfully");
        }

    }
}
