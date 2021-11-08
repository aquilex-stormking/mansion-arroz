using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MansionArroz.Net.Models
{
    public class UserTypes
    {
        public long userTypeId { get; set; }
        
        public string descripcion { get; set; }

        public bool active { get; set; }
    }
}
