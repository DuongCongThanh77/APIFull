﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Model
{
    public class User
    {
        [Key]
        public Guid UserID { get; set; }

        public string UserName { get; set; }
        public string Password { get; set; }

    }
}
