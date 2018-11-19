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
	}
}