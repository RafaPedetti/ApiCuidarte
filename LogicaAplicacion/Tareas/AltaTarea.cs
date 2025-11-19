using LogicaAplicacion.Dtos.Clientes;
using LogicaAplicacion.Dtos.MapeosDto;
using LogicaAplicacion.Dtos.Tareas;
using LogicaAplicacion.Dtos.TipoPlanes;
using LogicaNegocio.Entidades;
using LogicaNegocio.InterfacesRepocitorio;
using LogicaNegocio.InterfazServicios;
namespace LogicaAplicacion.Tareas
{
	public class AltaTarea : IAlta<TareaDto>
	{
		private readonly IRepositorioTarea _context;
		private readonly IRepositorioCliente _contextClient;
		private readonly IRepositorioUsuario _contextFuncionario;
		private readonly IRepositorioTipoServicio _contextTipoServicio;
		public AltaTarea(IRepositorioTarea context, IRepositorioCliente contextClient, IRepositorioUsuario contextFuncionario, IRepositorioTipoServicio contextTipoServicio)
		{
			_context = context;
			_contextClient = contextClient;
			_contextFuncionario = contextFuncionario;
			_contextTipoServicio = contextTipoServicio;
		}
		public TareaDto Ejecutar(TareaDto obj)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("El tipo de objeto esta vacio");
			}

			Tarea t = TareaMapper.FromDto(obj);
			Cliente c = _contextClient.GetById(obj.clienteId);
			if(c.Suscripcion.ProximoCobro < DateOnly.FromDateTime(DateTime.Now))

			{
				throw new InvalidOperationException("El cliente no tiene una suscripcion activa");
			}
			Usuario u = _contextFuncionario.GetById(obj.responsableId);
			t.Cliente = c;
			t.EmpleadoResponsable = u;
			if (obj.servicios != null)
			{
				foreach (ServicioDto s in obj.servicios)
				{
					TipoServicio ts = _contextTipoServicio.GetById(s.tipoServicio.id);
					Servicio servicio = new Servicio(0, ts, s.cantServicios);
					t.serviciosUsados.Add(servicio);
				}
			}
			Tarea tCreada = _context.Add(t);
			TareaDto tDto = TareaMapper.ToDto(tCreada);
			return tDto;
		}
	}
}
