using Application.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;

namespace Persistence.Contexts
{
    public class DbContextTransactionWrapper : ITransaction
    {
        private readonly AppDbContext _appDbContext;
        private  IDbContextTransaction? _dbTransaction;

        public DbContextTransactionWrapper(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task BeginTransactionAsync()
        {
            _dbTransaction = await _appDbContext.Database.BeginTransactionAsync();
        }

        public void Commit()
        {
            _dbTransaction?.Commit();
        }

        public void RollBack()
        {
            _dbTransaction?.Rollback();
        }
        public void Dispose()
        {
            _dbTransaction?.Dispose();
        }

    }
}
