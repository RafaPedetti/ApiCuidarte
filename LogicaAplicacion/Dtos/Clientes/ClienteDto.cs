using LogicaAplicacion.Dtos.Suscripciones;
using LogicaAplicacion.Dtos.TipoPlanes;
namespace LogicaAplicacion.Dtos.Clientes
{
	public record ClienteDto (int id,DateTime fecha,string nombre, string apellido,string ci,string Email, DateTime fechaNacimiento, string direccion, string telefono,string celular, string responsablePago, string formaPago, string observaciones, int TipoPlanId,SuscripcionDto? suscripcion,IEnumerable<ServicioDto> serviciosDisponibles)
	{
	}
}
