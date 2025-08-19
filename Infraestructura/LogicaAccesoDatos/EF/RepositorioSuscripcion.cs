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
	public class RepositorioSuscripcion : IRepositorioSuscripcion
	{
		private CuidarteContext _context;
		public RepositorioSuscripcion(CuidarteContext context)
		{
			_context = context;
		}

		public Suscripcion Add(Suscripcion obj)
		{
			if (obj == null) throw new ArgumentNullException(nameof(obj), "El objeto no puede ser nulo.");
			foreach (var cliente in obj.Clientes)
			{
				_context.Attach(cliente);
			}

			if (obj.Empresa != null)
			{
				_context.Attach(obj.Empresa);
			}
			_context.Suscripciones.Add(obj);
			_context.SaveChanges();
			return obj;
		}

		public void Delete(int id)
		{
			Suscripcion suscripcion = GetById(id);
			if (suscripcion == null)
			{
				throw new KeyNotFoundException($"Suscripcion con ID {id} no encontrado.");
			}
			suscripcion.Eliminado = true;
			Update(suscripcion);
		}

		public IEnumerable<Suscripcion> GetAll()
		{
			return _context.Suscripciones
				.Include(s => s.Plan)
				.Include(s => s.Clientes)
				.Include(s => s.Empresa)
				.Include(s => s.Mensualidades)
				.Where(s => !s.Eliminado)
				.ToList();
		}

		public Suscripcion GetById(int id)
		{
			if (id == null)
			{
				throw new ArgumentNullException(nameof(id), "El ID no puede ser nulo.");
			}
			Suscripcion suscripcion = _context.Suscripciones
				.Include(s => s.Plan)
				.Include(s => s.Clientes)
				.Include(s => s.Empresa)
			    .Include(s => s.Mensualidades)
				.FirstOrDefault(tp => tp.Id == id);

			if (suscripcion == null)
			{
				throw new KeyNotFoundException($"Suscripcion con ID {id} no encontrado.");
			}
			return suscripcion;
		}

		public Suscripcion GetByIdCliente(int id)
		{
			if (id == null)
			{
				throw new ArgumentNullException(nameof(id), "El ID no puede ser nulo.");
			}
			Suscripcion suscripcion = _context.Suscripciones
				.Include(s => s.Plan)
				.Include(s => s.Clientes)
				.Include(s => s.Empresa)
				.Include(s => s.Mensualidades)
				.FirstOrDefault(s => s.Clientes.Any(c => c.Id == id));

			if (suscripcion == null)
			{
				throw new KeyNotFoundException($"Suscripcion con ID {id} no encontrado.");
			}
			return suscripcion;
		}


		public Suscripcion Update(Suscripcion obj)
		{
			if (obj == null)
			{
				throw new ArgumentNullException(nameof(obj), "El objeto no puede ser nulo.");
			}
			Suscripcion suscripcion = GetById(obj.Id);
			suscripcion.Update(obj);
			_context.Suscripciones.Update(suscripcion);
			_context.SaveChanges();
			return suscripcion;
		}
	}
}
