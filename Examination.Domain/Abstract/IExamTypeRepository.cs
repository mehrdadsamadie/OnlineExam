using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Examination.Domain.Entities;

namespace Examination.Domain.Abstract
{
    public interface IExamTypeRepository
    {
        IQueryable<ExamType> AllExamTypes();
        void Add(ExamType examType);

    }
}
