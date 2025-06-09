
namespace Infraestructura.LogicaAccesoDatos.Excepciones
{
    public class NotFoundException : RepositorioException
    {
        public NotFoundException(): base("No se encontrò la informaciòn solicitada.") { }

        public NotFoundException(string message) : base(message) { }

    }
}
