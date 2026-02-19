using LogicaNegocio.Entidades;
using LogicaNegocio.Excepciones;
using LogicaNegocio.InterfacesRepocitorio;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructura.LogicaAccesoDatos.EF
{
	public class RepositorioTipoPlan : IRepositorioTipoPlan
	{
		private CuidarteContext _context;
		public RepositorioTipoPlan(CuidarteContext context)
		{
			_context = context;
		}

		public TipoPlan Add(TipoPlan obj)
		{
			if (obj == null) throw new ArgumentNullException(nameof(obj), "El objeto no puede ser nulo.");
			foreach (var servicio in obj.Servicios)
			{
				_context.Entry(servicio.tipoServicio).State = EntityState.Unchanged;
			}

			_context.TiposPlanes.Add(obj);
			_context.SaveChanges();
			return obj;
		}

		public void Delete(int id)
		{
			TipoPlan tipoPlan = GetById(id);
			if (tipoPlan == null)
			{
				throw new DomainException($"Tipo de plan con ID {id} no encontrado.");
			}
			tipoPlan.Eliminado = true;
			Update(tipoPlan);
		}

		public IEnumerable<TipoPlan> GetAll()
		{
			return _context.TiposPlanes.Include(tp => tp.Servicios)
			.ThenInclude(s => s.tipoServicio)
			.Where(ts => !ts.Eliminado).ToList();
		}

		public TipoPlan GetById(int id)
		{
			if (id == null)
			{
				throw new ArgumentNullException(nameof(id), "El ID no puede ser nulo.");
			}
			TipoPlan tipoPlan = _context.TiposPlanes
				.Include(tp => tp.Servicios)
				.ThenInclude(s => s.tipoServicio)
				.FirstOrDefault(tp => tp.Id == id);

			if (tipoPlan == null)
			{
				throw new DomainException($"Tipo de plan con ID {id} no encontrado.");
			}
			return tipoPlan;
		}

		public TipoPlan Update(TipoPlan obj)
		{
			if (obj == null)
			{
				throw new ArgumentNullException(nameof(obj), "El objeto no puede ser nulo.");
			}
			TipoPlan tipoPlan = GetById(obj.Id);
			tipoPlan.Update(obj);
			_context.TiposPlanes.Update(tipoPlan);
			_context.SaveChanges();
			return tipoPlan;
		}
	}
}