using LogicaNegocio.IntefacesDominio;
using LogicaNegocio.ValueObject.Tarea;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.Entidades
{
	public class Tarea : IEntity, IValidable
	{
		public int Id { get; set; }

		[Required]
		public Cliente Cliente { get; set; }

		[Required]
		public Usuario EmpleadoResponsable { get; set; }

		[Required]
		public DateTime fecha { get; set; }

		[Required]
		public EstadoTarea Estado { get; set; }

		public string Descripcion { get; set; }

		public List<Servicio> serviciosUsados { get; set; }

		public List<Servicio> ServiciosExtras { get; set; }

		[NotMapped]
		public decimal CostoTotal =>
			ServiciosExtras?.Sum(s => s.cantServicios * s.tipoServicio.PrecioHora) ?? 0;


		public bool Eliminado { get; set; }
		public Tarea(int id, Cliente c, Usuario e, DateTime f, string? desc, List<Servicio>? servicios, List<Servicio>? serviciosExtra)
		{
			this.Cliente = c ?? throw new ArgumentNullException(nameof(c));
			this.EmpleadoResponsable = e ?? throw new ArgumentNullException(nameof(e));
			this.fecha = f;
			this.Estado = EstadoTarea.Pendiente;
			this.Descripcion = desc ?? string.Empty;
			this.serviciosUsados = servicios ?? new List<Servicio>();
			this.ServiciosExtras = serviciosExtra ?? new List<Servicio>();

		}

		public Tarea(int id, DateTime f, string? desc, EstadoTarea estado)
		{
			this.Id = id;
			this.fecha = f;
			this.Estado = estado;
			this.Descripcion = desc ?? string.Empty;
			this.serviciosUsados = new List<Servicio>();
			this.ServiciosExtras = new List<Servicio>();
		}


		public Tarea() { }
		public void Validar()
		{
			throw new NotImplementedException();
		}


		public void Update(Cliente? c, Usuario? e, DateTime? f, string? d, EstadoTarea? et)
		{
			if (c != null) this.Cliente = c;
			if (e != null) this.EmpleadoResponsable = e;
			if (f != null) this.fecha = (DateTime)f;
			if (d != null) this.Descripcion = d ?? string.Empty;
			if (et != null) this.Estado = (EstadoTarea)et;
		}
		public void AplicarConsumoServicios(Cliente cliente, List<Servicio> serviciosSolicitados)
		{
			if (cliente == null || serviciosSolicitados == null) return;

			foreach (var solicitado in serviciosSolicitados)
			{
				if (solicitado.cantServicios <= 0 || solicitado.tipoServicio == null) continue;

				var disponible = cliente.ServiciosDisponibles
					.FirstOrDefault(s => s.tipoServicio == solicitado.tipoServicio);

				if (disponible == null || disponible.cantServicios == 0)
				{
					AgregarExtra(solicitado.tipoServicio, solicitado.cantServicios);
					continue;
				}

				if (disponible.cantServicios >= solicitado.cantServicios)
				{
					disponible.cantServicios -= solicitado.cantServicios;
				}
				else
				{
					int cubierto = disponible.cantServicios;
					int excedente = solicitado.cantServicios - cubierto;

					disponible.cantServicios = 0;
					AgregarExtra(solicitado.tipoServicio, excedente);
				}
			}
		}
		private void AgregarExtra(TipoServicio tipo, int cantidad)
		{
			var existente = ServiciosExtras.FirstOrDefault(s => s.tipoServicio == tipo);
			if (existente != null)
			{
				existente.cantServicios += cantidad;
			}
			else
			{
				ServiciosExtras.Add(new Servicio
				{
					tipoServicio = tipo,
					cantServicios = cantidad
				});
			}
		}
	}
}
