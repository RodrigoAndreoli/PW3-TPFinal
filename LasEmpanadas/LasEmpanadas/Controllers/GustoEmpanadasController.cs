using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using LasEmpanadas.Models;

namespace LasEmpanadas.Controllers
{
    public class GustoEmpanadasController : ApiController
    {
        private MasterEntities db = new MasterEntities();

        // GET: api/GustoEmpanadas
        public IQueryable<GustoEmpanada> GetGustoEmpanada()
        {
            return db.GustoEmpanada;
        }

        // GET: api/GustoEmpanadas/5
        [ResponseType(typeof(GustoEmpanada))]
        public IHttpActionResult GetGustoEmpanada(int id)
        {
            GustoEmpanada gustoEmpanada = db.GustoEmpanada.Find(id);
            if (gustoEmpanada == null)
            {
                return NotFound();
            }

            return Ok(gustoEmpanada);
        }

        // PUT: api/GustoEmpanadas/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutGustoEmpanada(int id, GustoEmpanada gustoEmpanada)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != gustoEmpanada.IdGustoEmpanada)
            {
                return BadRequest();
            }

            db.Entry(gustoEmpanada).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GustoEmpanadaExists(id))
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
        public IHttpActionResult PostGustoEmpanada(GustoEmpanada gustoEmpanada)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.GustoEmpanada.Add(gustoEmpanada);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (GustoEmpanadaExists(gustoEmpanada.IdGustoEmpanada))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = gustoEmpanada.IdGustoEmpanada }, gustoEmpanada);
        }

        // DELETE: api/GustoEmpanadas/5
        [ResponseType(typeof(GustoEmpanada))]
        public IHttpActionResult DeleteGustoEmpanada(int id)
        {
            GustoEmpanada gustoEmpanada = db.GustoEmpanada.Find(id);
            if (gustoEmpanada == null)
            {
                return NotFound();
            }

            db.GustoEmpanada.Remove(gustoEmpanada);
            db.SaveChanges();

            return Ok(gustoEmpanada);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool GustoEmpanadaExists(int id)
        {
            return db.GustoEmpanada.Count(e => e.IdGustoEmpanada == id) > 0;
        }
    }
}