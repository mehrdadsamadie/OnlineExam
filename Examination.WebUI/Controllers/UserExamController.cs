using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Examination.Domain.Concrete;
using Examination.Domain.Entities;
using Examination.Domain.Abstract;
using Examination.WebUI.Models;
using Microsoft.AspNet.Identity;
using System.Web.UI.WebControls;
using Examination.WebUI.Infrastructure;
using System.Text.RegularExpressions;
using IdentitySample.Models;
using Microsoft.AspNet.Identity.Owin;

namespace Examination.WebUI.Controllers
{
    [Authorize]
    public class UserExamController : Controller
    {
        // GET: UserExam

        private IQuestionRepository IQuestionRepository;
        private IExamRepository IExamRepository;
        private IAnswerRepository IAnswerRepository;
        private IGuideRepository IGuideRepository;
        private IUserScoreRepository IUserScoreRepository;
        public UserExamController(IQuestionRepository iQuestionRepository, IExamRepository iExamRepository, IAnswerRepository iAnswerRepository, IGuideRepository iGuideRepository, IUserScoreRepository iUserScoreRepository)
        {
            IQuestionRepository = iQuestionRepository;
            IExamRepository = iExamRepository;
            IAnswerRepository = iAnswerRepository;
            IGuideRepository = iGuideRepository;
            IUserScoreRepository = iUserScoreRepository;
        }

        ExamView Fill(int id)
        {
            var strCurrentUserId = User.Identity.GetUserId();
            var examview = new ExamView();
            var exam = IExamRepository.FindExam(id);
            var question = IQuestionRepository.GetQuestionNotAnswerd(exam.ExamId, strCurrentUserId).FirstOrDefault();
            if (question == null)
                return null;
            else
            {
                var _curentuserscore = IUserScoreRepository.GetUserScore(question.QuestionId, strCurrentUserId);
                if (_curentuserscore != null)
                {
                    if (_curentuserscore.NumberAnswer > 0)
                    {
                        var _hint = IGuideRepository.AllGuides(question.QuestionId).Skip(_curentuserscore.NumberAnswer - 1).FirstOrDefault();
                        if (_hint != null)
                        {
                            examview.Hint = _hint.Text;
                            examview.HintId = _hint.GuideId;
                        }
                    }
                }
                var answers = IAnswerRepository.AllAnswer(question.QuestionId).Select(x => new { x.AnswerId, x.Text }).ToList();
                examview.ExamId = exam.ExamId;
                examview.ExamTypeId = exam.ExamTypeId;
                examview.QuestionId = question.QuestionId;
                examview.Question = question.Text;
                if (question.CorrectAnswerId != null)
                    examview.QuestionPoint = (decimal)question.Score / (_curentuserscore == null ? 1 : _curentuserscore.NumberAnswer + 1);
                else
                    examview.QuestionPoint = null;
                examview.TotalQuestion = IQuestionRepository.TotalQuestion(exam.ExamId);
                examview.TotalQuestionAnswered = IQuestionRepository.TotalQuestionAnswered(exam.ExamId, strCurrentUserId) + 1;
                examview.ExamPoint = IUserScoreRepository.GetExamUserScore(exam.ExamId, strCurrentUserId);
                examview.StartTime = DateTime.Now;
                examview.Answers = answers.Select(x => new AnswerStudent() { AnswerId = x.AnswerId, Answer = x.Text }).ToList();
                return examview;
            }
        }
        public ActionResult Test(int id)
        {
            var _exam = IExamRepository.FindExam(id);
            if (_exam.ParentExamId != null)
            {
                if (IQuestionRepository.GetQuestionNotAnswerd(_exam.ParentExamId.Value, User.Identity.GetUserId()).FirstOrDefault() != null)
                {
                    return RedirectToAction("ActivExams", "Exam", null);
                }
            }
            var examview = Fill(id);
            if (examview == null)
                return View("Finish");
            return View(examview);
        }
        public ActionResult Report() 
        {
            return View();
        }


