using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestSystemProject.Entities;
using TestSystemProject.Logic.Interfaces;

namespace TestSystemProject.Logic.Services
{
    class UnitOfWork : IDisposable
    {
        private readonly DatabaseContext _context = new DatabaseContext();
        private AnswerService answerService;
        private QuestionService questionService;

        public AnswerService Answer
        {
            get
            {
                if (answerService == null)
                    answerService = new AnswerService();
                return answerService;
            }
        }

        public QuestionService Orders
        {
            get
            {
                if (questionService == null)
                    questionService = new QuestionService();
                return questionService;
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
