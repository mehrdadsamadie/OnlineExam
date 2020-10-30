using Examination.Domain.Abstract;
using Examination.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examination.Domain.Concrete
{
    public class UserScoreRepository : IUserScoreRepository
    {
        ExaminationEntities Context = new ExaminationEntities();
        public int GetNumQuestion(int examId, string userId)
        {
           var numquestion= Context.UserScores.Where(x => x.Question.ExamId ==examId &&x.UserId==userId && x.IsFinish==false).Count();
           return numquestion;
        }


        public decimal? GetExamUserScore(int examId, string userId)
        {
            var score = Context.UserScores.Where(x => x.Question.ExamId == examId && x.UserId == userId && x.IsFinish == true).ToList();
            if (score!=null)
               return score.Sum(x=>x.Grade);
            return null;
        }


        public void AddUserScore(UserScore userScore)
        {
            Context.UserScores.Add(userScore);
            Context.SaveChanges();
        }

        public void EditUserScore(UserScore userscore)
        {
            Context.Entry(userscore).State = EntityState.Modified;
            Context.SaveChanges();
        }

        public UserScore GetUserScore(int questionId, string userId)
        {
           var userscore= Context.UserScores.FirstOrDefault(x => x.QuestionId == questionId && x.UserId== userId);
           return userscore;
        }


        public IQueryable<UserScore> GetExamQuestionSecore(int examId, string userId)
        {
            var _userscores = Context.UserScores.Include(x=>x.Answer).Include(x=>x.Question).Include(x=>x.User).Where(x => x.Question.ExamId == examId && x.UserId == userId && x.IsFinish == true);
            return _userscores;
        }


        public IQueryable<UserScore> GetAllExamsScore()
        {
            var _userscores = Context.UserScores.Include(x => x.Answer).Include(x => x.Question).Include(x=>x.User).Where(x=>x.IsFinish == true);
            return _userscores;
        }
       
    }
}
