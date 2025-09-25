using LogicaAplicacion.Dtos.Suscripciones;
using LogicaNegocio.Entidades;
using LogicaNegocio.InterfacesRepocitorio;
using LogicaNegocio.InterfacesServicios.Mensualidades;
using LogicaNegocio.ValueObject.Suscripcion;
namespace LogicaAplicacion.Mensualidades
{
	public class PagarMensualidad : IPagarMensualidades<SuscripcionDto>
	{
		IRepositorioMensualidad _repositorioMensualidad;

		IRepositorioSuscripcion _repositorioSuscripcion;
		IRepositorioCliente _repositorioCliente;

		public PagarMensualidad(IRepositorioMensualidad repositorioMensualidad,IRepositorioSuscripcion repositorioSuscripcion, IRepositorioCliente repositorioCliente)
		{
			_repositorioMensualidad = repositorioMensualidad;
			_repositorioSuscripcion = repositorioSuscripcion;
			_repositorioCliente = repositorioCliente;
		}

		public Mensualidad Ejecutar(int id)
		{
			var suscripcion = _repositorioSuscripcion.GetById(id)
			?? throw new ArgumentException("Suscripción no encontrada");
			if (suscripcion.Estado == SuscripcionEstado.Cancelada)
				throw new InvalidOperationException("La suscripción no está activa.");
			var hoy = DateTime.UtcNow;
			var desde =new DateTime(hoy.Year, hoy.Month, 1);
			var hasta = DateOnly.FromDateTime(desde.AddMonths(1).AddDays(-1)); 
			var mensualidad = new Mensualidad
			{
				SubscriptionId = suscripcion.Id,
				PeriodoDesde = DateOnly.FromDateTime(desde),
				PeriodoHasta = hasta,
				Estado = MensualidadEstado.Pagada,
			};
			suscripcion.PagarMensualidad(mensualidad);
			_repositorioMensualidad.Add(mensualidad);
			if (hoy.Month == 1)
			{
				foreach(var cliente in suscripcion.Clientes)
				{
					cliente.ResetearServicios();
					_repositorioCliente.Update(cliente);
				}
			}

			return mensualidad;

		}

	}
}
