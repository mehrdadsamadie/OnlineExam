using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Examination.Domain.Abstract;
using Examination.Domain.Entities;

namespace Examination.Domain.Concrete
{
   public class AnswerRepository:IAnswerRepository
    {
       ExaminationEntities Context=new ExaminationEntities();
        public IQueryable<Answer> AllAnswer(int questionId)
        {
            return Context.Answers.Where(x => x.QuestionId == questionId).OrderBy(x=>x.Order);
        }

        public void AddAnswer(Answer answer)
        {
            Context.Answers.Add(answer);
            Context.SaveChanges();
        }

        public void EditAnswer(Answer answer)
        {
            Context.Answers.Attach(answer);
            Context.Entry(answer).State=EntityState.Modified;
            Context.SaveChanges();
        }

        public void DeleteAnswer(int answerId)
        {
            var answer = FindAnswer(answerId);
            Context.Answers.Remove(answer);
            Context.SaveChanges();
        }

        public Answer FindAnswer(int answerId)
        {
            return Context.Answers.FirstOrDefault(x => x.AnswerId == answerId);
        }
    }
}
