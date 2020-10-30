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
    public class QuestionNewController : Controller
    {
        private IQuestionRepository iQuestionRepository;
        private IExamRepository IExamRepository;
        public QuestionNewController(IQuestionRepository iquestionrepository, IExamRepository iexamrepository)
        {
            this.iQuestionRepository = iquestionrepository;
            this.IExamRepository = iexamrepository;

        }

        // GET: /QuestionNew/
        public ActionResult Index(int id)
        {
            Session["ExamId"] = null;
            var questions = iQuestionRepository.AllQuestions(id).Include(q => q.Exam).OrderBy(a => a.Order);
            var _exam = IExamRepository.FindExam(id);
            ViewBag.ExamName = _exam.Name;
            Session["ExamId"] = id;
            return View(questions.ToList());
        }

         //GET: /QuestionNew/Details/5
          public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var _question = iQuestionRepository.FindQuesstion(id.Value);
            if (_question == null)
            {
                return HttpNotFound();
            }
            return View(_question);
        }

        // GET: /QuestionNew/Create
        public ActionResult Create()
        {
            if (Session["ExamId"].ToString() != null)
            {
                var examid= int.Parse(Session["ExamId"].ToString());
                ViewBag.ExamName = IExamRepository.FindExam(examid).Name;
            }
            var question = new Question();
            question.Order = 0;
            return View(question);
        }

        // POST: /QuestionNew/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Question question)
        {
            ModelState.Remove("ExamId");
            if (ModelState.IsValid)
            {
                if (Session["ExamId"].ToString() != null)
                {
                    question.ExamId = int.Parse(Session["ExamId"].ToString());
                    iQuestionRepository.AddQuestion(question);
                    return RedirectToAction("Index", new { id = question.ExamId });
                }
                else return View(question);
            }
            else return View(question);
        }

        // GET: /QuestionNew/Edit/5
        public ActionResult Edit(int id)
        {
            Question question = iQuestionRepository.FindQuesstion(id);
            if (question == null)
            {
                return HttpNotFound();
            }
            return View(question);
        }

        //        // POST: /QuestionNew/Edit/5
        //        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Question question)
        {
            if (ModelState.IsValid)
            {
                iQuestionRepository.EditQuestion(question);
                return RedirectToAction("Index", new { id = question.ExamId });
            }
            return View(question);
        }

        //        // GET: /QuestionNew/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var _question = iQuestionRepository.FindQuesstion(id.Value);
            if (_question == null)
            {
                return HttpNotFound();
            }
            return View(_question);
        }

        // POST: /QuestionNew/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            iQuestionRepository.DeleteQuestion(id);
            return RedirectToAction("Index");
        }
    }
}
