using System.Web.Mvc;
using Examination.Domain.Entities;
using Examination.Domain.Abstract;

namespace IdentitySample.Controllers
{
    public class HomeController : Controller
    {
        private IExamTypeRepository iExamTypeRepository;
        public HomeController(IExamTypeRepository iexamtyperepository) 
        {
            iExamTypeRepository = iexamtyperepository;
        }
        public ActionResult Index( )
        {
            var tt= new ExamType();
            tt.Name="Tofel";
           // iExamTypeRepository.Add(tt);
            return View();
        }

        [Authorize]
        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [Authorize(Roles="Admin")]
        public ActionResult Manage()
        {
            ViewBag.Message = "Your Manage Page";

            return View();
        }
    }
}
