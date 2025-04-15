using Ardalis.Specification;
using Domain.Entities;

namespace Application.Specifications.Books
{
    public class GetBooksSpec : Specification<Book>
    {
        public GetBooksSpec(int pageNumber, int pageSize, string? filter = null)
        {
            if(!string.IsNullOrEmpty(filter))
            {
                Query.Where(b => b.Title.Contains(filter) || b.Author.Contains(filter));
            }

            Query.Skip((pageNumber - 1) * pageSize)
                 .Take(pageSize);
        }
    }
}
