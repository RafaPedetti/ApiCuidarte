using LogicaNegocio.IntefacesDominio;
using LogicaNegocio.ValueObject.Suscripcion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.Entidades
{
	public class Mensualidad : IEntity
	{
		public int Id { get; set; }
		public int SubscriptionId { get; set; }
		public Suscripcion? Subscription { get; set; }
		public DateTime FechaGeneracion { get; set; }
		public DateTime PeriodoDesde { get; set; }
		public DateTime PeriodoHasta { get; set; }
		public decimal Monto { get; set; }
		public DateTime? FechaPago { get; set; }
		public MensualidadEstado Estado { get; set; }
		public bool Eliminado { get; set; } = false;


		public Mensualidad()
		{
			FechaGeneracion = DateTime.Now;
			PeriodoDesde = DateTime.Now;
			PeriodoHasta = DateTime.Now.AddMonths(1);
			Monto = 0;
			Estado = MensualidadEstado.Pagada;
		}

		public Mensualidad(int subscriptionId, DateTime periodoDesde, DateTime periodoHasta, decimal monto)
		{
			SubscriptionId = subscriptionId;
			PeriodoDesde = periodoDesde;
			PeriodoHasta = periodoHasta;
			Monto = monto;
			FechaGeneracion = DateTime.Now;
			Estado = MensualidadEstado.Pagada;
			Eliminado = false;
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
			if (mensualidad.Monto < 0)
			{
				throw new ArgumentException("El monto no puede ser negativo.", nameof(mensualidad.Monto));
			}
			SubscriptionId = mensualidad.SubscriptionId;
			PeriodoDesde = mensualidad.PeriodoDesde;
			PeriodoHasta = mensualidad.PeriodoHasta;
			Monto = mensualidad.Monto;
			FechaPago = mensualidad.FechaPago;
			Estado = mensualidad.Estado;
			Eliminado = mensualidad.Eliminado;
			if (Eliminado)
			{
				Estado= MensualidadEstado.Cancelada;
			}
		}
	}
}
