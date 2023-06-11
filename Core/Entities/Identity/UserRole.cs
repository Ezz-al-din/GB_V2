using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities.Identity
{
    public class UserRole
    {
        public string UserId { get; set; }
        public string RoleId { get; set; }
        public string UserName { get; set; }
    }
}