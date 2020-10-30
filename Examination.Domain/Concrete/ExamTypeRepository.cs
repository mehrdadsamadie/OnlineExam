using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Examination.Domain.Abstract;
using Examination.Domain.Entities;

namespace Examination.Domain.Concrete
{
    public class ExamTypeRepository:IExamTypeRepository
    {
        ExaminationEntities ContextEntities =new ExaminationEntities();
        public void Add(Entities.ExamType examtype)
        {
            ContextEntities.ExamTypes.Add(examtype);
            ContextEntities.SaveChanges();
        }


        public IQueryable<ExamType> AllExamTypes()
        {
            return ContextEntities.ExamTypes;
        }
    }
}
