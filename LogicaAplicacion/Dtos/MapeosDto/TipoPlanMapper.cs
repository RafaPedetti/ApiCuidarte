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
			};
			return plan;
		}

		public static IEnumerable<UsuarioDto> ToListaDto(IEnumerable<Usuario> usuarios)
		{
			List<UsuarioDto> aux = new List<UsuarioDto>();
			foreach (var user in usuarios)
			{
				UsuarioDto userDto = UsuarioMapper.ToDto(user);
				aux.Add(userDto);
			}
			return aux;
		}
	}
}
