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
    public class GuideRepository:IGuideRepository
    {
        ExaminationEntities Context =new ExaminationEntities();

        public IQueryable<Guide> AllGuides(int questionId)
        {
            return Context.Guides.Where(x => x.QuestionId == questionId).OrderBy(x=>x.Order);
        }

        public void AddGuide(Guide guide)
        {
            Context.Guides.Add(guide);
            Context.SaveChanges();
        }

        public void EditGuide(Guide guide)
        {
            Context.Guides.Attach(guide);
            Context.Entry(guide).State=EntityState.Modified;
            Context.SaveChanges();
        }

        public Guide FindGuide(int guideId)
        {
           return Context.Guides.Include(x=>x.Question).FirstOrDefault(x => x.GuideId == guideId);
        }

        public void RemoveGuide(int guideId)
        {
            var guide = FindGuide(guideId);
            Context.Guides.Remove(guide);
            Context.SaveChanges();
        }
    }
}
