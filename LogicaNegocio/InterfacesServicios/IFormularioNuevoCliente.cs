using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.InterfacesServicios
{
	public interface IFormularioNuevoCliente<T>
	{
		void Ejecutar(T obj);
	}
}
