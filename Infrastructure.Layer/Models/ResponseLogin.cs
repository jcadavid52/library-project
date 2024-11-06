using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Layer.Models
{
    public class ResponseLogin
    {
        public string UserName { get; set; }
        public string Rol { get; set; }
        public bool Success { get; set; }
        public string IdUser { get; set; }

    }
}
