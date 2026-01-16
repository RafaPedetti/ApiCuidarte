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

		public IEnumerable<MensualidadDto> Ejecutar(int id, int clienteId)
		{
			IEnumerable<Mensualidad> mensualidades = _repositorioMensualidad.GetByCliente(id);
			IEnumerable<Tarea> tareas = _repositorioTarea.GetTareasByCliente(clienteId);
			decimal precio = 0;
			if (tareas.Count() == 0)
			{
				foreach (var mensualidad in mensualidades)
				{
					mensualidad.PrecioProximaMensualidad = mensualidad.Subscription.Plan.PrecioConDescuentoNoUso;
				}
			}
			else
			{
				foreach (var mensualidad in mensualidades)
				{
					mensualidad.PrecioProximaMensualidad = mensualidad.Subscription.Plan.Precio;
				}
			}
				return MensualidadMapper.ToListaDto(mensualidades);
		}

	}
}
