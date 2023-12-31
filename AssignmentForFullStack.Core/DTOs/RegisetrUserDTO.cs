﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssignmentForFullStack.Core.DTOs
{
    public class RegisetrUserDTO
    {
        [Required]
        public string UserName { get; set; }
        [Required, DataType(DataType.Password)]
        public string Password { get; set; }

        [Required,DataType(DataType.Password), Compare("Password")]
        public string ConfirmPassword { get; set; }
        [Required]
        public string Email { get; set; }

    }
}
