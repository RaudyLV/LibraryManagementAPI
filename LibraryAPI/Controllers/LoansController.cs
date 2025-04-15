using Application.Features.Books.Commands.UpdateBookCommands;
using Application.Features.Loans.Commands.NewFolder;
using Application.Features.Loans.Commands.UpdateCommand;
using Application.Features.Loans.Querys.GetAllLoans;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles ="Basic")]
    public class LoansController : BaseApiController
    {
        [HttpGet]
        public async Task<IActionResult> GetLoans([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10, [FromQuery] string? filter = null)
        {
            return Ok(await Mediator.Send(new GetLoansWithPaginationQuery(pageNumber, pageSize, filter)));
        }


        [HttpPost]
        public async Task<IActionResult> CreateLoan([FromBody] CreateLoanCommand request)
        {
            return Ok(await Mediator.Send(new CreateLoanCommand
            {
                BookId = request.BookId,
                UserId = request.UserId
            }));
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateLoan(int id, [FromBody] UpdateLoanCommand request)
        {
            return Ok(await Mediator.Send(new UpdateLoanCommand
            {
                LoanId = request.LoanId,
                UserId = request.UserId,
                isReturned = request.isReturned
            }));
        }

    }
}
