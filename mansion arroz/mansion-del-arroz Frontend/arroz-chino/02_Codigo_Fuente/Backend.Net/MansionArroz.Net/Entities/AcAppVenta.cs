using System;
using System.Collections.Generic;

#nullable disable

namespace MansionArroz.Net
{
    public partial class AcAppVenta
    {
        public AcAppVenta()
        {
            AcAppDetalleVenta = new HashSet<AcAppDetalleVenta>();
            AcAppDevoluciones = new HashSet<AcAppDevolucione>();
        }

        public long VentaId { get; set; }
        public long FuncionarioId { get; set; }
        public long ClienteId { get; set; }
        public string Promocion { get; set; }
        public string Descuento { get; set; }
        public bool Activo { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string UsuarioCreacion { get; set; }
        public DateTime FechaActualizacion { get; set; }
        public string UsuarioActualizacion { get; set; }

        public virtual AcAppCliente Cliente { get; set; }
        public virtual AcAppFuncionario Funcionario { get; set; }
        public virtual ICollection<AcAppDetalleVenta> AcAppDetalleVenta { get; set; }
        public virtual ICollection<AcAppDevolucione> AcAppDevoluciones { get; set; }
    }
}
