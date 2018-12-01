using LasEmpanadas.Models;
using LasEmpanadas.Services;
using System;
using System.Web.Mvc;

namespace LasEmpanadas.Controllers
{
    public class HomeController : Controller
    {
        LoginService LoginSvc = new LoginService();
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Error()
        {
            if (Session["loggedUser"] == null)
            {
                return RedirectToAction("Login");
            }
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Usuario User)
        {
            if (ModelState.IsValid)
            {
                if (
                LoginSvc.Login(User))
                {
                    return View("Index");
                }
                else
                {
                    ViewBag.errorGeneral = ("Usuario y/o contraseña inválidos");
                    return View(User);
                }
            }
            else
            {
                return View(User);
            }
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login");
        }

    }

}