using LogicaAplicacion.Dtos.MapeosDto;
using LogicaAplicacion.Dtos.Usuarios;
using LogicaNegocio.Entidades;
using LogicaNegocio.InterfacesRepocitorio;
using LogicaNegocio.InterfacesServicios.Mensualidades;
using LogicaNegocio.InterfazServicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.Mensualidades
{
	public class GetAllMensualidad : IObtenerPorCliente<Mensualidad>
	{
		IRepositorioMensualidad _repositorioMensualidad;

		public GetAllMensualidad(IRepositorioMensualidad repositorioMensualidad)
		{
			_repositorioMensualidad = repositorioMensualidad;
		}

		public IEnumerable<Mensualidad> Ejecutar(int id)
		{
			IEnumerable<Mensualidad> mensualidades = _repositorioMensualidad.GetByCliente(id);
			return mensualidades;
		}

	}
}
