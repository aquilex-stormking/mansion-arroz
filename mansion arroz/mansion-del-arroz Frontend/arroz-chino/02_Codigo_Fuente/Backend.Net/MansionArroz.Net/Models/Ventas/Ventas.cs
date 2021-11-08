using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MansionArroz.Net.Models.Ventas
{
    public class Ventas
    {
        public long  ventaId { get; set; }
        public long funcionarioId { get; set; }
        public string funcionarioNombreCompleto { get; set; }
        public string funcionarioNumeroIdentificacion { get; set; }
        public long clienteId { get; set; }
        public string clienteNumeroIdentificacion { get; set; }
        public string clienteNombreCompleto { get; set; }
        public string promocion { get; set; }
        public string descuento { get; set; }
        public List<DetalleVenta> detalleVentas { get; set; }
        public int total { get; set; }
        public int totalVenta { get; set; }
    }
}
