using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.InterfacesServicios.Mensualidades
{
	public interface IObtenerPorCliente<T>
	{	
		public IEnumerable<T> Ejecutar(int id);
	}
}
