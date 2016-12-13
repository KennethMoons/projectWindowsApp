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
    [Authorize(Roles = GebruikerRoles.Admin)]
    public class CampusController : ApiController
    {
        private WebApiOpendeurdag2Context db = new WebApiOpendeurdag2Context();

        // GET: api/Campus
        [AllowAnonymous]
        public IQueryable<Campus> GetCampus()
        {
            return db.Campus;
        }

        // GET: api/Campus/5
        [AllowAnonymous]
        [ResponseType(typeof(Campus))]
        public async Task<IHttpActionResult> GetCampus(int id)
        {
            Campus campus = await db.Campus.FindAsync(id);
            if (campus == null)
            {
                return NotFound();
            }

            return Ok(campus);
        }

        // PUT: api/Campus/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutCampus(int id, Campus campus)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != campus.CampusId)
            {
                return BadRequest();
            }

            db.Entry(campus).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CampusExists(id))
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

        // POST: api/Campus
        [ResponseType(typeof(Campus))]
        public async Task<IHttpActionResult> PostCampus(Campus campus)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Campus.Add(campus);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = campus.CampusId }, campus);
        }

        // DELETE: api/Campus/5
        [ResponseType(typeof(Campus))]
        public async Task<IHttpActionResult> DeleteCampus(int id)
        {
            Campus campus = await db.Campus.FindAsync(id);
            if (campus == null)
            {
                return NotFound();
            }

            db.Campus.Remove(campus);
            await db.SaveChangesAsync();

            return Ok(campus);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CampusExists(int id)
        {
            return db.Campus.Count(e => e.CampusId == id) > 0;
        }
    }
}