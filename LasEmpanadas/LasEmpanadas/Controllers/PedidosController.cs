using LasEmpanadas.Models;
using LasEmpanadas.Services;
using System.Web.Mvc;

namespace LasEmpanadas.Controllers
{
    public class PedidosController : Controller
    {
        PedidoService PedidoSvc = new PedidoService();

        public ActionResult Iniciar()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Iniciar(Pedido Order)
        {
            if (ModelState.IsValid)
            {
                PedidoSvc.CreateOrder(Order);
                return View(Order);
            }
            else
            {
                return View(Order);
            }
        }

        public ActionResult Iniciado()
        {
            return View();
        }

        public ActionResult Lista()
        {
            return View();
        }

        public ActionResult Editar()
        {
            return View();
        }

        public ActionResult Eliminar()
        {
            return View();
        }

        public ActionResult Elegir()
        {
            return View();
        }

        public ActionResult Detalle()
        {
            return View();
        }

    }

}