using LogicaNegocio.Entidades;
using LogicaNegocio.InterfacesRepocitorio;
using System;
using System.Collections.Generic;
using System.Linq;
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

		public void Add(TipoPlan obj)
		{
			if (obj == null) throw new ArgumentNullException(nameof(obj), "El objeto no puede ser nulo.");
			_context.TiposPlanes.Add(obj);
			_context.SaveChanges();
		}

		public void Delete(int id)
		{
			TipoPlan tipoPlan = GetById(id);
			if (tipoPlan == null)
			{
				throw new KeyNotFoundException($"Tipo de plan con ID {id} no encontrado.");
			}
			tipoPlan.Eliminado = true;
			Update(id, tipoPlan);
		}

		public IEnumerable<TipoPlan> GetAll()
		{
			return _context.TiposPlanes
			.Where(ts => !ts.Eliminado)
			.ToList();
		}

		public TipoPlan GetById(int id)
		{
			if (id == null)
			{
				throw new ArgumentNullException(nameof(id), "El ID no puede ser nulo.");
			}
			TipoPlan tipoPlan = _context.TiposPlanes.FirstOrDefault(tp => tp.Id == id && !tp.Eliminado);
			if (tipoPlan == null)
			{
				throw new KeyNotFoundException($"Tipo de plan con ID {id} no encontrado.");
			}
			return tipoPlan;
		}

		public void Update(int id, TipoPlan obj)
		{
			if (obj == null)
			{
				throw new ArgumentNullException(nameof(obj), "El objeto no puede ser nulo.");
			}
			TipoPlan tipoPlan = GetById(id);
			tipoPlan.Update(obj);
			_context.TiposPlanes.Update(tipoPlan);
			_context.SaveChanges();
		}
	}
}