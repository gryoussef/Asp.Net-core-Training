﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ASP.NETCORE_TP.Models
{
    public class User
    {
      
        public int Id { get; set; }
     
        public string nom { get; set; }
     
        public string prenom { get; set; }
        [Required]
 
        [DataType(DataType.EmailAddress)]
        public string email { get; set; }
    }
}
