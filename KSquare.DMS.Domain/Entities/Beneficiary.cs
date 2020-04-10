using System;
using System.Collections.Generic;

namespace KSquare.DMS.Domain.Entities
{
    public partial class Beneficiary
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Town { get; set; }
        public string Cnic { get; set; }
        public string FamilyCode { get; set; }
        public string Type { get; set; }
        public string Notes { get; set; }
        public bool? IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
