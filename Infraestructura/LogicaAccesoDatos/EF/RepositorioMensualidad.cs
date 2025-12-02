using LogicaNegocio.Entidades;
using LogicaNegocio.InterfacesRepocitorio;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructura.LogicaAccesoDatos.EF
{
	public class RepositorioMensualidad : IRepositorioMensualidad
	{
		private CuidarteContext _context;
		public RepositorioMensualidad(CuidarteContext context)
		{
			_context = context;
		}

		public Mensualidad Add(Mensualidad obj)
		{
			if (obj == null) throw new ArgumentNullException(nameof(obj), "El objeto no puede ser nulo.");
			_context.Mensualidades.Add(obj);
			_context.SaveChanges();
			return obj;
		}

		public void Delete(int id)
		{
			Mensualidad mensualidad = GetById(id);
			if (mensualidad == null)
			{
				throw new KeyNotFoundException($"Suscripcion con ID {id} no encontrado.");
			}
			mensualidad.Eliminado = true;
			Update(mensualidad);
		}

		public IEnumerable<Mensualidad> GetAll()
		{
			return _context.Mensualidades
				.Where(s => !s.Eliminado)
				.ToList();
		}

		public IEnumerable<Mensualidad> GetByCliente(int id)
		{
			return _context.Mensualidades
				.Include(m => m.Subscription)
	.ThenInclude(s => s.Clientes)
.Include(m => m.Subscription)
	.ThenInclude(s => s.Plan)

				.Where(m => m.Subscription != null &&
							m.Subscription.Id == id &&
							!m.Eliminado)
				.Take(12).ToList();
		}

		public Mensualidad GetById(int id)
		{
			if (id == null)
			{
				throw new ArgumentNullException(nameof(id), "El ID no puede ser nulo.");
			}
			Mensualidad mensualidad = _context.Mensualidades
				.FirstOrDefault(tp => tp.Id == id);

			if (mensualidad == null)
			{
				throw new KeyNotFoundException($"Mensualidad con ID {id} no encontrado.");
			}
			return mensualidad;
		}


		public Mensualidad Update(Mensualidad obj)
		{
			if (obj == null)
			{
				throw new ArgumentNullException(nameof(obj), "El objeto no puede ser nulo.");
			}
			Mensualidad suscripcion = GetById(obj.Id);
			suscripcion.Update(obj);
			_context.Mensualidades.Update(suscripcion);
			_context.SaveChanges();
			return suscripcion;
		}
	}
}
