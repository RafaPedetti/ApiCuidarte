using LogicaAplicacion.Dtos.Clientes;
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
	public class TipoPlanMapper
	{
		public static TipoPlan FromDto(TipoPlanDto tpDto)
		{
			var plan = new TipoPlan
			{
				Id = tpDto.id,
				Nombre = tpDto.nombre,
				Precio = tpDto.precio,
			};
			return plan;
		}

		public static TipoPlanDto ToDto(TipoPlan tp)
		{
			TipoPlanDto tpDto;
			if (tp.EmpresaId == null || tp.Empresa == null)
			{
				tpDto = new TipoPlanDto(tp.Id, tp.Nombre, ServicioMapper.ToListaDto(tp.Servicios).ToList(),null, tp.Precio);
			}
			else
			{
				tpDto = new TipoPlanDto(tp.Id, tp.Nombre, ServicioMapper.ToListaDto(tp.Servicios).ToList(), tp.Empresa.Id, tp.Precio);
			}
			return tpDto;
		}

		public static IEnumerable<TipoPlanDto> ToListaDto(IEnumerable<TipoPlan> tipoPlanes)
		{
			List<TipoPlanDto> aux = new List<TipoPlanDto>();
			foreach (var tipoPlan in tipoPlanes)
			{
				TipoPlanDto tipoPlanDto = TipoPlanMapper.ToDto(tipoPlan);
				aux.Add(tipoPlanDto);
			}
			return aux;
		}
	}
}
