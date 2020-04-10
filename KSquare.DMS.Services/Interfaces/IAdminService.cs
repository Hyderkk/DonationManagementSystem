using KSquare.DMS.Domain.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KSquare.DMS.Services.Interfaces
{
    public interface IAdminService
    {
        Task<List<BeneficiaryModel>> GetAllBeneficiaries();
        Task<ResponseModel<BeneficiaryModel>> AddBeneficiary(BeneficiaryModel beneficiaryModel);
        Task<ResponseModel<BeneficiaryModel>> UpdateBeneficiary(BeneficiaryModel beneficiaryModel);
        Task<ResponseModel<BeneficiaryModel>> GetBeneficiary(int beneficiaryId);
        Task<ResponseModel<ClientModel>> AddClient(ClientModel clientModel);

        Task<ResponseModel<ClientModel>> GetClient(int clientId);

        Task<List<ClientModel>> GetAllClients();

        Task<ResponseModel<ClientModel>> UpdateClient(ClientModel clientModel);

        Task<ResponseModel<string>> DeleteClient(int clientId);

        List<SelectListItem> GetCities();
    }
}
