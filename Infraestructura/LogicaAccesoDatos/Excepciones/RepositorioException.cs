
namespace Infraestructura.LogicaAccesoDatos.Excepciones
{
    public class RepositorioException : Exception
    {
        public RepositorioException() { }
        public RepositorioException(string message) : base(message) { }
    }
}
