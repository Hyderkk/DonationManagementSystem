using System;
using System.Collections.Generic;

namespace KSquare.DMS.Domain.Entities
{
    public partial class Donation
    {
        public int Id { get; set; }
        public int? BeneficiaryId { get; set; }
        public int? VolunteerId { get; set; }
        public string Type { get; set; }
        public int? Amount { get; set; }
        public int? RationPacket { get; set; }
        public string BeneficiaryCnic { get; set; }
        public string Description { get; set; }
        public bool? IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
