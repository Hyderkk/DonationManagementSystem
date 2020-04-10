using KSquare.DMS.Domain.Entities;
using KSquare.DMS.Domain.Models;
using KSquare.DMS.Repositories.Context;
using KSquare.DMS.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KSquare.DMS.Repositories.RepositoriesImpl
{
    public class UserRepository : IUserRepository
    {
        private readonly KSquareContext _context;

        internal UserRepository(KSquareContext context)
        {
            _context = context;
        }

        #region User

        public async Task<User> GetUser(int Id)
        {
            var user = await _context.Set<User>().FirstOrDefaultAsync(x => x.Id == Id && x.IsActive == true);
            return user;
        }

        public async Task<User> AddUser(User userEntity)
        {
            var user = _context.User.Add(userEntity);
            return await Task.FromResult(user.Entity);
        }

        public async Task<bool> UserExist(string userName, string email)
        {
            return await _context.Set<User>().AnyAsync(x => (x.UserName == userName && x.Email == email));
        }

        public async Task<User> VerifyUserCredentials(string userName, string passwordHash)
        {
            var entity = await _context.Set<User>().FirstOrDefaultAsync(x => x.IsActive == true && (x.UserName == userName || x.Email == userName) && x.Password.ToUpper() == passwordHash);
            return entity;
        }
        #endregion

        public async Task<List<UserCategory>> GetUserCategories()
        {
            var userCategories = await _context.Set<UserCategory>().Where(x => x.IsActive == true).ToListAsync();
            return userCategories;
        }

    }
}
