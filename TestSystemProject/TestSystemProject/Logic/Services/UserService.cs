using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestSystemProject.Entities;
using TestSystemProject.Logic.Interfaces;

namespace TestSystemProject.Logic.Services
{
    public class UserService : IRepository<User>
    {
        private readonly DatabaseContext _context = new DatabaseContext();        

        //при регистрации
        public void Create(User entity)
        {
            _context.Users.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(User entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetAll()
        {
            return _context.Users;
        }

        public User GetByEmail(string email)
        {
            return _context.Users.Where(x => x.Email == email).FirstOrDefault();
        }

        public User GetById(int id)
        {
            return _context.Users.Where(x => x.UserId == id).FirstOrDefault();

        }

        public User GetByName(string name)
        {
            throw new NotImplementedException();
        }

        public void Update(User entity)
        {
            throw new NotImplementedException();
        }
    }
}
