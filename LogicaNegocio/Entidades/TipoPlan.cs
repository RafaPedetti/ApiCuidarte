using LogicaNegocio.IntefacesDominio;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.Entidades
{
	public class TipoPlan : IEntity
	{
		public int Id { get; set; }

		[Required]
		public string Nombre { get; set; }

		[Required]
		public List<Servicio> Servicios{ get; set;} = new List<Servicio>();

		public int? EmpresaId { get; set; }
		public Empresa? Empresa { get; set; }

		[Required]
		public decimal Precio { get; set; }

		public ICollection<Suscripcion> Suscripciones { get; set; }
		  = new List<Suscripcion>();

		public bool Eliminado { get; set; }


		public TipoPlan()
		{
		}

		public TipoPlan(int id, string nombre, List<Servicio> servicios, decimal precio)
		{
			Id = id;
			Nombre = nombre;
			Servicios = servicios;
			Precio = precio;
		}

		public TipoPlan(string nombre, List<Servicio> servicios, decimal precio)
		{
			Nombre = nombre;
			Servicios = servicios;
			Precio = precio;
		}

		public void Update(TipoPlan obj)
		{
			this.Nombre = obj.Nombre;

			// Eliminar servicios que ya no están
			this.Servicios.RemoveAll(s => !obj.Servicios.Any(ns => ns.Id == s.Id));

			foreach (var nuevo in obj.Servicios)
			{
				var existente = this.Servicios.FirstOrDefault(s => s.Id == nuevo.Id);

				if (existente != null)
				{
					// Actualizar propiedades
					existente.cantServicios = nuevo.cantServicios;
					existente.tipoServicio = nuevo.tipoServicio;
				}
				else
				{
					// Agregar nuevo servicio
					this.Servicios.Add(new Servicio
					{
						cantServicios = nuevo.cantServicios,
						tipoServicio = nuevo.tipoServicio
					});
				}
			}
		}



		public void AddServicio(Servicio s)
		{
			if (this.Servicios == null) this.Servicios = new List<Servicio>();
			this.Servicios.Add(s);
		}
	}
}
