using AutoMapper;
using KSquare.DMS.Common;
using KSquare.DMS.Domain.Entities;
using KSquare.DMS.Domain.Models;
using KSquare.DMS.Domain.Models.RequestModel;
using KSquare.DMS.Repositories.Interfaces;
using KSquare.DMS.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;

namespace KSquare.DMS.Services.ServicesImpl
{
    public class UserService : IUserService
    {
        private readonly IMapper _autoMapper;
        private readonly IConfiguration _configuration;
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;
        private readonly ITokenService _tokenService;

        public UserService(IUnitOfWorkFactory unitOfWorkFactory,
              IMapper autoMapper,
              IConfiguration configuration,
              ITokenService tokenService
            )
        {
            _autoMapper = autoMapper;
            _unitOfWorkFactory = unitOfWorkFactory;
            _configuration = configuration;
            _tokenService = tokenService;
        }

        public async Task<ResponseModel<UserModel>> AddUser(AddUserRequestModel userRequestModel)
        {
            using (var unitOfWork = _unitOfWorkFactory.CreateUnitOfWork())
            {
                var response = new ResponseModel<UserModel>();

                if (await unitOfWork.UserRepository.UserExist(userRequestModel.UserName, userRequestModel.Email))
                {
                    response.IsError = true;
                    response.Message = "User already exists";
                }
                else
                {
                    var user = _autoMapper.Map<User>(userRequestModel);

                    user.UserCategoryId = (int)UserCategoryEnum.SuperAdmin;
                    user.TotalLogin = 0;
                    user.IsActive = true;
                    user.Status = "Activated";
                    user.Password = Utilities.MD5Hash("helloworld" + _configuration.GetSection("RandomPasswordSeed").Value);
                    user.CreatedBy = await _tokenService.GetClaimFromToken(JwtRegisteredClaimNames.Sub);
                    user.CreatedDate = DateTime.Now;
                    user.UpdatedBy = await _tokenService.GetClaimFromToken(JwtRegisteredClaimNames.Sub);
                    user.UpdatedDate = DateTime.Now;
                    user.Email = user.Email.ToLower();


                    var addedUser = await unitOfWork.UserRepository.AddUser(user);
                    if (addedUser != null)
                    {
                        //Got to do this to get user id generated
                        await unitOfWork.SaveChangesAsync();
                        //   await _notificationService.SendAccountActivationEmail(addedUser);
                        // response.Model = _autoMapper.Map<UserModel>(addedUser);

                        response.Message = "User added successfuly";
                    }
                    else
                    {
                        response.IsError = true;
                        response.Message = "Unable to add user";
                    }

                }
                return response;
            }
        }


        public async Task<ResponseModel<UserModel>> VerifyUser(LoginRequestModel loginRequestModel)
        {
            string passwordHash = Utilities.MD5Hash(loginRequestModel.Password + _configuration.GetSection("RandomPasswordSeed").Value);
            var response = new ResponseModel<UserModel>();
            using (var unitOfWork = _unitOfWorkFactory.CreateUnitOfWork())
            {
                var userEntity = await unitOfWork.UserRepository.VerifyUserCredentials(loginRequestModel.UserName, passwordHash);

                if (userEntity == null)
                {
                    response.IsError = true;
                    response.Message = "Invalid credentials";
                }
                else
                {
                    userEntity.LastLogin = DateTime.Now;
                    userEntity.TotalLogin += 1;
                    await unitOfWork.SaveChangesAsync();

                    response.Model = _autoMapper.Map<UserModel>(userEntity);
                }

                return response;
            }
        }
        public async Task<List<UserCategoryModel>> GetUserCategories()
        {
            using (var unitOfWork = _unitOfWorkFactory.CreateUnitOfWork())
            {
                var response = new List<UserCategoryModel>();
                var customerCategories = await unitOfWork.UserRepository.GetUserCategories();
                response = _autoMapper.Map<List<UserCategoryModel>>(customerCategories);
                return response;
            }
        }
    }
}
