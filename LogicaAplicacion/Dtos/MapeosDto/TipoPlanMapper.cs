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
			TipoPlan plan = new TipoPlan(tpDto.id,tpDto.nombre, tpDto.precio,tpDto.precioConDescuento ,tpDto.destino);
			return plan;
		}

		public static TipoPlanDto ToDto(TipoPlan tp)
		{
			TipoPlanDto tpDto;
			if (tp.EmpresaId == null || tp.Empresa == null)
			{
				tpDto = new TipoPlanDto(tp.Id, tp.Nombre, ServicioMapper.ToListaDto(tp.Servicios).ToList(),null, tp.Precio, tp.PrecioConDescuentoNoUso, tp.Destino);
			}
			else
			{
				tpDto = new TipoPlanDto(tp.Id, tp.Nombre, ServicioMapper.ToListaDto(tp.Servicios).ToList(), tp.Empresa.Id, tp.Precio, tp.PrecioConDescuentoNoUso, tp.Destino);
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
