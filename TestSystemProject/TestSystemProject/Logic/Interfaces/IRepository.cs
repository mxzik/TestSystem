using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSystemProject.Logic.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();

        T GetById(int id);

        T GetByEmail(string email);

        T GetByName(string name);

        void Create(T entity);

        void Update(T entity);

        void Delete(T entity);
    }
}
