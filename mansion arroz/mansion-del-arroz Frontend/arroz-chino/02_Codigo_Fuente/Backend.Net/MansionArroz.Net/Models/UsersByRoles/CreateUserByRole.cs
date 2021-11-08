using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MansionArroz.Net.Models.Roles
{
    public class CreateUserByRole
    {
        public long userId { get; set; }
        
        public long roleId { get; set; }

        public string usuarioAuditoria { get; set; }
    }
}
