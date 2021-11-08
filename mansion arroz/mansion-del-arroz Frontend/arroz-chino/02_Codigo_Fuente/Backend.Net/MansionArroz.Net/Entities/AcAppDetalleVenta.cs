using System;
using System.Collections.Generic;

#nullable disable

namespace MansionArroz.Net
{
    public partial class AcAppDetalleVenta
    {
        public long DetalleVentaId { get; set; }
        public long VentaId { get; set; }
        public long ProductoId { get; set; }
        public string ValorUnitarioImpuesto { get; set; }
        public string Cantidad { get; set; }
        public string ValorUnitario { get; set; }
        public bool Activo { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string UsuarioCreacion { get; set; }
        public DateTime FechaActualizacion { get; set; }
        public string UsuarioActualizacion { get; set; }

        public virtual AcAppProducto Producto { get; set; }
        public virtual AcAppVenta Venta { get; set; }
    }
}
