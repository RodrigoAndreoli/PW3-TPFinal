using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LasEmpanadas.Models;

namespace LasEmpanadas.Controllers
{
    public class PedidosController : Controller
    {

        public ActionResult Iniciar()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Iniciar(Pedido p)
        {
            if (ModelState.IsValid)
            {
                //Agregar pedido y todo lo demas
                return View();
            }
            else
            {
                return View(p);
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