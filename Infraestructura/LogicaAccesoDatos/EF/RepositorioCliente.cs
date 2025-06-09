using LogicaNegocio.Entidades;
using LogicaNegocio.InterfacesRepocitorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructura.LogicaAccesoDatos.EF
{
	public class RepositorioCliente : IRepositorioCliente
	{
		public void Add(Cliente obj)
		{
			throw new NotImplementedException();
		}

		public void CambiarPlan(TipoPlan tipoPlan)
		{
			throw new NotImplementedException();
		}

		public void Delete(int id)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<Cliente> GetAll()
		{
			throw new NotImplementedException();
		}

		public Cliente GetById(int id)
		{
			throw new NotImplementedException();
		}

		public void Update(int id, Cliente obj)
		{
			throw new NotImplementedException();
		}
	}
}
