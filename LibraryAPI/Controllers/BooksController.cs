using Application.Features.Books.Commands.CreateBookCommands;
using Application.Features.Books.Commands.DeleteBookCommands;
using Application.Features.Books.Commands.UpdateBookCommands;
using Application.Features.Books.Querys.GetBookById;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : BaseApiController
    {

        [HttpGet()]
        public async Task<IActionResult> GetAllBooks([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10, [FromQuery] string? filter = null)
        {
            return Ok(await Mediator.Send(new GetAllBooksQuery(pageNumber, pageSize, filter)));
        }

        [HttpPost]
        [Authorize(Roles = "Basic")]
        public async Task<IActionResult> CreateBook([FromBody]CreateBookCommand request)
        {
            return Ok(await Mediator.Send(request));
        }

        [HttpPut("{Id}")]
        [Authorize(Roles = "Basic")]
        public async Task<IActionResult> UpdateBook(int id,[FromBody] UpdateBookCommand request)
        {
            if(id != request.Id)
            {
                return BadRequest();
            }

            return Ok(await Mediator.Send(request));
        }

        [HttpDelete("{Id}")]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> DeleteBook(int id)
        {
            return Ok(await Mediator.Send(new DeleteBookCommand { Id = id}));
        }
    }
}
