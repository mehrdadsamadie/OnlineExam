using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Examination.Domain.Entities;
using Examination.Domain.Abstract;

namespace Examination.WebUI.Controllers
{
    [Authorize(Roles = "Admin,Teacher")]
    public class GuideController : Controller
    {
        private IGuideRepository iGuideRepository;
        private IQuestionRepository iQuestionRepository;
        public GuideController(IGuideRepository iguiderepository, IQuestionRepository iquestionrepository)
        {
            iGuideRepository = iguiderepository;
            iQuestionRepository = iquestionrepository;
        }

        // GET: /Guide/
        public ActionResult Index(int id)
        {
            var _question = iQuestionRepository.FindQuesstion(id);
            ViewBag.QuestionText = _question.Text;
            Session["QuestionId"] = null;
            ViewBag.ExamId = iQuestionRepository.FindQuesstion(id).ExamId;
            var guides = iGuideRepository.AllGuides(id).Include(g => g.Question).OrderBy(a => a.Order);
            Session["QuestionId"] = id;
            return View(guides.ToList());
        }

        // GET: /Guide/Details/5
        public ActionResult Details(int id)
        {

            Guide guide = iGuideRepository.FindGuide(id);
            if (guide == null)
            {
                return HttpNotFound();
            }
            return View(guide);
        }

        // GET: /Guide/Create
        public ActionResult Create()
        {
            if (Session["QuestionId"].ToString() != null)
            {
                var _questionId = int.Parse(Session["QuestionId"].ToString());
                ViewBag.QuestionText = iQuestionRepository.FindQuesstion(_questionId).Text;
            }
            var guide = new Guide();
            guide.Order = 0;
            return View(guide);
        }

        // POST: /Guide/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Guide guide)
        {
            ModelState.Remove("QuestionId");
            ModelState.Remove("GuideId");
            if (ModelState.IsValid)
            {
                if (Session["QuestionId"].ToString() != null)
                {
                    guide.QuestionId = int.Parse(Session["QuestionId"].ToString());
                    iGuideRepository.AddGuide(guide);
                    return RedirectToAction("Index", new { id = guide.QuestionId });
                }
                else
                    return View(guide);
            }
            else
                return View(guide);
        }

        // GET: /Guide/Edit/5
        public ActionResult Edit(int id)
        {
            Guide guide = iGuideRepository.FindGuide(id);
            if (guide == null)
            {
                return HttpNotFound();
            }
            return View(guide);
        }

        // POST: /Guide/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Guide guide)
        {
            if (ModelState.IsValid)
            {
                iGuideRepository.EditGuide(guide);
                return RedirectToAction("Index", new { id = guide.QuestionId });
            }
            else
                return View(guide);

        }

        // GET: /Guide/Delete/5
        public ActionResult Delete(int id)
        {

            Guide guide = iGuideRepository.FindGuide(id);
            if (guide == null)
            {
                return HttpNotFound();
            }
            return View(guide);
        }

        // POST: /Guide/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var hint = iGuideRepository.FindGuide(id);
            iGuideRepository.RemoveGuide(id);
            return RedirectToAction("Index", new { id=hint.QuestionId});
        }

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
