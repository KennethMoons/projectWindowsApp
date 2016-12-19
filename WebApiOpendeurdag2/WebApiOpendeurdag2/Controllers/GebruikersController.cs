using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using WebApiOpendeurdag2.Models;
using WebApiOpendeurdag2.Security;

namespace WebApiOpendeurdag2.Controllers
{
    [Authorize]
    public class GebruikersController : ApiController
    {
        private WebApiOpendeurdag2Context db = new WebApiOpendeurdag2Context();

        // GET: api/Gebruikers
        [Authorize(Roles = GebruikersRollen.Admin)]
        public IQueryable<Gebruiker> GetGebruikers()
        {
            return db.Gebruikers;
        }

        // GET: api/Gebruikers/5
        [Authorize(Roles = GebruikersRollen.Admin)]
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

        // GET: api/Gebruikers/Current
        [Route("api/Gebruikers/Current")]
        [ResponseType(typeof(Gebruiker))]
        public IHttpActionResult GetCurrent()
        {
            // Check if user is set
            if (!(HttpContext.Current.User is APIPrincipal))
            {
                return NotFound();
            }

            // Return current user
            APIPrincipal principal = (APIPrincipal)HttpContext.Current.User;

            Gebruiker gebruiker = db.Gebruikers
                .Include(g => g.VoorkeurCampussen)
                .Include(g => g.VoorkeurOpleidingen)
                .Where(g => g.GebruikerId == principal.User.GebruikerId)
                .Single();

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

            var dbGebruiker = db.Gebruikers
                .Include(g => g.VoorkeurCampussen)
                .Include(g => g.VoorkeurOpleidingen)
                .Single(g => g.GebruikerId == gebruiker.GebruikerId);

            db.Entry(dbGebruiker).CurrentValues.SetValues(gebruiker);

            var deletedCampussen = dbGebruiker.VoorkeurCampussen.Except(gebruiker.VoorkeurCampussen).ToList();
            var addedCampussen = gebruiker.VoorkeurCampussen.Except(dbGebruiker.VoorkeurCampussen).ToList();

            Debug.WriteLine("CAMPUSSEN: - {0} + {1}", deletedCampussen.Count, addedCampussen.Count);

            deletedCampussen.ForEach(c => dbGebruiker.VoorkeurCampussen.Remove(c));

            foreach (Campus campus in addedCampussen)
            {
                if (db.Entry(campus).State == EntityState.Detached)
                {
                    db.Campus.Attach(campus);
                }
                dbGebruiker.VoorkeurCampussen.Add(campus);
            }

            var deletedOpleidingen = dbGebruiker.VoorkeurOpleidingen.Except(gebruiker.VoorkeurOpleidingen).ToList();
            var addedOpleidingen = gebruiker.VoorkeurOpleidingen.Except(dbGebruiker.VoorkeurOpleidingen).ToList();

            Debug.WriteLine("OPLEIDINGEN: - {0} + {1}", deletedOpleidingen.Count, addedOpleidingen.Count);

            deletedOpleidingen.ForEach(o => dbGebruiker.VoorkeurOpleidingen.Remove(o));

            foreach (Opleiding opleiding in addedOpleidingen)
            {
                if (db.Entry(opleiding).State == EntityState.Detached)
                {
                    db.Opleidings.Attach(opleiding);
                }
                dbGebruiker.VoorkeurOpleidingen.Add(opleiding);
            }

            db.Entry(dbGebruiker).State = EntityState.Modified;

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