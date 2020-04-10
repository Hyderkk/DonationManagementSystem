using System;
using System.Collections.Generic;

namespace KSquare.DMS.Domain.Entities
{
    public partial class Consumer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string DeviceImei { get; set; }
        public int CustomerId { get; set; }
        public bool? IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }

        public virtual Customer Customer { get; set; }
    }
}
