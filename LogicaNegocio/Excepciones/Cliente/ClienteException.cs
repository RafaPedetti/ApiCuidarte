using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.Excepciones.Cliente
{
	public class ClienteException : DomainException
	{
		public ClienteException(string message) : base(message)
		{
		}
	}
}
