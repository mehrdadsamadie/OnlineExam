using Examination.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examination.Domain.Abstract
{
    public interface IUserScoreRepository
    {
        int GetNumQuestion(int examId, string userId);
        decimal? GetExamUserScore(int examId, string userId);
        void AddUserScore(UserScore userScore);
        void EditUserScore(UserScore userScore);
        UserScore GetUserScore(int questionId, string userId);
        IQueryable<UserScore> GetExamQuestionSecore(int examId, string userId);
        IQueryable<UserScore> GetAllExamsScore();
    }
}
