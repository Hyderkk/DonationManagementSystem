using KSquare.DMS.Domain.Models;
using KSquare.DMS.Domain.Models.RequestModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KSquare.DMS.Services.Interfaces
{
    public interface IUserService
    {
        Task<List<UserCategoryModel>> GetUserCategories();
        Task<ResponseModel<UserModel>> AddUser(AddUserRequestModel userRequestModel);
        Task<ResponseModel<UserModel>> VerifyUser(LoginRequestModel loginRequestModel);
    }
}
