using LogicaNegocio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.InterfazServicios
{
    public interface IFindByMonto<T>
    {
        public IEnumerable<Cliente> Ejecutar(double monto);
    }
}
