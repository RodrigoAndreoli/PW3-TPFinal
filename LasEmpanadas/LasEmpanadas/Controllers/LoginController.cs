using LasEmpanadas.Models;
using LasEmpanadas.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LasEmpanadas.Controllers
{
    public class LoginController : Controller
    {
        static LoginService LoginService = new LoginService();

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Iniciar(Usuario user)
        {
            if (ModelState.IsValid)
            {
                LoginService.Login(user);
                return View(user);
            }
            else
            {
                return View(p);
            }
        }
    }
}