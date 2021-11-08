using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MansionArroz.Net.Models.Users
{
    public class User
    {
        public long userId { get; set; }
        public long funcionarioId { get; set; }
        public string numeroIdentificacion { get; set; }
        public string usuario { get; set; }
        public string claveAcceso { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string telefono { get; set; }
        public string direccion { get; set; }
        public string correoElectronico { get; set; }
        public long tipoUsuarioId { get; set; }
        public string tipoUsuarioDescripcion { get; set; }
        public bool activo { get; set; }
    }
}
