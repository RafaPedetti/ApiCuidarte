using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.InterfazServicios
{
    public interface IEditar<TDto, TRespuesta>
    {
        public TRespuesta Ejecutar( TDto obj);
    }
}
