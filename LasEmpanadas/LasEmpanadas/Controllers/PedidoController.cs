using LasEmpanadas.Models;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;

namespace LasEmpanadas.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class PedidoController : ApiController
    {
        private MasterEntities Db = new MasterEntities();

        // GET: api/Pedido
        public IQueryable<Pedido> GetPedido()
        {
            return Db.Pedido;
        }

        // GET: api/Pedido/{Id}
        [ResponseType(typeof(Pedido))]
        public IHttpActionResult GetPedido(int Id)
        {
            Pedido Order = Db.Pedido.Find(Id);
            if (Order == null)
            {
                return NotFound();
            }

            return Ok(Order);
        }

        // PUT: api/Pedido/{Id}
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPedido(int Id, Pedido Order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (Id != Order.IdPedido)
            {
                return BadRequest();
            }

            Db.Entry(Order).State = EntityState.Modified;

            try
            {
                Db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PedidoExists(Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Pedido
        [ResponseType(typeof(Pedido))]
        public IHttpActionResult PostPedido(Pedido Order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Db.Pedido.Add(Order);

            try
            {
                Db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (PedidoExists(Order.IdPedido))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { Id = Order.IdPedido }, Order);
        }

        // DELETE: api/Pedido/{Id}
        [ResponseType(typeof(Pedido))]
        public IHttpActionResult DeletePedido(int Id)
        {
            Pedido Order = Db.Pedido.Find(Id);

            if (Order == null)
            {
                return NotFound();
            }

            Db.Pedido.Remove(Order);
            Db.SaveChanges();

            return Ok(Order);
        }

        protected override void Dispose(bool Disposing)
        {
            if (Disposing)
            {
                Db.Dispose();
            }

            base.Dispose(Disposing);
        }

        private bool PedidoExists(int Id)
        {
            return Db.Pedido.Count(Element => Element.IdPedido == Id) > 0;
        }

    }

}