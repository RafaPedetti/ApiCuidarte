using LogicaNegocio.IntefacesDominio;
using LogicaNegocio.ValueObject.TipoPlan;
using System.ComponentModel.DataAnnotations;
namespace LogicaNegocio.Entidades
{
	public class TipoPlan : IEntity
	{
		public int Id { get; set; }

		[Required]
		public string Nombre { get; set; }

		[Required]
		public List<Servicio> Servicios { get; set; } = new List<Servicio>();

		public int? EmpresaId { get; set; }
		public Empresa? Empresa { get; set; }

		[Required]
		public decimal Precio { get; set; }

		public ICollection<Suscripcion> Suscripciones { get; set; }
		  = new List<Suscripcion>();

		public PlanDestino Destino { get; set; }

		public bool Eliminado { get; set; }


		public TipoPlan()
		{
		}

		public TipoPlan(int id, string nombre, List<Servicio> servicios, decimal precio, PlanDestino destino)
		{
			Id = id;
			Nombre = nombre;
			Servicios = servicios;
			Precio = precio;
			Destino = destino;
		}

		public TipoPlan(string nombre, List<Servicio> servicios, decimal precio, PlanDestino destino)
		{
			Nombre = nombre;
			Servicios = servicios;
			Precio = precio;
			Destino = destino;
		}

		public TipoPlan(int id, string nombre, decimal precio, PlanDestino destino)
		{
			Id = id;
			Nombre = nombre;
			Precio = precio;
			Destino = destino;
		}


		public void Update(TipoPlan obj)
		{
			this.Nombre = obj.Nombre;
			this.Precio = obj.Precio;
			this.Destino = obj.Destino;
			this.Servicios.RemoveAll(s => !obj.Servicios.Any(ns => ns.Id == s.Id));

			foreach (var nuevo in obj.Servicios)
			{
				var existente = this.Servicios.FirstOrDefault(s => s.Id == nuevo.Id);

				if (existente != null)
				{
					existente.cantServicios = nuevo.cantServicios;
					existente.tipoServicio = nuevo.tipoServicio;
				}
				else
				{
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
