using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MansionArroz.Net.Models.Clientes
{
    public class UpdateCliente
    {
        public long clienteId { get; set; }
        public string usuarioAuditoria { get; set; }
        public string direccion { get; set; }
        public string telefono { get; set; }
        public string apellido { get; set; }
        public string nombre { get; set; }
        public string numeroIdentificacion { get; set; }
        public bool activo { get; set; }
    }
}
