using LogicaNegocio.Entidades;
using LogicaNegocio.InterfazRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.InterfacesRepocitorio
{
	public interface IRepositorioCliente : IRepositorio<Cliente>
	{
		public void CambiarPlan(TipoPlan tipoPlan);
	}
}
