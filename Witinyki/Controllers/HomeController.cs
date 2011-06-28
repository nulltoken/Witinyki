using System.Web.Mvc;
using Microsoft.Web.Mvc;

namespace Witinyki.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return this.RedirectToAction<WikiController>(c => c.Index());
        }
    }
}