using LogicaNegocio.Excepciones.Usuario;
using LogicaNegocio.IntefacesDominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LogicaNegocio.ValueObject
{
	public class Email : IValidable
	{
		public string Value { get; private set; }

		public Email(string e) 
		{
			this.Value = e;
		}

		public Email() { }


		public void Validar()
		{
			if (string.IsNullOrWhiteSpace(Value))
			{
				throw new UsuarioException("El correo electrónico no puede estar vacío o ser nulo.");
			}

			// Expresión regular para validar el formato de correo electrónico
			string pattern = @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$";
			Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);

			if(!regex.IsMatch(Value)) throw new UsuarioException("El correo electrónico no es valido.");
		}
	}
}
