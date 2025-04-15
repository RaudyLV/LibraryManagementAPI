using Application.Interfaces;
using Domain.Entities;

namespace Persistence.Services
{
    public class BookService : IBookService
    {
        private readonly IRepositoryAsync<Book> _bookRepository;

        public BookService(IRepositoryAsync<Book> bookRepository)
        {
            _bookRepository = bookRepository;
        }
        public async Task AddBookAsync(Book newBook)
        {

            await _bookRepository.AddAsync(newBook);
            await _bookRepository.SaveChangesAsync();
        }

    }
}
