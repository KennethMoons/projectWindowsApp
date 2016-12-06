using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using WebApiOpendeurdag2.Models;

namespace WebApiOpendeurdag2.Controllers
{
    public class InfomomentsController : ApiController
    {
        private WebApiOpendeurdag2Context db = new WebApiOpendeurdag2Context();

        // GET: api/Infomoments
        public IQueryable<Infomoment> GetInfomoments()
        {
            return db.Infomoments;
        }

        // GET: api/Infomoments/5
        [ResponseType(typeof(Infomoment))]
        public async Task<IHttpActionResult> GetInfomoment(int id)
        {
            Infomoment infomoment = await db.Infomoments.FindAsync(id);
            if (infomoment == null)
            {
                return NotFound();
            }

            return Ok(infomoment);
        }

        // PUT: api/Infomoments/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutInfomoment(int id, Infomoment infomoment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != infomoment.InfomomentId)
            {
                return BadRequest();
            }

            db.Entry(infomoment).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InfomomentExists(id))
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

        // POST: api/Infomoments
        [ResponseType(typeof(Infomoment))]
        public async Task<IHttpActionResult> PostInfomoment(Infomoment infomoment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Infomoments.Add(infomoment);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = infomoment.InfomomentId }, infomoment);
        }

        // DELETE: api/Infomoments/5
        [ResponseType(typeof(Infomoment))]
        public async Task<IHttpActionResult> DeleteInfomoment(int id)
        {
            Infomoment infomoment = await db.Infomoments.FindAsync(id);
            if (infomoment == null)
            {
                return NotFound();
            }

            db.Infomoments.Remove(infomoment);
            await db.SaveChangesAsync();

            return Ok(infomoment);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool InfomomentExists(int id)
        {
            return db.Infomoments.Count(e => e.InfomomentId == id) > 0;
        }
    }
}