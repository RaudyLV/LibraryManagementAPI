using Domain.Entities;

namespace Application.Interfaces
{
    public interface IBookService
    {
        Task AddBookAsync(Book book);
    }
}
