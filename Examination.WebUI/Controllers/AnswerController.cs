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
    public class AnswerController : Controller
    {
        private IAnswerRepository iAnswerRepository;
        private IQuestionRepository iQuestionRepository;
        public AnswerController(IAnswerRepository ianswerrepository, IQuestionRepository iquestionrepository)
        {
            this.iAnswerRepository = ianswerrepository;
            this.iQuestionRepository = iquestionrepository;
        }

        // GET: /Answer/
        public ActionResult Index(int id)
        {
            Session["QuestionId"] = null;

            var _question = iQuestionRepository.FindQuesstion(id);
            ViewBag.ExamId = _question.ExamId;
            ViewBag.QuestionText = _question.Text;
            var answers = iAnswerRepository.AllAnswer(id).Include(a => a.Question).OrderBy(x => x.Order);
            Session["QuestionId"] = id;
            return View(answers.ToList());
        }

        //// GET: /Answer/Details/5
        public ActionResult Details(int id)
        {

            Answer answer = iAnswerRepository.FindAnswer(id);
            if (answer == null)
            {
                return HttpNotFound();
            }
            return View(answer);
        }

        // GET: /Answer/Create
        public ActionResult Create()

        {
            if (Session["QuestionId"].ToString() != null)
            {
                var _questionId = int.Parse(Session["QuestionId"].ToString());
                ViewBag.QuestionText = iQuestionRepository.FindQuesstion(_questionId).Text;
            }
            var answer = new Answer();
            answer.Order = 0;
            return View(answer);
        }

        // POST: /Answer/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Answer answer)
        {
            ModelState.Remove("QuestionId");
            ModelState.Remove("AnswerId");
            if (Session["QuestionId"].ToString() != null)
            {
                answer.QuestionId = int.Parse(Session["QuestionId"].ToString());
                if (ModelState.IsValid)
                {
                    iAnswerRepository.AddAnswer(answer);
                    return RedirectToAction("Index", new { id = answer.QuestionId });
                }
                else return View(answer);
            }
            else
            {
                return View(answer);
            }
        }

        //// GET: /Answer/Edit/5
        public ActionResult Edit(int id)
        {
            Answer answer = iAnswerRepository.FindAnswer(id);
            if (answer == null)
            {
                return HttpNotFound();
            }
            return View(answer);
        }

        //// POST: /Answer/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Answer answer)
        {
            if (ModelState.IsValid)
            {
                iAnswerRepository.EditAnswer(answer);
                return RedirectToAction("Index", new { @id = answer.QuestionId });
            }
            return View(answer);
        }

        //// GET: /Answer/Delete/5
        public ActionResult Delete(int id)
        {
            Answer answer = iAnswerRepository.FindAnswer(id);
            if (answer == null)
            {
                return HttpNotFound();
            }
            return View(answer);
        }

        //// POST: /Answer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Answer answer = iAnswerRepository.FindAnswer(id);
            iAnswerRepository.DeleteAnswer(id);
            return RedirectToAction("Index", new { id = answer.QuestionId });
        }

        [HttpPost]
        // [ValidateAntiForgeryToken]
        public void Correct(int id)
        {
            var answerid = int.Parse(Session["QuestionId"].ToString());
            var answer = iQuestionRepository.FindQuesstion(answerid);
            answer.CorrectAnswerId = id;
            iQuestionRepository.EditQuestion(answer);
        }
        [HttpPost]
        // [ValidateAntiForgeryToken]
        public void RemoveAnswer()
        {
            var _questionId = int.Parse(Session["QuestionId"].ToString());
            var _question = iQuestionRepository.FindQuesstion(_questionId);
            _question.CorrectAnswerId = null;
            iQuestionRepository.EditQuestion(_question);
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
