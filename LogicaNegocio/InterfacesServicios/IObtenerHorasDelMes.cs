using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.InterfacesServicios
{
	public interface IObtenerHorasDelMes
	{
		public IEnumerable<int> Ejecutar(int idFuncionario, int mes, int anio);
	}
}
