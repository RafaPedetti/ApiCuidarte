using LogicaNegocio.Excepciones;
using LogicaNegocio.IntefacesDominio;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.Entidades
{
	public class FondoPortada : IEntity
	{
		public int Id { get; set; }

		[Required]
		public string Url { get; set; }



		public FondoPortada(int id, string url)
		{
			Id = id;
			Url = url;
		}

		public FondoPortada()
		{
		}

		public void Update(FondoPortada fondoPortada)
		{
			if (fondoPortada == null)
			{
				throw new DomainException(nameof(fondoPortada));
			}
			Url = fondoPortada.Url;
		}
	}
}
