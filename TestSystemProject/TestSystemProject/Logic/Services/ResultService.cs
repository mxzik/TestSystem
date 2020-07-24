using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestSystemProject.Entities;
using TestSystemProject.Logic.Interfaces;

namespace TestSystemProject.Logic.Services
{
    class ResultService 
    {
        private readonly DatabaseContext _context = new DatabaseContext();

        public void Create(Result entity)
        {
            _context.Results.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(Result entity)
        {
            _context.Results.Remove(entity);
            _context.SaveChanges();
        }
        public Result GetByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Result> GetAll()
        {
            return _context.Results;
        }

        public Result GetById(int id)
        {
            return _context.Results.Where(x => x.ResultId == id).FirstOrDefault();
        }

        public void Update(Result entity)
        {
            Result result = _context.Results.Where(x => x.ResultId == entity.ResultId).FirstOrDefault();

            if (result != null)
            {
                result.TotalScore = entity.TotalScore;
                result.TestId = entity.TestId;
                result.UserId = entity.UserId;
                _context.SaveChanges();
            }
        }
    }
}
