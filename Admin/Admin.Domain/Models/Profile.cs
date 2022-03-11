﻿using System.Collections.Generic;

namespace Admin.Domain.Models
{
    public class Profile
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string EmpId { get; set; }
        public string Mobile { get; set; }
        public List<Skill> Skills { get; set; }
    }
}
