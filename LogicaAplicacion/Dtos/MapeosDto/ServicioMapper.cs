using LogicaAplicacion.Dtos.Clientes;
using LogicaAplicacion.Dtos.Tareas;
using LogicaAplicacion.Dtos.TipoPlanes;
using LogicaAplicacion.Dtos.Usuarios;
using LogicaNegocio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.Dtos.MapeosDto
{
	public class ServicioMapper
	{
		public static	ServicioDto ToDto(Servicio servicio)
		{
			var servicioDto = new ServicioDto(servicio.Id,new TiposServicioDto(servicio.tipoServicio.Id,servicio.tipoServicio.Nombre,servicio.tipoServicio.PrecioHora), servicio.cantServicios);

			return servicioDto;
		}

		public static IEnumerable<ServicioDto> ToListaDto(IEnumerable<Servicio> servicios)
		{
			List<ServicioDto> aux = new List<ServicioDto>();
			foreach (var servicio in servicios)
			{
				ServicioDto servicioDto = ServicioMapper.ToDto(servicio);
				aux.Add(servicioDto);
			}
			return aux;
		}

	}
}
