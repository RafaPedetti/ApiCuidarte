using LogicaNegocio.IntefacesDominio;
using LogicaNegocio.ValueObject.Suscripcion;

namespace LogicaNegocio.Entidades
{
	public class Mensualidad : IEntity
	{
		public int Id { get; set; }
		public int SubscriptionId { get; set; }
		public Suscripcion? Subscription { get; set; }
		public decimal Precio { get; set; }
		public DateOnly PeriodoDesde { get; set; }
		public DateOnly PeriodoHasta { get; set; }
		public MensualidadEstado Estado { get; set; }

		public decimal PrecioProximaMensualidad { get; set; }
		public bool Eliminado { get; set; } = false;


		public Mensualidad()
		{
			PeriodoDesde = DateOnly.FromDateTime(DateTime.UtcNow);
			PeriodoHasta = DateOnly.FromDateTime(DateTime.Now.AddMonths(1));
			Estado = MensualidadEstado.Pagada;
		}

		public Mensualidad(int subscriptionId, DateOnly periodoDesde, DateOnly periodoHasta, decimal precio)
		{
			SubscriptionId = subscriptionId;
			PeriodoDesde = periodoDesde;
			PeriodoHasta = periodoHasta;
			Estado = MensualidadEstado.Pagada;
			Precio = precio;
		}
		public Mensualidad(int id,int subscriptionId,Suscripcion? suscripcion, DateOnly periodoDesde, DateOnly periodoHasta, MensualidadEstado estado, decimal precio, decimal precioProximaMensualidad)
		{
			Id = id;
			SubscriptionId = subscriptionId;
			if (suscripcion != null) Subscription = suscripcion;
			PeriodoDesde = periodoDesde;
			PeriodoHasta = periodoHasta;
			Estado = estado;
			Precio = precio;
			PrecioProximaMensualidad = precioProximaMensualidad;
		}


		public void Update(Mensualidad mensualidad)
		{
			if (mensualidad == null)
			{
				throw new ArgumentNullException(nameof(mensualidad), "La mensualidad no puede ser nula.");
			}
			if (mensualidad.SubscriptionId <= 0)
			{
				throw new ArgumentException("El ID de la suscripción debe ser mayor que cero.", nameof(mensualidad.SubscriptionId));
			}
			if (mensualidad.PeriodoDesde >= mensualidad.PeriodoHasta)
			{
				throw new ArgumentException("La fecha de inicio del periodo debe ser anterior a la fecha de fin del periodo.", nameof(mensualidad.PeriodoDesde));
			}
			SubscriptionId = mensualidad.SubscriptionId;
			PeriodoDesde = mensualidad.PeriodoDesde;
			PeriodoHasta = mensualidad.PeriodoHasta;
			Estado = mensualidad.Estado;
			Eliminado = mensualidad.Eliminado;
			Precio = mensualidad.Precio;
			if (Eliminado)
			{
				Estado= MensualidadEstado.Cancelada;
			}
		}
	}
}
