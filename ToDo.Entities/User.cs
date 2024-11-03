using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo.Entities
{
    public class User : IdentityUser
    {
        public string Id { get; set; }
        public string Name { get; set; }
        
    }
}
