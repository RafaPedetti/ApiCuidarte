using LogicaAplicacion.Dtos.Clientes;
using LogicaAplicacion.Dtos.MapeosDto;
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
	public class AltaMensualidad : IAlta<Mensualidad, Mensualidad>
	{
		private readonly IRepositorioMensualidad _context;
		public AltaMensualidad(IRepositorioMensualidad context)
		{
			_context = context;
		}
		public Mensualidad Ejecutar(Mensualidad obj)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("El tipo de objeto esta vacio");
			}
			return _context.Add(obj);
		}
	}
}
