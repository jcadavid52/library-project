﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Layer.DTOs
{
    public class UserDto
    {
        public string Id { get; set; }
        public string PhoneNumber { get; set; }
        public string LastName { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Adress { get; set; }
        public string Institution { get; set; }

    }
}
