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
    public class NewsitemsController : ApiController
    {
        private WebApiOpendeurdag2Context db = new WebApiOpendeurdag2Context();

        // GET: api/Newsitems
        [AllowAnonymous]
        public IQueryable<Newsitem> GetNewsitems()
        {
            return db.Newsitems.Include(i => i.Campus).Include(i => i.Opleiding);
        }

        // GET: api/Newsitems/5
        [AllowAnonymous]
        [ResponseType(typeof(Newsitem))]
        public async Task<IHttpActionResult> GetNewsitem(int id)
        {
            Newsitem newsitem = await db.Newsitems.FindAsync(id);
            if (newsitem == null)
            {
                return NotFound();
            }

            return Ok(newsitem);
        }

        // PUT: api/Newsitems/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutNewsitem(int id, Newsitem newsitem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != newsitem.NewsitemId)
            {
                return BadRequest();
            }

            db.Entry(newsitem).State = EntityState.Modified;
            
            if (newsitem.Campus != null)
            {
                db.Entry(newsitem.Campus).State = EntityState.Unchanged;
            }

            if (newsitem.Opleiding != null)
            {
                db.Entry(newsitem.Opleiding).State = EntityState.Unchanged;
            }

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NewsitemExists(id))
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

        // POST: api/Newsitems
        [ResponseType(typeof(Newsitem))]
        public async Task<IHttpActionResult> PostNewsitem(Newsitem newsitem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Newsitems.Add(newsitem);

            if (newsitem.Campus != null)
            {
                db.Entry(newsitem.Campus).State = EntityState.Unchanged;
            }

            if (newsitem.Opleiding != null)
            {
                db.Entry(newsitem.Opleiding).State = EntityState.Unchanged;
            }

            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = newsitem.NewsitemId }, newsitem);
        }

        // DELETE: api/Newsitems/5
        [ResponseType(typeof(Newsitem))]
        public async Task<IHttpActionResult> DeleteNewsitem(int id)
        {
            Newsitem newsitem = await db.Newsitems.FindAsync(id);
            if (newsitem == null)
            {
                return NotFound();
            }

            db.Newsitems.Remove(newsitem);
            await db.SaveChangesAsync();

            return Ok(newsitem);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool NewsitemExists(int id)
        {
            return db.Newsitems.Count(e => e.NewsitemId == id) > 0;
        }
    }
}