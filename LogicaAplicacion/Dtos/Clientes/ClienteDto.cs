using LogicaAplicacion.Dtos.Suscripciones;
namespace LogicaAplicacion.Dtos.Clientes
{
	public record ClienteDto (int id,string nombre, string apellido,string ci,string Email, DateTime fechaNacimiento, string direccion, string telefono, int TipoPlanId,SuscripcionDto? suscripcion)
	{
	}
}
