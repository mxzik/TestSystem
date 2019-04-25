using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestSystemProject.Entities;
using TestSystemProject.Logic.Interfaces;

namespace TestSystemProject.Logic.Services
{
    public class ThemeService : IRepository<Theme>
    {
        private readonly DatabaseContext _context = new DatabaseContext();

        public void Create(Theme entity)
        {
            _context.Themes.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(Theme entity)
        {
            _context.Themes.Remove(entity);
            _context.SaveChanges();
        }

        public IEnumerable<Theme> GetAll()
        {
            return _context.Themes;
        }

        public Theme GetByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public Theme GetById(int id)
        {
            return _context.Themes.Where(x => x.ThemeId == id).FirstOrDefault();
        }

        public Theme GetByName(string name)
        {
            return _context.Themes.Where(x => x.Name == name).FirstOrDefault();
        }

        public void Update(Theme entity)
        {
            Theme updateTheme = _context.Themes.Where(x => x.ThemeId == entity.ThemeId).FirstOrDefault();

            if (updateTheme != null)
            {
                updateTheme.Name = entity.Name;
                updateTheme.Description = entity.Description;
                _context.SaveChanges();
            }
        }
    }
}
