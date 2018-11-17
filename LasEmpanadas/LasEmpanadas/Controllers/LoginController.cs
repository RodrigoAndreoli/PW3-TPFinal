using LasEmpanadas.Models;
using LasEmpanadas.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LasEmpanadas.Controllers
{
    public class UserController : Controller
    {
        static LoginService LoginService = new LoginService();

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Usuario user)
        {
            if (ModelState.IsValid)
            {
                LoginService.Login(user);
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }
    }
}