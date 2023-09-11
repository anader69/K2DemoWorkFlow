﻿using System;
using System.Collections.Generic;

namespace Framework.Identity.Data.Dtos
{
    public class ClaimDto
    {
        public List<string> UserRole { get; set; }
        public Guid UserId { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string JobTitle { get; set; }
        public string Departement { get; set; }
    }
}
