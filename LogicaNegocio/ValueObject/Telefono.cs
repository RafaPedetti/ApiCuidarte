using LogicaNegocio.Excepciones.Cliente;
using LogicaNegocio.IntefacesDominio;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.ValueObject
{
	public class Telefono : IValidable
	{
		[Required]
		public string Value {  get; set; }


		public Telefono(string numero)
		{
			this.Value = numero;
			Validar();
		}

		public Telefono() { }

		public void Validar()
		{

			if (Value.Trim().Length < 8 || Value.Trim().Length > 9)
			{
				throw new TelefonoInvalidoException("Numero de telefono invalido");
			}
		}
	}
}
