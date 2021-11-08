using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MansionArroz.Net.Models.Roles
{
    public class UserByRole
    {
        public long roleId { get; set; }

        public string role_Descripcion { get; set; }
        
        public long userId { get; set; }

        public string user { get; set; }

        public bool activo { get; set; }
    }
}
