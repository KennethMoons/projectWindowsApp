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
    public class VoorkeurCampusController : ApiController
    {
        private WebApiOpendeurdag2Context db = new WebApiOpendeurdag2Context();

        // GET: api/VoorkeurCampus
        public IQueryable<VoorkeurCampus> GetVoorkeurCampus()
        {
            return db.VoorkeurCampus;
        }

        // GET: api/VoorkeurCampus/5
        [ResponseType(typeof(VoorkeurCampus))]
        public async Task<IHttpActionResult> GetVoorkeurCampus(int id)
        {
            VoorkeurCampus voorkeurCampus = await db.VoorkeurCampus.FindAsync(id);
            if (voorkeurCampus == null)
            {
                return NotFound();
            }

            return Ok(voorkeurCampus);
        }

        // PUT: api/VoorkeurCampus/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutVoorkeurCampus(int id, VoorkeurCampus voorkeurCampus)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != voorkeurCampus.VoorkeurCampusId)
            {
                return BadRequest();
            }

            db.Entry(voorkeurCampus).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VoorkeurCampusExists(id))
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

        // POST: api/VoorkeurCampus
        [ResponseType(typeof(VoorkeurCampus))]
        public async Task<IHttpActionResult> PostVoorkeurCampus(VoorkeurCampus voorkeurCampus)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.VoorkeurCampus.Add(voorkeurCampus);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = voorkeurCampus.VoorkeurCampusId }, voorkeurCampus);
        }

        // DELETE: api/VoorkeurCampus/5
        [ResponseType(typeof(VoorkeurCampus))]
        public async Task<IHttpActionResult> DeleteVoorkeurCampus(int id)
        {
            VoorkeurCampus voorkeurCampus = await db.VoorkeurCampus.FindAsync(id);
            if (voorkeurCampus == null)
            {
                return NotFound();
            }

            db.VoorkeurCampus.Remove(voorkeurCampus);
            await db.SaveChangesAsync();

            return Ok(voorkeurCampus);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool VoorkeurCampusExists(int id)
        {
            return db.VoorkeurCampus.Count(e => e.VoorkeurCampusId == id) > 0;
        }
    }
}