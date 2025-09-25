
using LogicaAplicacion.Dtos.Empresas;
using LogicaNegocio.Entidades;

namespace LogicaAplicacion.Dtos.MapeosDto
{
	public class EmpresaMapper
	{
		public static Empresa FromDto(EmpresaDto eDto)
		{
			Empresa empresa;
			if(eDto.suscripcion != null) empresa = new Empresa(eDto.id, eDto.nombre, eDto.TipoPlanId, eDto.telefonoContacto, SuscripcionMapper.FromDto(eDto.suscripcion));
			else empresa = new Empresa(eDto.id, eDto.nombre, eDto.TipoPlanId, eDto.telefonoContacto);
			return empresa;
		}

		public static EmpresaDto ToDto(Empresa empresa)
		{
			var empresaDto = new EmpresaDto(empresa.Id, empresa.Nombre, empresa.TelefonoContacto.Value, empresa.TipoPlanId, SuscripcionMapper.ToDto(empresa.Suscripcion) ?? null);
			return empresaDto;
		}

		public static IEnumerable<EmpresaDto> ToListaDto(IEnumerable<Empresa> empresas)
		{
			List<EmpresaDto> aux = new List<EmpresaDto>();
			foreach (var empresa in empresas)
			{
				EmpresaDto empresaDto = EmpresaMapper.ToDto(empresa);
				aux.Add(empresaDto);
			}
			return aux;
		}
	}
}
