using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.InterfazRepositorio
{
    public interface IRepositorio <T>
    {
        public T Add(T obj);
        public void Delete(int id);
        public T Update(T obj);
        public T GetById(int id);
		public IEnumerable<T> GetAll();

    }
}
