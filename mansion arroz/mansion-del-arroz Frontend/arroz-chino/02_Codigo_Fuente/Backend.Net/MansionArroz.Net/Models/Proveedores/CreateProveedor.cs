using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MansionArroz.Net.Models.Proveedores
{
    public class CreateProveedor
    {
        public string descripcion { get; set; }
        public string correoElectronico { get; set; }
        public string telefono { get; set; }
        
        public string usuarioAuditoria { get; set; }
        

   }
}
