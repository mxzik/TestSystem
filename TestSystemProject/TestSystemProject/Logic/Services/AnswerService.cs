using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestSystemProject.Entities;
using TestSystemProject.Logic.Interfaces;

namespace TestSystemProject.Logic.Services
{
    public class AnswerService : IRepository<Answer>
    {

        private readonly DatabaseContext _context = new DatabaseContext();

        public void Create(Answer entity)
        {
            _context.Answers.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(Answer entity)
        {
            _context.Answers.Remove(entity);
            _context.SaveChanges();
        }

        public IEnumerable<Answer> GetAll()
        {
            return _context.Answers;
        }

        public Answer GetByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public Answer GetById(int id)
        {
            return _context.Answers.Where(x => x.AnswerId == id).FirstOrDefault();
        }

        public Answer GetByName(string name)
        {
            return _context.Answers.Where(x => x.Text == name).FirstOrDefault();
        }

        public void Update(Answer entity)
        {
            Answer answer = _context.Answers.Where(x => x.AnswerId == entity.AnswerId).FirstOrDefault();

            if(answer != null)
            {
                answer.Text = entity.Text;
                answer.IsRight = entity.IsRight;
                answer.QuestionId = entity.QuestionId;
                _context.SaveChanges();
            }
        }
    }
}
