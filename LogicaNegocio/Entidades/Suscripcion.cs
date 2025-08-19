using LogicaNegocio.IntefacesDominio;
using LogicaNegocio.ValueObject.Suscripcion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.Entidades
{
	public class Suscripcion : IEntity
	{
		public int Id { get; set; }

		public int? ClienteId { get; set; }
		public ICollection<Cliente> Clientes { get; set; }
			= new List<Cliente>();
		public int? EmpresaId { get; set; }         
		public Empresa? Empresa { get; set; }

		public int PlanId { get; set; }
		public TipoPlan Plan { get; set; }

		public DateTime FechaInicio { get; set; }
		public DateTime ProximoCobro { get; set; }
		public SuscripcionEstado Estado { get; set; }

		public ICollection<Mensualidad> Mensualidades { get; set; }
			= new List<Mensualidad>();

		public bool Eliminado { get; set; } = false;

		public Suscripcion() { }

		public Suscripcion(int? clienteId, int? empresaId, int planId, DateTime fechaInicio, DateTime proximoCobro)
		{
			if ((clienteId is null && empresaId is null) || (clienteId != null && empresaId != null))
				throw new ArgumentException("Una suscripción debe tener un cliente o una empresa, pero no ambos.");
			if (clienteId is null && clienteId != null)  ClienteId = clienteId;
			if (empresaId is null && empresaId != null) EmpresaId = empresaId;
			PlanId = planId;
			FechaInicio = fechaInicio;
			ProximoCobro = proximoCobro;
			Estado = SuscripcionEstado.Activa;
		}

		public Suscripcion(int? clienteId, int? empresaId, int planId)
		{
			if ((clienteId is null && empresaId is null) || (clienteId != null && empresaId != null))
				throw new ArgumentException("Una suscripción debe tener un cliente o una empresa, pero no ambos.");
			if (clienteId != null) ClienteId = clienteId;
			if (empresaId != null) EmpresaId = empresaId;
			PlanId = planId;
			FechaInicio = DateTime.UtcNow;
			ProximoCobro = DateTime.UtcNow.AddMonths(1);
			Estado = SuscripcionEstado.Activa;
		}

		public Suscripcion(Cliente cliente, TipoPlan plan)
		{
			if (cliente != null)
			{
				this.Clientes.Add(cliente);
				ClienteId = cliente.Id;
			}
			PlanId = plan.Id;
			Plan = plan;
			FechaInicio = DateTime.UtcNow;
			ProximoCobro = DateTime.UtcNow.AddMonths(1);
			Estado = SuscripcionEstado.Activa;
			Mensualidad mensualidad = new Mensualidad(this.Id, this.FechaInicio, this.ProximoCobro, plan.Precio);
			Mensualidades.Add(mensualidad);
		}

		public Suscripcion(Empresa empresa, TipoPlan plan)
		{
			if (empresa != null)
			{
				this.Empresa = empresa;
				this.EmpresaId = empresa.Id;
			}
			PlanId = plan.Id;
			Plan = plan;
			FechaInicio = DateTime.UtcNow;
			ProximoCobro = DateTime.UtcNow.AddMonths(1);
			Estado = SuscripcionEstado.Activa;
			Mensualidad mensualidad = new Mensualidad(this.Id, this.FechaInicio, this.ProximoCobro, plan.Precio);
			Mensualidades.Add(mensualidad);
		}
		public void Update(Suscripcion suscripcionUpdate)
		{
			if (suscripcionUpdate == null) throw new ArgumentNullException(nameof(suscripcionUpdate), "El objeto de actualización no puede ser nulo.");
			if (suscripcionUpdate.ClienteId != null) ClienteId = suscripcionUpdate.ClienteId;
			if (suscripcionUpdate.EmpresaId != null) EmpresaId = suscripcionUpdate.EmpresaId;
			if (suscripcionUpdate.PlanId != 0) PlanId = suscripcionUpdate.PlanId;
			if (suscripcionUpdate.FechaInicio != default) FechaInicio = suscripcionUpdate.FechaInicio;
			if (suscripcionUpdate.ProximoCobro != default) ProximoCobro = suscripcionUpdate.ProximoCobro;
			Estado = suscripcionUpdate.Estado;
			this.Eliminado= suscripcionUpdate.Eliminado;
		}

		public void PagarMensualidad(Mensualidad m)
		{
			if (m == null)
				throw new ArgumentNullException(nameof(m));

			if (Mensualidades.Any(x => x.PeriodoDesde == m.PeriodoDesde && x.PeriodoHasta == m.PeriodoHasta))
				throw new InvalidOperationException("La mensualidad ya fue registrada.");

			this.Mensualidades.Add(m);
			this.ProximoCobro = m.PeriodoHasta;
			this.FechaInicio = m.PeriodoDesde;

		}
	}

}

