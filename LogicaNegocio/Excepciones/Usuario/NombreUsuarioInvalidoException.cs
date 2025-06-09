using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.Excepciones.Usuario
{
    public class NombreUsuarioInvalidoException : UsuarioException
    {
        public NombreUsuarioInvalidoException(string msj): base(msj){ }
    }
}
