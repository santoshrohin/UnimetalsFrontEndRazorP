using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UnimetalWeb.Models
{
    public class AuthResponse : CommonResponse
    {
        
        public Auth result { get; set; }
        

    }
    public class Auth
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }

        public string Username { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }

        public string Password { get; set; }
        public string Token { get; set; }

        public bool IsAdmin { get; set; }
    }
}
