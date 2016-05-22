using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RESTTest
{
    public class Usuario
    {
        public DTOHeader DTOHeader { get; set; }
        public DTOUsuario DTOUsuario { get; set; }
    }

    public class DTOUsuario
    {
        public string codUsuario { get; set; }
        public string nombres { get; set; }
    }

    public class DTOHeader
    {
        public string CodigoRetorno { get; set; }
        public string DescRetorno { get; set; }
    }
}
