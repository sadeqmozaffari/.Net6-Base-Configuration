using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Entities.Identity
{
    public class RoleClaim : IdentityRoleClaim<int>
    {
      
        public virtual Role Role { get; set; }
    }
}
