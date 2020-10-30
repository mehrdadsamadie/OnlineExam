using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Examination.Domain.Abstract;
using Examination.Domain.Concrete;
using Examination.Domain.Entities;
using RTE;

namespace Examination.WebUI.Controllers
{
   
    public class ExamController : Controller
    {
        private IExamRepository iExamRepository;
        private IExamTypeRepository iExamTypeRepository;

        public ExamController(IExamRepository iexamrepository, IExamTypeRepository iexamtyperepository)
        {
            this.iExamTypeRepository = iexamtyperepository;
            this.iExamRepository = iexamrepository;
        }
        [Authorize(Roles = "Admin,Teacher")]
        // GET: /Exam/
        public ActionResult Index()
        {
            var exams = iExamRepository.AllExams();
            return View(exams.ToList());
        }
        [Authorize(Roles = "Admin,Teacher")]
        //// GET: /Exam/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Exam exam = iExamRepository.FindExam(id);
            if (exam == null)
            {
                return HttpNotFound();
            }
            ViewBag.Description = exam.Description;
            return View(exam);
        }
        [Authorize(Roles = "Admin,Teacher")]
        // GET: /Exam/Create
        public ActionResult Create()
        {
            ViewBag.ExamTypeId = new SelectList(iExamTypeRepository.AllExamTypes().ToList(), "ExamTypeId", "Name");
            ViewBag.ParentExamId = new SelectList(iExamRepository.AllExams().ToList(), "ExamId", "Name");
            return View();
        }

        // POST: /Exam/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin,Teacher")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Exam exam)
        {
            if (ModelState.IsValid)
            {
                exam.IsDelete = false;
                exam.IsActive = true;
                iExamRepository.AddExam(exam);
                return RedirectToAction("Index");
            }
            ViewBag.Description = exam.Description;
            ViewBag.ExamTypeId = new SelectList(iExamTypeRepository.AllExamTypes().ToList(), "ExamTypeId", "Name");
            ViewBag.ParentExamId = new SelectList(iExamRepository.AllExams().ToList(), "ExamId", "Name");
            return View(exam);
        }
        [Authorize(Roles = "Admin,Teacher")]
        //// GET: /Exam/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Exam exam = iExamRepository.FindExam(id);
            if (exam == null)
            {
                return HttpNotFound();
            }
            ViewBag.Description = exam.Description;
            ViewBag.ExamTypeId = new SelectList(iExamTypeRepository.AllExamTypes().ToList(), "ExamTypeId", "Name",exam.ExamTypeId);
            ViewBag.ParentExamId = new SelectList(iExamRepository.AllExams().ToList(), "ExamId", "Name",exam.ParentExamId);
            return View(exam);
        }

        //// POST: /Exam/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Teacher")]
        public ActionResult Edit(Exam exam)
        {
            if (ModelState.IsValid)
            {
                iExamRepository.EditExam(exam);
                return RedirectToAction("Index");
            }
            ViewBag.Description = exam.Description;
            ViewBag.ExamTypeId = new SelectList(iExamTypeRepository.AllExamTypes().ToList(), "ExamTypeId", "Name");
            ViewBag.ParentExamId = new SelectList(iExamRepository.AllExams().ToList(), "ExamId", "Name");
            return View(exam);
        }
        [Authorize(Roles = "Admin,Teacher")]
        //// GET: /Exam/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Exam exam = iExamRepository.FindExam(id);
            if (exam == null)
            {
                return HttpNotFound();
            }
            return View(exam);
        }
        [Authorize(Roles = "Admin,Teacher")]
        //// POST: /Exam/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Exam exam = iExamRepository.FindExam(id);
            iExamRepository.DeleteExam(exam);
            return RedirectToAction("Index");
        }


        //[HttpPost, ActionName("Active")]
        //[ValidateAntiForgeryToken]
        public ActionResult Active(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                iExamRepository.ActiveExam(id);   
            }
            return RedirectToAction("Index");
        }
        public ActionResult ActivExams() 
        {
            return View(iExamRepository.AllActiveExams().ToList());
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
