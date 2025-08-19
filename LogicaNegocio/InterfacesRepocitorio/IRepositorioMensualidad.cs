using LogicaNegocio.Entidades;
using LogicaNegocio.InterfazRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.InterfacesRepocitorio
{
	public interface IRepositorioMensualidad : IRepositorio<Mensualidad>
	{
		public IEnumerable<Mensualidad> GetByCliente(int idCliente);
	}
}
