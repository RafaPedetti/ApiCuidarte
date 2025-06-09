using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.InterfazRepositorio
{
    public interface IRepositorio <T>
    {
        public void Add(T obj);
        public void Delete(int id);
        public void Update(int id, T obj);
        public T GetById(int id);
        public IEnumerable<T> GetAll();
    }
}
