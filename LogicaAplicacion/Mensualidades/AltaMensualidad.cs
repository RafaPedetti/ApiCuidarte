using LogicaAplicacion.Dtos.Clientes;
using LogicaAplicacion.Dtos.MapeosDto;
using LogicaAplicacion.Dtos.Suscripciones;
using LogicaNegocio.Entidades;
using LogicaNegocio.InterfacesRepocitorio;
using LogicaNegocio.InterfazServicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.Mensualidades
{
	public class AltaMensualidad : IAlta<MensualidadDto>
	{
		private readonly IRepositorioMensualidad _context;
		private readonly IRepositorioTarea _contexTarea;
		private readonly IRepositorioSuscripcion repositorioSuscripcion;
		public AltaMensualidad(IRepositorioMensualidad context)
		{
			_context = context;
		}
		public MensualidadDto Ejecutar(MensualidadDto obj)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("El tipo de objeto esta vacio");
			}
			decimal precio = obj.precio;
			if(obj.clienteId != null)
			{
				Suscripcion suscripcion = repositorioSuscripcion.GetById(obj.suscripcionId);
				IEnumerable<Tarea> tareas = _contexTarea.GetTareasByCliente((int)obj.clienteId);
				if(tareas.Count() == 0)
				{
					precio = suscripcion.Plan.PrecioConDescuentoNoUso;
				}
			}
			var objConPrecio = obj with { precio = precio };
			Mensualidad m = _context.Add(MensualidadMapper.FromDto(objConPrecio));
			return MensualidadMapper.ToDto(m);
		}
	}
}
