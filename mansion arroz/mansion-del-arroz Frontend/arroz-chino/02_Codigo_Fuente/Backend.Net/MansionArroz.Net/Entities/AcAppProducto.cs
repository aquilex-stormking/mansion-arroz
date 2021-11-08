using System;
using System.Collections.Generic;

#nullable disable

namespace MansionArroz.Net
{
    public partial class AcAppProducto
    {
        public AcAppProducto()
        {
            AcAppDetalleVenta = new HashSet<AcAppDetalleVenta>();
            AcAppInventarios = new HashSet<AcAppInventario>();
        }

        public long ProductoId { get; set; }
        public long MarcaId { get; set; }
        public long ProveedorId { get; set; }
        public long CategoriaId { get; set; }
        public string Descripcion { get; set; }
        public string Observaciones { get; set; }
        public string Valor { get; set; }
        public string ValorImpuesto { get; set; }
        public bool Activo { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string UsuarioCreacion { get; set; }
        public DateTime FechaActualizacion { get; set; }
        public string UsuarioActualizacion { get; set; }

        public virtual AcAppCategoria Categoria { get; set; }
        public virtual AcAppMarca Marca { get; set; }
        public virtual AcAppProveedore Proveedor { get; set; }
        public virtual ICollection<AcAppDetalleVenta> AcAppDetalleVenta { get; set; }
        public virtual ICollection<AcAppInventario> AcAppInventarios { get; set; }
    }
}
