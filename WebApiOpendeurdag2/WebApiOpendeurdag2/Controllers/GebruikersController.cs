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
    public class GebruikersController : ApiController
    {
        private WebApiOpendeurdag2Context db = new WebApiOpendeurdag2Context();

        // GET: api/Gebruikers
        public IQueryable<Gebruiker> GetGebruikers()
        {
            return db.Gebruikers;
        }

        // GET: api/Gebruikers/5
        [ResponseType(typeof(Gebruiker))]
        public async Task<IHttpActionResult> GetGebruiker(int id)
        {
            Gebruiker gebruiker = await db.Gebruikers.FindAsync(id);
            if (gebruiker == null)
            {
                return NotFound();
            }

            return Ok(gebruiker);
        }

        // PUT: api/Gebruikers/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutGebruiker(int id, Gebruiker gebruiker)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != gebruiker.GebruikerId)
            {
                return BadRequest();
            }

            db.Entry(gebruiker).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GebruikerExists(id))
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

        // POST: api/Gebruikers
        [ResponseType(typeof(Gebruiker))]
        public async Task<IHttpActionResult> PostGebruiker(Gebruiker gebruiker)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Gebruikers.Add(gebruiker);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = gebruiker.GebruikerId }, gebruiker);
        }

        // DELETE: api/Gebruikers/5
        [ResponseType(typeof(Gebruiker))]
        public async Task<IHttpActionResult> DeleteGebruiker(int id)
        {
            Gebruiker gebruiker = await db.Gebruikers.FindAsync(id);
            if (gebruiker == null)
            {
                return NotFound();
            }

            db.Gebruikers.Remove(gebruiker);
            await db.SaveChangesAsync();

            return Ok(gebruiker);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool GebruikerExists(int id)
        {
            return db.Gebruikers.Count(e => e.GebruikerId == id) > 0;
        }
    }
}