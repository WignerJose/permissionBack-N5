﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PermissionsN5.Domain.Entities
{
    public class PermissionType
    {
        public int Id { get; set; }
        public string Description { get; set; } = string.Empty;
    }
}
