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
		public IEnumerable<Tarea> GetByTexto(string texto,string? usuario);

		public IEnumerable<Tarea> GetAll(int pagina,string? usuario);
		public int TotalItemsAsync();

		public int GetHorasFuncionario(int idFuncionario, int mes, int anio);

	}
}
