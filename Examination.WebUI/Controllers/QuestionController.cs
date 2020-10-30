using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Examination.WebUI.Models;
using Examination.Domain.Abstract;
using Examination.Domain.Concrete;
using Examination.Domain.Entities;

namespace Examination.WebUI.Controllers
{
    [Authorize(Roles = "Admin,Teacher")]
    public class QuestionController : Controller
    {
        ////
        //// GET: /Question/
        //public ActionResult Index()
        //{
        //    return View();
        //}

        ////
        //// GET: /Question/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}
        private IQuestionRepository iQuestionRepository;
        public QuestionController(IQuestionRepository iquestionrepository)
        {
            this.iQuestionRepository = iquestionrepository;

        }


        public ActionResult HtmlHint()
        {
            return PartialView("_Hint", new HintView());
        }

        public ActionResult HtmlAnswer()
        {
            return PartialView("_Answer", new AnswerView());
        }

        //
        // GET: /Question/Create
        public ActionResult Create(int id)
        {
            var model = new QuestionView(id);
            return View(model);
        }

        //
        // POST: /Question/Create
        [HttpPost]
        public ActionResult Create(QuestionView model)
        {
            try
            {


                return RedirectToAction("Index", "Exam");
            }
            catch
            {
                return View();
            }
        }

        ////
        //// GET: /Question/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        ////
        //// POST: /Question/Edit/5
        //[HttpPost]
        //public ActionResult Edit(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add update logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        ////
        //// GET: /Question/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        ////
        //// POST: /Question/Delete/5
        //[HttpPost]
        //public ActionResult Delete(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add delete logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}
