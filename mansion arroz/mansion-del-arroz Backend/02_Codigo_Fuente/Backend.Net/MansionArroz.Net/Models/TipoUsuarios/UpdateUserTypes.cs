using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MansionArroz.Net.Models
{
    public class UpdateUserTypes
    {
        public long userTypeId { get; set; }

        public string descripcion { get; set; }

        public bool active { get; set; }

        public string usuarioAuditoria { get; set; }
    }
}
