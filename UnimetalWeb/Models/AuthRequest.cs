using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace UnimetalWeb.Models
{
    public class AuthRequest
    {
        [Required(ErrorMessage ="User Id Is Required")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Password Is Required")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Financial Year Id Is Required")]
        public int FinancialYearId { get; set; }
    }
}
