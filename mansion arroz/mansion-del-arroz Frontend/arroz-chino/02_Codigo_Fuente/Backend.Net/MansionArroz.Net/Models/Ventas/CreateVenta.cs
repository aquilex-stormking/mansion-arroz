using MansionArroz.Net.Models.Productos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MansionArroz.Net.Models
{
    public class CreateVenta
    {
        public long funcionarioId { get; set; }
        public long clienteId { get; set; }
        public string promocion { get; set; }
        public string descuento { get; set; }
        public List<ProductoVenta> productoVenta { get; set; }
        public bool activo { get; set; }
        public string usuarioAuditoria { get; set; }
    }
}
