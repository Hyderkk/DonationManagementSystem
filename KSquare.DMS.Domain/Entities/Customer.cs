﻿using System;
using System.Collections.Generic;

namespace KSquare.DMS.Domain.Entities
{
    public partial class Customer
    {
        public Customer()
        {
            Consumer = new HashSet<Consumer>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string PhoneNumber2 { get; set; }
        public string Type { get; set; }
        public string Notes { get; set; }
        public int ClientId { get; set; }
        public bool? IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }

        public virtual Client Client { get; set; }
        public virtual ICollection<Consumer> Consumer { get; set; }
    }
}
