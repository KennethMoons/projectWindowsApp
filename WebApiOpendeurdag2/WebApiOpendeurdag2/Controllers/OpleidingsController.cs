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
    [Authorize(Roles = GebruikersRollen.Admin)]
    public class OpleidingsController : ApiController
    {
        private WebApiOpendeurdag2Context db = new WebApiOpendeurdag2Context();

        // GET: api/Opleidings
        [AllowAnonymous]
        public IQueryable<Opleiding> GetOpleidings()
        {
            return db.Opleidings;
        }

        // GET: api/Opleidings/5
        [AllowAnonymous]
        [ResponseType(typeof(Opleiding))]
        public async Task<IHttpActionResult> GetOpleiding(int id)
        {
            Opleiding opleiding = await db.Opleidings.FindAsync(id);
            if (opleiding == null)
            {
                return NotFound();
            }

            return Ok(opleiding);
        }

        // PUT: api/Opleidings/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutOpleiding(int id, Opleiding opleiding)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != opleiding.OpleidingId)
            {
                return BadRequest();
            }

            db.Entry(opleiding).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OpleidingExists(id))
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

        // POST: api/Opleidings
        [ResponseType(typeof(Opleiding))]
        public async Task<IHttpActionResult> PostOpleiding(Opleiding opleiding)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Opleidings.Add(opleiding);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = opleiding.OpleidingId }, opleiding);
        }

        // DELETE: api/Opleidings/5
        [ResponseType(typeof(Opleiding))]
        public async Task<IHttpActionResult> DeleteOpleiding(int id)
        {
            Opleiding opleiding = await db.Opleidings.FindAsync(id);
            if (opleiding == null)
            {
                return NotFound();
            }

            db.Opleidings.Remove(opleiding);
            await db.SaveChangesAsync();

            return Ok(opleiding);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool OpleidingExists(int id)
        {
            return db.Opleidings.Count(e => e.OpleidingId == id) > 0;
        }
    }
}