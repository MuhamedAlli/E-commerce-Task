using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssignmentForFullStack.Core.Interfaces
{
    public interface IBaseRepository<T>  where T : class
    {
        T GetById(int id);
        T GetByCode(string id);
        IEnumerable<T> GetAll(string[] includes = null);
        IEnumerable<T> FindAll(Func<T,bool> creteria);

        T Add(T entity);
        T Update(T entity);
        T Delete(T entity);
    }
}
