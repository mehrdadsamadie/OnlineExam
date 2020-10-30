using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Examination.Domain.Abstract;
using Examination.Domain.Entities;
using System.Data.Entity;

namespace Examination.Domain.Concrete
{
    public class ExamRepository : IExamRepository
    {
        ExaminationEntities Context = new ExaminationEntities();
        public IQueryable<Exam> AllExams()
        {
            return Context.Exams.Where(x => x.IsDelete == false);

        }


        public void AddExam(Exam exam)
        {
            Context.Exams.Add(exam);
            Context.SaveChanges();
        }


        public Exam FindExam(int? id)
        {
            if (id == null)
                return null;
            return Context.Exams.FirstOrDefault(x => x.ExamId == id);
        }


        public void EditExam(Exam exam)
        {
            Context.Exams.Attach(exam);
            Context.Entry(exam).State = EntityState.Modified;
            Context.SaveChanges();
        }


        public void DeleteExam(Exam exam)
        {
            exam.IsDelete = true;
            Context.Entry(exam).State = EntityState.Modified;
            Context.SaveChanges();
        }



        public void RemoveExam(Exam exam)
        {
            Context.Exams.Remove(exam);
            Context.SaveChanges();
        }


        public void ActiveExam(int? id)
        {
            if (id != null)
            {
                var exam = Context.Exams.FirstOrDefault(x => x.ExamId == id);
                if (exam != null)
                {
                    exam.IsActive = !exam.IsActive;
                    Context.Entry(exam).State = EntityState.Modified;
                    Context.SaveChanges();
                }
            }
        }


        public IQueryable<Exam> AllActiveExams()
        {
            return Context.Exams.Where(x => x.IsActive == true && x.IsDelete == false && ((x.StartDate <= DateTime.Today && DateTime.Today <= x.EndDate) || (x.StartDate == null && x.EndDate == null) || (x.StartDate <= DateTime.Today && x.EndDate == null)));
        }
    }
}
