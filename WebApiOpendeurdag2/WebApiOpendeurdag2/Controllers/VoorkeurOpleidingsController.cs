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
    [Authorize]
    public class VoorkeurOpleidingsController : ApiController
    {
        private WebApiOpendeurdag2Context db = new WebApiOpendeurdag2Context();

        // GET: api/VoorkeurOpleidings
        public IQueryable<VoorkeurOpleiding> GetVoorkeurOpleidings()
        {
            return db.VoorkeurOpleidings;
        }

        // GET: api/VoorkeurOpleidings/5
        [ResponseType(typeof(VoorkeurOpleiding))]
        public async Task<IHttpActionResult> GetVoorkeurOpleiding(int id)
        {
            VoorkeurOpleiding voorkeurOpleiding = await db.VoorkeurOpleidings.FindAsync(id);
            if (voorkeurOpleiding == null)
            {
                return NotFound();
            }

            return Ok(voorkeurOpleiding);
        }

        // PUT: api/VoorkeurOpleidings/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutVoorkeurOpleiding(int id, VoorkeurOpleiding voorkeurOpleiding)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != voorkeurOpleiding.VoorkeurOpleidingId)
            {
                return BadRequest();
            }

            db.Entry(voorkeurOpleiding).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VoorkeurOpleidingExists(id))
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

        // POST: api/VoorkeurOpleidings
        [ResponseType(typeof(VoorkeurOpleiding))]
        public async Task<IHttpActionResult> PostVoorkeurOpleiding(VoorkeurOpleiding voorkeurOpleiding)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.VoorkeurOpleidings.Add(voorkeurOpleiding);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = voorkeurOpleiding.VoorkeurOpleidingId }, voorkeurOpleiding);
        }

        // DELETE: api/VoorkeurOpleidings/5
        [ResponseType(typeof(VoorkeurOpleiding))]
        public async Task<IHttpActionResult> DeleteVoorkeurOpleiding(int id)
        {
            VoorkeurOpleiding voorkeurOpleiding = await db.VoorkeurOpleidings.FindAsync(id);
            if (voorkeurOpleiding == null)
            {
                return NotFound();
            }

            db.VoorkeurOpleidings.Remove(voorkeurOpleiding);
            await db.SaveChangesAsync();

            return Ok(voorkeurOpleiding);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool VoorkeurOpleidingExists(int id)
        {
            return db.VoorkeurOpleidings.Count(e => e.VoorkeurOpleidingId == id) > 0;
        }
    }
}