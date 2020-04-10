using KSquare.DMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KSquare.DMS.Repositories.Interfaces
{
    public interface IAdminRepository
    {
        Task<Client> AddClient(Client clientEntity);
        Task<Client> GetClient(int clientId);
        Task<List<Client>> GetAllClients();
        Task<Client> GetClientByName(string clientName);

        Task<Beneficiary> AddBeneficiary(Beneficiary beneficiaryEntity);
        Task<Beneficiary> GetBeneficiary(int beneficiaryId);
        Task<List<Beneficiary>> GetAllBeneficiaries();
        Task<Beneficiary> GetBeneficiaryByName(string beneficiaryName);
        Task<Beneficiary> GetBeneficiaryByCNIC(string beneficiaryCNIC);
        Task<List<Beneficiary>> GetBeneficiary(string beneficiaryName, string beneficiaryCNIC);

        Task<Volunteer> AddVolunteer(Volunteer VolunteerEntity);
        Task<Volunteer> GetVolunteer(int VolunteerId);
        Task<List<Volunteer>> GetAllVolunteers();
        Task<Volunteer> GetVolunteerByName(string VolunteerName);
    }
}
