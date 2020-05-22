using System;
using System.Collections.Generic;

namespace TP2_ASP.NETCORE.Models
{
    public partial class Users
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
