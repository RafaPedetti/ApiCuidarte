using LogicaNegocio.Excepciones.Usuario;
using LogicaNegocio.IntefacesDominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LogicaNegocio.ValueObject.Usuario
{
	public class Password : IValidable
	{
		public string Value { get; set; }

		public Password(string password)
		{
			this.Value = password;
			Validar();
			HashPassword();
		}

		public Password()
		{
		}

		public void Validar()
		{
			ValidarLargo();
			if (!ValidarRequisitos())
			{
				throw new PasswordInvalidoExcpetion("El formato de la contraseña es invalido");
			}
		}

		public void ValidarLargo()
		{
			if (string.IsNullOrEmpty(Value) || Value.Length < 5)
			{
				throw new PasswordInvalidoExcpetion("La contraseña debe tener al menos 6 caracteres");
			}
		}

		public bool ValidarRequisitos()
		{
			string pattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[.;,!]).{6,}$";
			return Regex.IsMatch(this.Value, pattern);
		}

		public void HashPassword()
		{
			string salt = BCrypt.Net.BCrypt.GenerateSalt(12);
			Value = BCrypt.Net.BCrypt.HashPassword(this.Value);
		}

		public bool VerifyPassword( string pass)
		{
			return BCrypt.Net.BCrypt.Verify(pass, this.Value);
		}
	}
}
