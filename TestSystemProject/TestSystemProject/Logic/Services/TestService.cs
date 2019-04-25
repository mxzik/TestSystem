using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestSystemProject.Entities;
using TestSystemProject.Logic.Interfaces;

namespace TestSystemProject.Logic.Services
{
    public class TestService : IRepository<Test>
    {
        private readonly DatabaseContext _context = new DatabaseContext();

        public void Create(Test entity)
        {
            _context.Tests.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(Test entity)
        {
            _context.Tests.Remove(entity);
            _context.SaveChanges();
        }

        public IEnumerable<Test> GetAll()
        {
            return _context.Tests;
        }

        public Test GetByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public Test GetById(int id)
        {
            return _context.Tests.Where(x => x.TestId == id).FirstOrDefault();
        }

        public Test GetByName(string name)
        {
            return _context.Tests.Where(x => x.Name == name).FirstOrDefault();
        }

        public void Update(Test entity)
        {
            Test test = _context.Tests.Where(x => x.TestId == entity.TestId).FirstOrDefault();

            if(test != null)
            {
                test.Name = entity.Name;
                test.ThemeId = entity.ThemeId;
                _context.SaveChanges();
            }
        }
    }
}
