using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Mime;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Examination.Domain.Abstract;
using Examination.Domain.Entities;

namespace Examination.Domain.Concrete
{
    public class QuestionRepository:IQuestionRepository
    {
        ExaminationEntities Context = new ExaminationEntities();
        public IQueryable<Question> AllQuestions(int examId)
        {
           return Context.Questions.Where(x => x.ExamId == examId).OrderByDescending(x=>x.Order);
        }

        public void AddQuestion(Question question)
        {
            Context.Questions.Add(question);
            Context.SaveChanges();
        }

        public Question FindQuesstion(int questionId)
        {
            return Context.Questions.FirstOrDefault(x => x.QuestionId == questionId);
        }

        public void EditQuestion(Question question)
        {
            Context.Questions.Attach(question);
            Context.Entry(question).State = EntityState.Modified;
            Context.SaveChanges();
        }


        public void DeleteQuestion(int questionId)
        {
            var question = FindQuesstion(questionId);
            Context.Questions.Remove(question);
            Context.SaveChanges();
        }


        public IQueryable<Question> GetQuestionNotAnswerd(int examId,string userId)
        {
            var questions = Context.Questions.Where(x => x.ExamId == examId && !Context.UserScores.Where(y => y.UserId == userId && y.IsFinish==true).Select(y => y.QuestionId).Contains(x.QuestionId)).OrderBy(x => x.Order);
            return questions;
        }

        public int TotalQuestion(int examId)
        {
            var _total = Context.Questions.Where(x => x.ExamId == examId).Count();
            return _total;
        }

        public int TotalQuestionAnswered(int examId, string userId)
        {
            var _total = Context.Questions.Where(x => x.ExamId == examId && Context.UserScores.Where(y => y.UserId == userId && y.IsFinish == true).Select(y => y.QuestionId).Contains(x.QuestionId)).OrderBy(x => x.Order).Count();
            return _total;

        }


        public IQueryable<Question> GetQuestionAnswerd(int examId, string userId)
        {
            var questions = Context.Questions.Where(x => x.ExamId == examId && Context.UserScores.Where(y => y.UserId == userId && y.IsFinish == true).Select(y => y.QuestionId).Contains(x.QuestionId)).OrderByDescending(x => x.Order);
            return questions;            
        }
    }
}
