using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Examination.Domain.Entities;

namespace Examination.Domain.Abstract
{
    public interface IGuideRepository
    {
        IQueryable<Guide> AllGuides(int questionId);
        void AddGuide(Guide guide);
        void EditGuide(Guide guide);
        Guide FindGuide(int guideId);
        void RemoveGuide(int guideId);
    }
}
