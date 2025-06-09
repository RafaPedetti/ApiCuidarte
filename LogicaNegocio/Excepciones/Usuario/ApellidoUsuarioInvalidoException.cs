using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.Excepciones.Usuario
{
    public class ApellidoUsuarioInvalidoException : UsuarioException
    {
        public ApellidoUsuarioInvalidoException(string msj) : base(msj) { }
    }
}
