using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestSystemProject.Entities;
using TestSystemProject.Logic.Interfaces;

namespace TestSystemProject.Logic.Services
{
    public class QuestionService : IRepository<Question>
    {
        private readonly DatabaseContext _context = new DatabaseContext();

        public void Create(Question entity)
        {
            _context.Questions.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(Question entity)
        {
            _context.Questions.Remove(entity);
            _context.SaveChanges();
        }

        public IEnumerable<Question> GetAll()
        {
            return _context.Questions;
        }

        public Question GetByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public Question GetById(int id)
        {
            return _context.Questions.Where(x => x.QuestionId == id).FirstOrDefault();
        }

        public Question GetByName(string name)
        {
            return _context.Questions.Where(x => x.Text == name).FirstOrDefault();
        }

        public void Update(Question entity)
        {
            Question question = _context.Questions.Where(x => x.QuestionId == entity.QuestionId).FirstOrDefault();

            if(question != null)
            {
                question.Text = entity.Text;
                question.Score = entity.Score;
                question.TestId = entity.TestId;
                _context.SaveChanges();
            }
        }
    }
}
