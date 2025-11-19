using LogicaNegocio.Entidades;
using LogicaNegocio.InterfacesRepocitorio;
using LogicaNegocio.InterfazRepositorio;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructura.LogicaAccesoDatos.EF
{
	public class RepositorioFondoPortada : IRepositorioFondoPortada
	{
		private CuidarteContext _context;
		public RepositorioFondoPortada(CuidarteContext context)
		{
			_context = context;
		}
		public FondoPortada Add(FondoPortada obj)
		{
			if (obj == null) throw new ArgumentNullException(nameof(obj), "El objeto no puede ser nulo.");

			var fondoExistente = _context.FondosPortada.SingleOrDefault();

			if (fondoExistente != null)
			{
				fondoExistente.Url = obj.Url;
			}
			else
			{
				_context.FondosPortada.Add(obj);
			}

			_context.SaveChanges();
			return obj;

		}

		public void Delete(int id)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<FondoPortada> GetAll()
		{
			throw new NotImplementedException();
		}

		public FondoPortada GetById(int id)
		{
			return _context.FondosPortada.FirstOrDefault();

		}

		public FondoPortada Update(FondoPortada obj)
		{
			throw new NotImplementedException();
		}
	}
}
