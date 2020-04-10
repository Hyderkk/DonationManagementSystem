using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace KSquare.DMS.Domain.Models
{
    public class TokenModel
    {
        [Required]
        public string AccessToken { get; set; }
        public int AccessTokenExpiry { get; set; }
        [Required]
        public string RefreshToken { get; set; }
    }
}
