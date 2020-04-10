﻿using System;
using System.Collections.Generic;
using System.Text;

namespace KSquare.DMS.Domain.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public int ParentId { get; set; }
        public string ProfilePicturePath { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public int UserCategoryId { get; set; }
        public int UserRoleId { get; set; }
        public DateTime? UserActivatedDate { get; set; }
        public int? TotalLogin { get; set; }
        public DateTime? LastLogin { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public string Status { get; set; }

        public UserCategoryModel UserCategory { get; set; }
        public UserRoleModel UserRole { get; set; }
    }
}
