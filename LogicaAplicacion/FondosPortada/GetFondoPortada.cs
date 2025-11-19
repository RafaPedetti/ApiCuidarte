using LogicaAplicacion.Dtos;
using LogicaNegocio.Entidades;
using LogicaNegocio.InterfacesRepocitorio;
using LogicaNegocio.InterfazServicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.FondosPortada
{
	public class GetFondoPortada : IObtener<FondoPortadaDto>
	{
		private readonly IRepositorioFondoPortada _context;
		public GetFondoPortada(IRepositorioFondoPortada context)
		{
			_context = context;
		}
		public FondoPortadaDto Ejecutar(int i)
		{
			FondoPortada fondo = _context.GetById(i);
			if (fondo == null)
				throw new ArgumentException("No se encontró el fondo de portada");
			return new FondoPortadaDto(fondo.Url);
		}
	}
}
