using Application.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Services
{
    public class LoanService : ILoanService
    {
        private readonly IRepositoryAsync<Loan> _loanRepository;
        private readonly IRepositoryAsync<User> _userRepository;
        private readonly ITransaction _transaction;
        public LoanService(IRepositoryAsync<Loan> loanRepository, IRepositoryAsync<User> userRepository,
                             ITransaction transaction)
        {
            _loanRepository = loanRepository;
            _userRepository = userRepository;
            _transaction = transaction;
        }

        public async Task<Loan> GetLoanAsync(int id)
        {
            var loan = await _loanRepository.GetByIdAsync(id);
            if(loan == null)
            {
                throw new KeyNotFoundException("The loan was not found");
            }

            return loan;
        }
        public async Task AddLoanAsync(Loan loan)
        {
            var user = await _userRepository.GetByIdAsync(loan.UserId);

            user!.AddLoan(loan);

            await _transaction.BeginTransactionAsync();

            try
            {
                await _loanRepository.AddAsync(loan);
                await _loanRepository.SaveChangesAsync();

                await _userRepository.UpdateAsync(user);
                await _userRepository.SaveChangesAsync();

                _transaction.Commit();

            }
            catch (Exception)
            {
                _transaction.RollBack();

                throw;
            }
        }


        public async Task UpdateLoanAsycn(Loan loan)
        {
            var user = await _userRepository.GetByIdAsync(loan.UserId);

            user!.RemoveLoan(loan);

            await _transaction.BeginTransactionAsync();
            try
            {
                await _loanRepository.UpdateAsync(loan);
                await _loanRepository.SaveChangesAsync();

                await _userRepository.UpdateAsync(user);
                await _userRepository.SaveChangesAsync();

                _transaction.Commit();
            }
            catch (DbUpdateException)
            {
                _transaction.RollBack();

                throw;
            }
        }
    }
}

