﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs.ResponseDTO
{
    public class LoginResponseDTO
    {
        public string Token { get; set; }

        public string Role { get; set; }
    }
}
