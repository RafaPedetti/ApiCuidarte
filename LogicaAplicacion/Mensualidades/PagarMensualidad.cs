using LogicaNegocio.Entidades;
using LogicaNegocio.InterfacesRepocitorio;
using LogicaNegocio.InterfacesServicios.Mensualidades;
using LogicaNegocio.ValueObject.Suscripcion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.Mensualidades
{
	public class PagarMensualidad : IPagarMensualidades<Suscripcion>
	{
		IRepositorioMensualidad _repositorioMensualidad;

		IRepositorioSuscripcion _repositorioSuscripcion;

		public PagarMensualidad(IRepositorioMensualidad repositorioMensualidad,IRepositorioSuscripcion repositorioSuscripcion)
		{
			_repositorioMensualidad = repositorioMensualidad;
			_repositorioSuscripcion = repositorioSuscripcion;
		}

		public Mensualidad Ejecutar(int id)
		{
			var suscripcion = _repositorioSuscripcion.GetById(id)
			?? throw new ArgumentException("Suscripción no encontrada");
			if (suscripcion.Estado == SuscripcionEstado.Cancelada)
				throw new InvalidOperationException("La suscripción no está activa.");
			var hoy = DateTime.UtcNow;
			var desde = new DateTime(hoy.Year, hoy.Month, 1);
			var hasta = desde.AddMonths(1).AddDays(-1); 
			var mensualidad = new Mensualidad
			{
				SubscriptionId = suscripcion.Id,
				FechaGeneracion = hoy,
				PeriodoDesde = desde,
				PeriodoHasta = hasta,
				Monto = suscripcion.Plan.Precio,
				Estado = MensualidadEstado.Pagada,
			};
			suscripcion.PagarMensualidad(mensualidad);
			_repositorioMensualidad.Add(mensualidad);
			return mensualidad;

		}

	}
}
