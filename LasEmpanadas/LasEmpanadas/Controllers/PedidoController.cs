using LasEmpanadas.Models;
using LasEmpanadas.Services;
using System.Web.Mvc;

namespace LasEmpanadas.Controllers
{
    public class PedidoController : Controller
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
                Pedido CreatedOrder = PedidoSvc.CreateOrder(Order);
                return RedirectToAction("Iniciado", new { id = CreatedOrder.IdPedido });
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