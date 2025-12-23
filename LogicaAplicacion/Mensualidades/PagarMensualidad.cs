using LogicaAplicacion.Dtos.Suscripciones;
using LogicaNegocio.Entidades;
using LogicaNegocio.InterfacesRepocitorio;
using LogicaNegocio.InterfacesServicios.Mensualidades;
using LogicaNegocio.ValueObject.Suscripcion;
namespace LogicaAplicacion.Mensualidades
{
	public class PagarMensualidad : IPagarMensualidades<SuscripcionDto>
	{
		IRepositorioMensualidad _contextMensualidad;
		IRepositorioSuscripcion _contextSuscripcion;
		IRepositorioCliente _contextCliente;
		IRepositorioTarea _contextTarea;

		public PagarMensualidad(IRepositorioMensualidad repositorioMensualidad, IRepositorioSuscripcion repositorioSuscripcion, IRepositorioCliente repositorioCliente, IRepositorioTarea repositorioTarea)
		{
			_contextMensualidad = repositorioMensualidad;
			_contextSuscripcion = repositorioSuscripcion;
			_contextCliente = repositorioCliente;
			_contextTarea = repositorioTarea;
		}

		public Mensualidad Ejecutar(int id, int? ClienteId)
		{
			Suscripcion suscripcion = _contextSuscripcion.GetById(id)
			?? throw new ArgumentException("Suscripción no encontrada");
			if (suscripcion.Estado == SuscripcionEstado.Cancelada)
				throw new InvalidOperationException("La suscripción no está activa.");
			var hoy = DateTime.UtcNow;
			var desde = new DateTime(hoy.Year, hoy.Month, 1);
			var hasta = DateOnly.FromDateTime(desde.AddMonths(1).AddDays(-1));
			decimal precio = suscripcion.Plan.Precio;
			if (ClienteId != null)
			{
				IEnumerable<Tarea> tareas = _contextTarea.GetTareasByCliente((int)ClienteId);
				if (tareas.Count() == 0)
				{
					precio = suscripcion.Plan.PrecioConDescuentoNoUso;
				}
			if (tareas.Count() == 0)
			{
				precio = suscripcion.Plan.PrecioConDescuentoNoUso;
			}
			}
			var mensualidad = new Mensualidad
			{
				SubscriptionId = suscripcion.Id,
				PeriodoDesde = DateOnly.FromDateTime(desde),
				PeriodoHasta = hasta,
				Estado = MensualidadEstado.Pagada,
				Precio = precio
			};
			suscripcion.PagarMensualidad(mensualidad);
			_contextMensualidad.Add(mensualidad);

			if (suscripcion.Mensualidades.Count % 12 == 0)
			{
				foreach (var cliente in suscripcion.Clientes)
				{
					cliente.ResetearServicios();
					_contextCliente.Update(cliente);
				}
			}

			return mensualidad;

		}

	}
}
