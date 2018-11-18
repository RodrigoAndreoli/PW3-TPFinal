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
        public ActionResult Iniciar(Pedido OrderDTO)
        {
            if (ModelState.IsValid)
            {
                Pedido Order = PedidoSvc.CreateOrder(OrderDTO);
                return RedirectToAction("Iniciado", new { id = Order.IdPedido });
            }
            else
            {
                return View(OrderDTO);
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