using LogicaAplicacion.Dtos.MapeosDto;
using LogicaAplicacion.Dtos.Tareas;
using LogicaNegocio.InterfacesRepocitorio;
using LogicaNegocio.InterfazServicios;

namespace LogicaAplicacion.Tareas
{
	public class GetByIdTarea : IObtener<TareaDto>
	{
		private readonly IRepositorioTarea _context;
		public GetByIdTarea(IRepositorioTarea context)
		{
			_context = context;
		}

		public TareaDto Ejecutar(int id)
		{
			TareaDto tDto = TareaMapper.ToDto(_context.GetById(id));
			return tDto;
		}

	}
}