        [HttpPost]
        public ActionResult Test(ExamView examview)
        {
            var _correctanswer = IQuestionRepository.FindQuesstion(examview.QuestionId).CorrectAnswerId;
            var _strcurrentuserid = User.Identity.GetUserId();
            var _question = IQuestionRepository.FindQuesstion(examview.QuestionId);
            var _Hint = IGuideRepository.AllGuides(_question.QuestionId);
            var _userscore = IUserScoreRepository.GetUserScore(examview.QuestionId, _strcurrentuserid);
            if (_userscore != null)
            {
                _userscore.UserAnswerId = examview.SelctedAnswer;
                _userscore.NumberAnswer++;
                if (examview.SelctedAnswer == _correctanswer || _correctanswer == null)
                {
                    _userscore.IsFinish = true;
                    _userscore.EndTime = DateTime.Now;
                }
                if (_question.CorrectAnswerId != null)
                    _userscore.Grade = (decimal)_question.Score / _userscore.NumberAnswer;
                IUserScoreRepository.EditUserScore(_userscore);
            }
            else
            {
                var _newuserscore = new UserScore();
                _newuserscore.UserId = _strcurrentuserid;
                _newuserscore.QuestionId = examview.QuestionId;
                _newuserscore.NumberAnswer = 1;
                _newuserscore.UserAnswerId = examview.SelctedAnswer;
                _newuserscore.StartTime = examview.StartTime;
                if (examview.SelctedAnswer == _correctanswer || _correctanswer == null || _Hint.Count() == 0)
                {
                    _newuserscore.IsFinish = true;
                    _newuserscore.EndTime = DateTime.Now;
                }
                if (_question.CorrectAnswerId != null)
                    if (_Hint.Count() == 0 && examview.SelctedAnswer != _correctanswer)
                        _newuserscore.Grade = 0;
                    else
                        _newuserscore.Grade = (decimal)_question.Score / _newuserscore.NumberAnswer;
                else
                    _newuserscore.Grade = null;
                IUserScoreRepository.AddUserScore(_newuserscore);
            }

            return RedirectToAction("Test", new { id = examview.ExamId });
        }
        [HttpGet]
        public ActionResult ExamsResult()
        {
            var strCurrentUserId = User.Identity.GetUserId();
            var _examsresulut = new List<UserExamsResultView>();
            var _exams = IExamRepository.AllExams().ToList();
            foreach (var exam in _exams)
            {
                if (IQuestionRepository.GetQuestionNotAnswerd(exam.ExamId, strCurrentUserId).FirstOrDefault() == null)
                {
                    var _userexamresult = new UserExamsResultView();
                    _userexamresult.ExamId = exam.ExamId;
                    _userexamresult.ExamName = exam.Name;
                    _userexamresult.ExamType = exam.ExamType.Name;
                    _userexamresult.ExamPoint = IUserScoreRepository.GetExamUserScore(exam.ExamId, strCurrentUserId);
                    _examsresulut.Add(_userexamresult);
                }
            }
            return View(_examsresulut);
        }

        [HttpGet]
        public ActionResult ExamResult(int id)
        {
            var strCurrentUserId = User.Identity.GetUserId();
            var _examresulut = new List<UserExamResultView>();
            var _exam = IExamRepository.FindExam(id);
            if (_exam != null)
            {
                if (IQuestionRepository.GetQuestionNotAnswerd(_exam.ExamId, strCurrentUserId).FirstOrDefault() == null)
                {
                    var _userscores = IUserScoreRepository.GetExamQuestionSecore(_exam.ExamId, strCurrentUserId).ToList();
                    foreach (var _userscore in _userscores)
                    {
                        var _userexamrsult = new UserExamResultView();
                        _userexamrsult.QuestionId = _userscore.QuestionId;
                        _userexamrsult.QuestionName = _userscore.Question.Text;
                        _userexamrsult.SelectedAnswer = _userscore.Answer.Text;
                        _userexamrsult.NumberAnswer = _userscore.NumberAnswer;
                        _userexamrsult.duration = (_userscore.EndTime - _userscore.StartTime);
                        _userexamrsult.QuestionPoint = _userscore.Grade;
                        _examresulut.Add(_userexamrsult);
                    }
                }
            }
            return View(_examresulut);
        }
        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult AdminResultAllExcel()
        {
            var _users = UserManager.Users.ToList();
            var _exam = IExamRepository.AllExams().ToList();
            var _allscore = new List<UserScore>();
            foreach (var user in _users)
            {
                foreach (var exam in _exam)
                {
                    if (IQuestionRepository.GetQuestionNotAnswerd(exam.ExamId, user.Id).FirstOrDefault() == null)
                    {
                        var _userscores = IUserScoreRepository.GetExamQuestionSecore(exam.ExamId, user.Id).ToList();
                        _allscore.AddRange(_userscores);
                    }
                }
            }
            var _result = _allscore.Select(x => new { x.User.UserName, x.Question.Exam.Name, Question = Regex.Replace(x.Question.Text, @"<[^>]+>|&nbsp;", "").Trim(), Answer = x.Answer.Text, x.NumberAnswer, x.Grade, Time = (x.EndTime - x.StartTime) });
            GridView gv = new GridView();
            gv.DataSource = _result;
            gv.DataBind();
            return new DownloadFileActionResult(gv, "ResultExam.xls");
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult AdminResultExcel()
        {
            var _users = UserManager.Users.ToList();
            var _exam = IExamRepository.AllExams().ToList();
            var _allscore = new List<UserScore>();
            foreach (var user in _users)
            {
                foreach (var exam in _exam)
                {
                    if (IQuestionRepository.GetQuestionNotAnswerd(exam.ExamId, user.Id).FirstOrDefault() == null)
                    {
                        var _userscores = IUserScoreRepository.GetExamQuestionSecore(exam.ExamId, user.Id).ToList();
                        _allscore.AddRange(_userscores);
                    }
                }
            }
            var tt = _allscore.GroupBy(x => new { x.User.UserName, x.Question.Exam.ExamId }).Select(g => new { g.Key.UserName, ExamName=g.Max(x=>x.Question.Exam.Name), Grade = g.Sum(x => x.Grade) }).ToList();

            GridView gv = new GridView();
            gv.DataSource = tt;
            gv.DataBind();
            return new DownloadFileActionResult(gv, "ResultExam.xls");
        }
    }
}
