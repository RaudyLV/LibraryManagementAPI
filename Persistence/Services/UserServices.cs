using Application.Interfaces;
using Domain.Entities;
using identity.Models;
using Microsoft.AspNetCore.Identity;

namespace Persistence.Services
{
    public class UserServices : IUserService
    {
        private readonly IRepositoryAsync<User> _userRepository;
        private readonly ITransaction _transaction;
        private readonly UserManager<AppUser> _userManager;
        public UserServices(IRepositoryAsync<User> userRepository, ITransaction transaction, 
                            UserManager<AppUser> userManager)
        {
            _userRepository = userRepository;
            _transaction = transaction;
            _userManager = userManager;
        }


        public Task<List<User>> GetAllUsers()
        {
            throw new NotImplementedException();
        }

        public async Task<User> GetByIdAsync(string id)
        {
            var user = await _userRepository.GetByIdAsync(id);

            return user!;
        }
        public async Task AddUserAsync(User user)
        {
            await _userRepository.AddAsync(user);
            await _userRepository.SaveChangesAsync();
        }

        public async Task DeleteUserAsync(string id)
        {
            var identiuser = await _userManager.FindByIdAsync(id);
            var domainUser = await _userRepository.GetByIdAsync(id);

            await _transaction.BeginTransactionAsync();
            try
            {
                await _userManager.DeleteAsync(identiuser!);

                await _userRepository.DeleteAsync(domainUser!);
                await _userRepository.SaveChangesAsync();

                _transaction.Commit();
            }
            catch (Exception)
            {
                _transaction.RollBack();
                throw;
            }
        }
    }
}
