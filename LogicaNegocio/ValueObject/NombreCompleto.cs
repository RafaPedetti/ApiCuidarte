using LogicaNegocio.Excepciones.Usuario;
using LogicaNegocio.IntefacesDominio;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace LogicaNegocio.ValueObject
{
	public class NombreCompleto : IValidable
	{
		[NotMapped]
		public string Nombre { get; set; }

		[NotMapped]
		public string Apellido { get; set; }


		public NombreCompleto(string n, string a) 
		{
			this.Nombre = n;
			this.Apellido = a;
			Validar();
		}

		public NombreCompleto() { }

		public void Validar()
		{
			ValidarNombre();
			ValidarApellido();
		}
		public void ValidarApellido()
		{
			if (string.IsNullOrEmpty(Nombre))
			{
				throw new ApellidoUsuarioInvalidoException("El nombre no debe estar vacio ");
			}
			if(Nombre.Length < 2)
			{
				throw new ApellidoUsuarioInvalidoException("El nombre debe tener al menos 2 caracteres");
			}
		}
		public void ValidarNombre()
		{
			if (string.IsNullOrEmpty(Apellido))
			{
				throw new ApellidoUsuarioInvalidoException("El apellido no debe estar vacio ");
			}
			if (Apellido.Length < 2)
			{
				throw new ApellidoUsuarioInvalidoException("El apellido debe tener al menos 2 caracteres");
			}
		}

		public void Update(string? nombre, string? apellido)
		{
			if (nombre != null)
			{
				this.Nombre = nombre;
				ValidarNombre();
			}
			if (apellido != null)
			{
				this.Apellido = apellido;
				ValidarApellido();
			}
		}
	}
}
