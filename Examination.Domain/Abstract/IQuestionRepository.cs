using Examination.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examination.Domain.Abstract
{
    public interface IQuestionRepository
    {
        IQueryable<Question> AllQuestions(int examId);
        IQueryable<Question> GetQuestionNotAnswerd(int examId, string userId);
        IQueryable<Question> GetQuestionAnswerd(int examId, string userId);
        void AddQuestion(Question question);
        Question FindQuesstion(int questionId);
        void EditQuestion(Question question);
        void DeleteQuestion(int questionId);
        int TotalQuestion(int examId);
        int TotalQuestionAnswered(int examId, string userId);

    }
}
