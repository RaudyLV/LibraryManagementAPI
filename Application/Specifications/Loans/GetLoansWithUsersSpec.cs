using Application.DTOs;
using Ardalis.Specification;
using Domain.Entities;

namespace Application.Specifications.Loans
{
    public class LoanWithUserAndBookSpec : Specification<Loan>
    {
        public LoanWithUserAndBookSpec(int pageNumber, int pageSize, string? filter = null)
        {
            Query.Include(l => l.User)
                 .Include(l => l.Book);

            if (!string.IsNullOrEmpty(filter))
            {
                Query.Where(l => l.User.FirstName.Contains(filter) || l.Book.Title.Contains(filter));
            }

            Query.Skip((pageNumber - 1) * pageSize)
                 .Take(pageSize);
        }
    }

}
