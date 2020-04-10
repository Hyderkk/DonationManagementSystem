using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace KSquare.DMS.Domain.Models.RequestModel
{
    public class AddUserRequestModel
    {
        [Required]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        [Required]
        public string Email { get; set; }
        public int ParentId { get; set; }
        public string ProfilePicturePath { get; set; }
       // public string Password { get; set; }
        public string PhoneNumber { get; set; }
        [Required]
        public int UserCategoryId { get; set; }
        [Required]
        public int UserRoleId { get; set; }

    }
}
