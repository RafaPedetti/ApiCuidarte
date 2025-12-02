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
	public class RepositorioEmpresa : IRepositorioEmpresa
	{
		private CuidarteContext _context;
		public RepositorioEmpresa(CuidarteContext context)
		{
			_context = context;
		}
		public Empresa Add(Empresa obj)
		{
			if (obj == null) throw new ArgumentNullException(nameof(obj), "El objeto no puede ser nulo.");
			_context.Empresas.Add(obj);
			_context.SaveChanges();
			return obj;
		}

		public void Delete(int id)
		{
			Empresa empresa = GetById(id);
			if (empresa == null)
			{
				throw new KeyNotFoundException($"Empresa con ID {id} no encontrado.");
			}
			empresa.Eliminado = true;
			Update(empresa);
		}

		public IEnumerable<Empresa> GetAll()
		{
			return _context.Empresas
				.Include(e => e.Plan)
				.ThenInclude(tp => tp.Servicios)
				.ThenInclude(s => s.tipoServicio)
				.Include(e=> e.Suscripcion)
				.ThenInclude(s => s.Plan)
				.Where(c => !c.Eliminado).ToList();
		}

		public Empresa GetById(int id)
		{
			if (id == null)
			{
				throw new ArgumentNullException(nameof(id), "El ID no puede ser nulo.");
			}
			Empresa empresa = _context.Empresas
				.Include(e => e.Plan)
				.ThenInclude(tp => tp.Servicios)
				.ThenInclude(s => s.tipoServicio)
				.Include(c => c.Suscripcion)
				.FirstOrDefault(e => e.Id == id);

			if (empresa == null)
			{
				throw new KeyNotFoundException($"La empresa con ID {id} no encontrado.");
			}
			return empresa;
		}

		public Empresa Update(Empresa obj)
		{
			if (obj == null)
			{
				throw new ArgumentNullException(nameof(obj), "El objeto no puede ser nulo.");
			}
			Empresa empresa = GetById(obj.Id);
			empresa.Update(obj);
			_context.Empresas.Update(empresa);
			_context.SaveChanges();
			return empresa;
		}
	}
}
