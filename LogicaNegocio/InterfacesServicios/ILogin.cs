using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.InterfazServicios
{
    public interface ILogin<T>
    {
        public T Ejecutar(string Email, string Password);
    }
}
