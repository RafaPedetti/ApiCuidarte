using LogicaNegocio.Entidades;
using LogicaNegocio.Excepciones;
using LogicaNegocio.InterfacesRepocitorio;
using LogicaNegocio.InterfazRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructura.LogicaAccesoDatos.EF
{
	public class RepositorioTipoServicio : IRepositorioTipoServicio
	{
		private CuidarteContext _context;

		public RepositorioTipoServicio(CuidarteContext context)
		{
			_context = context;
		}

		public TipoServicio Add(TipoServicio obj)
		{
			if(obj == null) throw new ArgumentNullException(nameof(obj), "El objeto no puede ser nulo.");
			_context.TipoServicios.Add(obj);
			_context.SaveChanges();
			return obj;
		}
		public void Delete(int id)
		{
			TipoServicio tipoServicio = GetById(id);
			if(tipoServicio == null)
			{
				throw new DomainException($"Tipo de servicio con ID {id} no encontrado.");
			}
			tipoServicio.Eliminado = true; 
			Update(tipoServicio);
		}
		public IEnumerable<TipoServicio> GetAll()
		{
			return _context.TipoServicios
				.Where(ts => !ts.Eliminado)
				.ToList();
		}

		public TipoServicio GetById(int id)
		{
			if(id == null)
			{
				throw new ArgumentNullException(nameof(id), "El ID no puede ser nulo.");
			}
			TipoServicio tipoServicio = _context.TipoServicios.FirstOrDefault(ts => ts.Id == id && !ts.Eliminado);
			if(tipoServicio == null)
			{
				throw new DomainException($"Tipo de servicio con ID {id} no encontrado.");
			}
			return tipoServicio;
		}
		public TipoServicio Update(TipoServicio obj)
		{
			if(obj == null)
			{
				throw new ArgumentNullException(nameof(obj), "El objeto no puede ser nulo.");
			}
			TipoServicio tipoServicio = GetById(obj.Id);
			if(tipoServicio == null) throw new DomainException($"Tipo de servicio con ID {obj.Id} no encontrado.");
			tipoServicio.Update(obj);
			_context.TipoServicios.Update(tipoServicio);
			_context.SaveChanges();
			return tipoServicio;
		}
	}
}
