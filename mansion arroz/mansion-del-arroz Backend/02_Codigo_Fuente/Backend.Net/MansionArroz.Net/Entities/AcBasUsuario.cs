using System;
using System.Collections.Generic;

#nullable disable

namespace MansionArroz.Net
{
    public partial class AcBasUsuario
    {
        public AcBasUsuario()
        {
            AcAppFuncionarios = new HashSet<AcAppFuncionario>();
            AcAppUsuariosPorRoles = new HashSet<AcAppUsuariosPorRole>();
        }

        public long UsuarioId { get; set; }
        public string Usuario { get; set; }
        public string ClaveAcceso { get; set; }
        public string CorreoElectronico { get; set; }
        public long TipoUsuarioId { get; set; }
        public bool Activo { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string UsuarioCreacion { get; set; }
        public DateTime FechaActualizacion { get; set; }
        public string UsuarioActualizacion { get; set; }

        public virtual AcBasTiposUsuario TipoUsuario { get; set; }
        public virtual ICollection<AcAppFuncionario> AcAppFuncionarios { get; set; }
        public virtual ICollection<AcAppUsuariosPorRole> AcAppUsuariosPorRoles { get; set; }
    }
}
