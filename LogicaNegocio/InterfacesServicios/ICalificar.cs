

using LogicaNegocio.ValueObject.Tarea;

namespace LogicaNegocio.InterfacesServicios
{
	public interface ICalificar<T>
	{
		public T Ejecutar(T calificacion);
	}
}
