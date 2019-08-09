using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FaturaTakip.Models
{
    public class MembershipUserInRole
    {
        public string UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
    }
}