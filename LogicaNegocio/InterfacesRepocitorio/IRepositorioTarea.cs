using LogicaNegocio.Entidades;
using LogicaNegocio.InterfazRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.InterfacesRepocitorio
{
	public interface IRepositorioTarea : IRepositorio<Tarea>
	{
		public IEnumerable<Tarea> GetByTexto(string texto);

		public IEnumerable<Tarea> GetAll(int pagina);
		public int TotalItemsAsync();
	}
}
