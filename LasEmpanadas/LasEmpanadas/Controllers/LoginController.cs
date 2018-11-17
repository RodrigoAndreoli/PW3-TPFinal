using LasEmpanadas.Models;
using LasEmpanadas.Services;
using System.Web.Mvc;

namespace LasEmpanadas.Controllers
{
    public class UserController : Controller
    {
        LoginService LoginSvc = new LoginService();

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Usuario User)
        {
            if (ModelState.IsValid)
            {
                LoginSvc.Login(User);
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

    }

}