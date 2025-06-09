using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.Excepciones.Cliente
{
	public class TelefonoInvalidoException : ClienteException
	{
		public TelefonoInvalidoException(string message) : base(message)
		{
		}
	}
}
