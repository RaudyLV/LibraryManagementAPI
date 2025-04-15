using Domain.Entities;

namespace Application.Interfaces
{
    public interface ILoanService 
    {
        Task<Loan> GetLoanAsync(int id);
        Task AddLoanAsync(Loan loan);
        Task UpdateLoanAsycn(Loan loan);
    }
}
