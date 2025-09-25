using LogicaAplicacion.Dtos.MapeosDto;
using LogicaAplicacion.Dtos.Suscripciones;
using LogicaNegocio.Entidades;
using LogicaNegocio.InterfacesRepocitorio;
using LogicaNegocio.InterfacesServicios.Mensualidades;

namespace LogicaAplicacion.Mensualidades
{
	public class GetAllMensualidad : IObtenerPorCliente<MensualidadDto>
	{
		IRepositorioMensualidad _repositorioMensualidad;
		IRepositorioTarea _repositorioTarea;

		public GetAllMensualidad(IRepositorioMensualidad repositorioMensualidad, IRepositorioTarea repositorioTarea)
		{
			_repositorioMensualidad = repositorioMensualidad;
			_repositorioTarea = repositorioTarea;
		}

		public IEnumerable<MensualidadDto> Ejecutar(int id)
		{
			IEnumerable<Mensualidad> mensualidades = _repositorioMensualidad.GetByCliente(id);
			return MensualidadMapper.ToListaDto(mensualidades);
		}

	}
}
