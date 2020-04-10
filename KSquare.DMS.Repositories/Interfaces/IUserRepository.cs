using KSquare.DMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KSquare.DMS.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetUser(int Id);
        Task<User> AddUser(User userEntity);
        Task<bool> UserExist(string userName, string email);
        Task<User> VerifyUserCredentials(string userName, string passwordHash);

        Task<List<UserCategory>> GetUserCategories();


    }
}
