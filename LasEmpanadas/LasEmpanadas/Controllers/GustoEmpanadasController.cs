using LasEmpanadas.Models;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;

namespace LasEmpanadas.Controllers
{
    public class GustoEmpanadasController : ApiController
    {
        MasterEntities Db = new MasterEntities();

        // GET: api/GustoEmpanadas
        public IQueryable<GustoEmpanada> GetGustoEmpanada()
        {
            return Db.GustoEmpanada;
        }

        // GET: api/GustoEmpanadas/{Id}
        [ResponseType(typeof(GustoEmpanada))]
        public IHttpActionResult GetGustoEmpanada(int Id)
        {
            GustoEmpanada Gusto = Db.GustoEmpanada.Find(Id);

            if (Gusto == null)
            {
                return NotFound();
            }

            return Ok(Gusto);
        }

        // PUT: api/GustoEmpanadas/{Id}
        [ResponseType(typeof(void))]
        public IHttpActionResult PutGustoEmpanada(int Id, GustoEmpanada Gusto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (Id != Gusto.IdGustoEmpanada)
            {
                return BadRequest();
            }

            Db.Entry(Gusto).State = EntityState.Modified;

            try
            {
                Db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GustoEmpanadaExists(Id))
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

        // POST: api/GustoEmpanadas
        [ResponseType(typeof(GustoEmpanada))]
        public IHttpActionResult PostGustoEmpanada(GustoEmpanada Gusto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Db.GustoEmpanada.Add(Gusto);

            try
            {
                Db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (GustoEmpanadaExists(Gusto.IdGustoEmpanada))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { Id = Gusto.IdGustoEmpanada }, Gusto);
        }

        // DELETE: api/GustoEmpanadas/{Id}
        [ResponseType(typeof(GustoEmpanada))]
        public IHttpActionResult DeleteGustoEmpanada(int Id)
        {
            GustoEmpanada Gusto = Db.GustoEmpanada.Find(Id);

            if (Gusto == null)
            {
                return NotFound();
            }

            Db.GustoEmpanada.Remove(Gusto);
            Db.SaveChanges();

            return Ok(Gusto);
        }

        protected override void Dispose(bool Disposing)
        {
            if (Disposing)
            {
                Db.Dispose();
            }

            base.Dispose(Disposing);
        }

        private bool GustoEmpanadaExists(int Id)
        {
            return Db.GustoEmpanada.Count(Element => Element.IdGustoEmpanada == Id) > 0;
        }

    }

}