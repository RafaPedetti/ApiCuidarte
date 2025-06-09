using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.Excepciones.Usuario
{
	internal class PasswordInvalidoExcpetion : UsuarioException
	{
		public PasswordInvalidoExcpetion(string message) : base(message)
		{
		}
	}
}
