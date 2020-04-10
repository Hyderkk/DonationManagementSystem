using KSquare.DMS.Domain.Entities;
using KSquare.DMS.Repositories.Context;
using KSquare.DMS.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KSquare.DMS.Repositories.RepositoriesImpl
{
    public class AdminRepository : IAdminRepository
    {
        private readonly KSquareContext _context;

        internal AdminRepository(KSquareContext context)
        {
            _context = context;
        }

        public async Task<Beneficiary> AddBeneficiary(Beneficiary beneficiaryEntity)
        {
            var beneficiary = _context.Beneficiary.Add(beneficiaryEntity);
            return await Task.FromResult(beneficiary.Entity);
        }

        public async Task<List<Beneficiary>> GetAllBeneficiaries()
        {
            var beneficiaries = await _context.Set<Beneficiary>().ToListAsync();
            return beneficiaries;
        }

        public async Task<Beneficiary> GetBeneficiary(int beneficiaryId)
        {
            var beneficiary = await _context.Set<Beneficiary>().FirstOrDefaultAsync(x => x.Id == beneficiaryId);
            return beneficiary;
        }

        public async Task<List<Beneficiary>> GetBeneficiary(string beneficiaryName, string beneficiaryCNIC)
        {
            var beneficiaries = await _context.Set<Beneficiary>().Where(x => (string.IsNullOrEmpty(beneficiaryCNIC) || x.Cnic == beneficiaryCNIC) && (string.IsNullOrEmpty(beneficiaryName) || x.FirstName.Contains(beneficiaryName) || x.LastName.Contains(beneficiaryName))).ToListAsync();
            return beneficiaries;
        }

        public async Task<Beneficiary> GetBeneficiaryByCNIC(string beneficiaryCNIC)
        {
            var beneficiary = await _context.Set<Beneficiary>().FirstOrDefaultAsync(x => x.Cnic == beneficiaryCNIC);
            return beneficiary;
        }

        public Task<Beneficiary> GetBeneficiaryByName(string beneficiaryName)
        {
            throw new NotImplementedException();
        }

        public Task<Volunteer> AddVolunteer(Volunteer VolunteerEntity)
        {
            throw new NotImplementedException();
        }

        public Task<List<Volunteer>> GetAllVolunteers()
        {
            throw new NotImplementedException();
        }

        public Task<Volunteer> GetVolunteer(int VolunteerId)
        {
            throw new NotImplementedException();
        }

        public Task<Volunteer> GetVolunteerByName(string VolunteerName)
        {
            throw new NotImplementedException();
        }

        public async Task<Client> AddClient(Client clientEntity)
        {
            throw new NotImplementedException();
            //var client = _context.Client.Add(clientEntity);
            //return await Task.FromResult(client.Entity);
        }

        public async Task<List<Client>> GetAllClients()
        {
            var clients = await _context.Set<Client>().ToListAsync();
            return clients;
        }

        public async Task<Client> GetClient(int clientId)
        {
            var client = await _context.Set<Client>().FirstOrDefaultAsync(x => x.Id == clientId && x.IsActive == true);
            return client;
        }

        public async Task<Client> GetClientByName(string clientName)
        {
            var client = await _context.Set<Client>().FirstOrDefaultAsync(x => x.Name == clientName);
            return client;
        }
    }
}
