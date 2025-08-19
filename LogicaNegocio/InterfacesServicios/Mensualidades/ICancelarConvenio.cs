using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.InterfacesServicios.Mensualidades
{
	public interface ICancelarConvenio<T>
	{
		public void Ejecutar(int id);
	}
}
