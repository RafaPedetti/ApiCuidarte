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
			
			Mensualidad m =_context.Add(MensualidadMapper.FromDto(obj));
			return MensualidadMapper.ToDto(m);
		}
	}
}
