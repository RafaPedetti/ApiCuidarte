using LogicaNegocio.IntefacesDominio;
using LogicaNegocio.ValueObject;
using LogicaNegocio.ValueObject.Usuario;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.Entidades
{
	public abstract class Usuario : IEntity
	{
		[Required]
		public int Id { get; set; }

		[Required]
		public Email Email { get; set; }

		[Required]
		public NombreCompleto NombreCompleto { get; set; }

		[Required]
		public Password Password { get; set; }

		public bool Eliminado { get; set; } = false;

		public  abstract string  Discriminador { get; set; }

		public Usuario(string email, string nombre, string apellido, string password)
		{
			NombreCompleto = new NombreCompleto(nombre,apellido);
			Password = new Password(password);
			this.Email = new Email(email);
		}

		public Usuario() { }

		public void Update(string email,string nombre, string apellido, string pass)
		{
			if(email != null)
			{
				this.Email = new Email(email);
			}
			if (nombre != null && apellido != null)
			{
				this.NombreCompleto = new NombreCompleto(nombre,apellido);
			}
			if(pass != null)
			{
				this.Password = new Password(pass);
			}
		}

	}

}
