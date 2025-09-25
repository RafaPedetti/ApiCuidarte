using LogicaAplicacion.Dtos.Clientes;
using LogicaAplicacion.Dtos.MapeosDto;
using LogicaAplicacion.Dtos.Tareas;
using LogicaNegocio.Entidades;
using LogicaNegocio.InterfacesRepocitorio;
using LogicaNegocio.InterfazServicios;

namespace LogicaAplicacion.Tareas
{
	public class EditarTarea : IEditar<TareaDto>
	{
		public readonly IRepositorioCliente _contextC;
		public readonly IRepositorioUsuario _contextU;
		public readonly IRepositorioTarea _contextT;

		public EditarTarea(IRepositorioCliente contextC, IRepositorioTarea contextT, IRepositorioUsuario contextU)
		{
			_contextC = contextC;
			_contextT = contextT;
			_contextU = contextU;
		}
		public TareaDto Ejecutar(TareaDto obj)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("El tipo de objeto esta vacio");
			}
			if (obj.id <= 0)
			{
				throw new ArgumentException("El Id del cliente debe ser mayor a 0");
			}
			Tarea t = TareaMapper.FromDto(obj);
			Cliente c = _contextC.GetById(obj.clienteId);
			Usuario u = _contextU.GetById(obj.responsableId);
			t.Cliente = c;
			t.EmpleadoResponsable = u;
			TareaDto tDto = TareaMapper.ToDto(_contextT.Update(t));
			return tDto;

		}
	}
}
