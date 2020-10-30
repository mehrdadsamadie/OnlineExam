using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Examination.Domain.Entities;

namespace Examination.Domain.Abstract
{
    public interface IExamRepository
    {
        IQueryable<Exam> AllExams();
        IQueryable<Exam> AllActiveExams();
        void AddExam(Exam exam);
        Exam FindExam(int? id);
        void EditExam(Exam exam);
        void DeleteExam(Exam exam);
        void ActiveExam(int? id);
        void RemoveExam(Exam exam);
    }
}
