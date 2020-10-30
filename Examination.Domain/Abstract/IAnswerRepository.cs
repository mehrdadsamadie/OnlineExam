using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Examination.Domain.Entities;

namespace Examination.Domain.Abstract
{
    public interface IAnswerRepository
    {
        IQueryable<Answer> AllAnswer(int questionId);
        void AddAnswer(Answer answer);
        void EditAnswer(Answer answer);
        void DeleteAnswer(int answerId);
        Answer FindAnswer(int answerId);
    }
}
