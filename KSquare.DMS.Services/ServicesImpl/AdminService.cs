using AutoMapper;
using KSquare.DMS.Domain.Entities;
using KSquare.DMS.Domain.Models;
using KSquare.DMS.Repositories.Interfaces;
using KSquare.DMS.Services.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;

namespace KSquare.DMS.Services.ServicesImpl
{
    public class AdminService : IAdminService
    {
        private readonly IMapper _autoMapper;
        private readonly IConfiguration _configuration;
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;
        private readonly ITokenService _tokenService;

        public AdminService(IUnitOfWorkFactory unitOfWorkFactory,
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

        public async Task<List<BeneficiaryModel>> GetAllBeneficiaries()
        {
            using (var unitOfWork = _unitOfWorkFactory.CreateUnitOfWork())
            {
                var response = new List<BeneficiaryModel>();

                var beneficiaries = await unitOfWork.AdminRepository.GetAllBeneficiaries();

                if (beneficiaries == null)
                {
                    response = null;
                }
                else
                {
                    response = _autoMapper.Map<List<BeneficiaryModel>>(beneficiaries);
                }

                return response;
            }
        }

        public async Task<ResponseModel<BeneficiaryModel>> AddBeneficiary(BeneficiaryModel beneficiaryModel)
        {
            using (var unitOfWork = _unitOfWorkFactory.CreateUnitOfWork())
            {
                var response = new ResponseModel<BeneficiaryModel>();

                var BeneficiaryExists = await unitOfWork.AdminRepository.GetBeneficiaryByCNIC(beneficiaryModel.Cnic);

                if (BeneficiaryExists != null)
                {
                    response.IsError = true;
                    response.Message = "Beneficiary already exists";
                }
                else
                {
                    var Beneficiary = _autoMapper.Map<Beneficiary>(beneficiaryModel);

                    Beneficiary.IsActive = true;
                    Beneficiary.CreatedBy = "Test"; //await _tokenService.GetClaimFromToken(JwtRegisteredClaimNames.Sub);
                    Beneficiary.CreatedDate = DateTime.Now;
                    Beneficiary.UpdatedBy = "Test";  //await _tokenService.GetClaimFromToken(JwtRegisteredClaimNames.Sub);
                    Beneficiary.UpdatedDate = DateTime.Now;

                    var addedBeneficiary = await unitOfWork.AdminRepository.AddBeneficiary(Beneficiary);
                    if (addedBeneficiary != null)
                    {
                        await unitOfWork.SaveChangesAsync();
                        response.Message = "Beneficiary added successfuly";
                    }
                    else
                    {
                        response.IsError = true;
                        response.Message = "Unable to add Beneficiary";
                    }
                }
                return response;
            }
        }

        public async Task<ResponseModel<BeneficiaryModel>> UpdateBeneficiary(BeneficiaryModel beneficiaryModel)
        {
            using (var unitOfWork = _unitOfWorkFactory.CreateUnitOfWork())
            {
                var response = new ResponseModel<BeneficiaryModel>();

                var beneficiary = await unitOfWork.AdminRepository.GetBeneficiary(beneficiaryModel.Id);

                if (beneficiary == null)
                {
                    response.IsError = true;
                    response.Message = "Beneficiary does not exists";
                }
                else
                {
                    beneficiaryModel.CreatedDate = beneficiary.CreatedDate;
                    beneficiaryModel.CreatedBy = beneficiary.CreatedBy;
                    beneficiaryModel.UpdatedBy = await _tokenService.GetClaimFromToken(JwtRegisteredClaimNames.Sub);
                    beneficiaryModel.UpdatedDate = DateTime.Now;

                    _autoMapper.Map(beneficiaryModel, beneficiary);

                    if (await unitOfWork.SaveChangesAsync())
                    {
                        response.Message = "Beneficiary updated successfully.";
                        response.Model = beneficiaryModel;
                    }
                    else
                    {
                        response.IsError = true;
                        response.Message = "Unable to update Beneficiary";
                    }
                }
                return response;
            }
        }

        public async Task<ResponseModel<BeneficiaryModel>> GetBeneficiary(int beneficiaryId)
        {
            using (var unitOfWork = _unitOfWorkFactory.CreateUnitOfWork())
            {
                var response = new ResponseModel<BeneficiaryModel>();

                var beneficiary = await unitOfWork.AdminRepository.GetBeneficiary(beneficiaryId);

                if (beneficiary == null)
                {
                    response.IsError = true;
                    response.Message = "Beneficiary does not exists";
                }
                else
                {
                    response.Model = _autoMapper.Map<BeneficiaryModel>(beneficiary);
                }

                return response;
            }
        }



        public async Task<ResponseModel<ClientModel>> AddClient(ClientModel clientModel)
        {
            using (var unitOfWork = _unitOfWorkFactory.CreateUnitOfWork())
            {
                var response = new ResponseModel<ClientModel>();

                var clientExists = await unitOfWork.AdminRepository.GetClientByName(clientModel.Name);

                if (clientExists != null)
                {
                    response.IsError = true;
                    response.Message = "Client already exists";
                }
                else
                {
                    var client = _autoMapper.Map<Client>(clientModel);

                    client.IsActive = true;
                    client.CreatedBy = await _tokenService.GetClaimFromToken(JwtRegisteredClaimNames.Sub);
                    client.CreatedDate = DateTime.Now;
                    client.UpdatedBy = await _tokenService.GetClaimFromToken(JwtRegisteredClaimNames.Sub);
                    client.UpdatedDate = DateTime.Now;

                    var addedClient = await unitOfWork.AdminRepository.AddClient(client);
                    if (addedClient != null)
                    {
                        await unitOfWork.SaveChangesAsync();
                        response.Message = "Client added successfuly";
                    }
                    else
                    {
                        response.IsError = true;
                        response.Message = "Unable to add Client";
                    }
                }
                return response;
            }
        }

        public async Task<ResponseModel<ClientModel>> UpdateClient(ClientModel clientModel)
        {
            using (var unitOfWork = _unitOfWorkFactory.CreateUnitOfWork())
            {
                var response = new ResponseModel<ClientModel>();

                var client = await unitOfWork.AdminRepository.GetClient(clientModel.Id);

                if (client == null)
                {
                    response.IsError = true;
                    response.Message = "Client does not exists";
                }
                else
                {
                    clientModel.CreatedDate = client.CreatedDate;
                    clientModel.CreatedBy = client.CreatedBy;
                    clientModel.UpdatedBy = await _tokenService.GetClaimFromToken(JwtRegisteredClaimNames.Sub);
                    clientModel.UpdatedDate = DateTime.Now;

                    _autoMapper.Map(clientModel, client);

                    //   client.UpdatedBy = await _tokenService.GetClaimFromToken(JwtRegisteredClaimNames.Sub);
                    //   client.UpdatedDate = DateTime.Now;

                    if (await unitOfWork.SaveChangesAsync())
                    {
                        response.Message = "Client updated successfully.";
                        response.Model = clientModel;
                    }
                    else
                    {
                        response.IsError = true;
                        response.Message = "Unable to update Client";
                    }
                }
                return response;
            }
        }

        public async Task<List<ClientModel>> GetAllClients()
        {
            using (var unitOfWork = _unitOfWorkFactory.CreateUnitOfWork())
            {
                var response = new List<ClientModel>();

                var clients = await unitOfWork.AdminRepository.GetAllClients();

                if (clients == null)
                {
                    response = null;
                }
                else
                {
                    response = _autoMapper.Map<List<ClientModel>>(clients);
                }

                return response;
            }
        }

        public async Task<ResponseModel<ClientModel>> GetClient(int clientId)
        {
            using (var unitOfWork = _unitOfWorkFactory.CreateUnitOfWork())
            {
                var response = new ResponseModel<ClientModel>();

                var client = await unitOfWork.AdminRepository.GetClient(clientId);

                if (client == null)
                {
                    response.IsError = true;
                    response.Message = "Client does not exists";
                }
                else
                {
                    response.Model = _autoMapper.Map<ClientModel>(client);
                }

                return response;
            }
        }

        public async Task<ResponseModel<string>> DeleteClient(int clientId)
        {
            using (var unitOfWork = _unitOfWorkFactory.CreateUnitOfWork())
            {
                var response = new ResponseModel<string>();

                var client = await unitOfWork.AdminRepository.GetClient(clientId);

                if (client == null)
                {
                    response.IsError = true;
                    response.Message = "Client does not exists";
                }
                else
                {
                    client.IsActive = false;
                    client.UpdatedBy = await _tokenService.GetClaimFromToken(JwtRegisteredClaimNames.Sub);
                    client.UpdatedDate = DateTime.Now;

                    if (await unitOfWork.SaveChangesAsync())
                    {
                        response.Message = "Client deleted successfully.";
                    }
                    else
                    {
                        response.IsError = true;
                        response.Message = "Unable to delete Client";
                    }
                }
                return response;
            }
        }


        public List<SelectListItem> GetCities()
        {
            List<SelectListItem> regions = new List<SelectListItem>()
            {
                new SelectListItem
                {
                    Value = "1",
                    Text = "Tando Allahyar",
                    Selected = true
                },
                new SelectListItem
                {
                    Value = "2",
                    Text = "karachi"
                },
                new SelectListItem
                {
                    Value = "3",
                    Text = "Hyderabad"
                }
            };
            return regions;
        }
    }
}
