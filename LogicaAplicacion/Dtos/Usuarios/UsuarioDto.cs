namespace LogicaAplicacion.Dtos.Usuarios
{
	public record UsuarioDto(int Id, string Email, string Nombre, string Apellido, string password, string Discriminador,bool Eliminado, string? token)
	{
	}
}
