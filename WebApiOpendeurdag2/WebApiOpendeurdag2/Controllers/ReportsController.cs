using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Mvc;
using WebApiOpendeurdag2.Models;
using WebApiOpendeurdag2.Services;

namespace WebApiOpendeurdag2.Controllers
{
    public class ReportsController : Controller
    {
        private WebApiOpendeurdag2Context db = new WebApiOpendeurdag2Context();

        [System.Web.Http.AllowAnonymous]
        public async Task<ActionResult> GetReports()
        {
            Stream s = ReportFactory.Create<Gebruiker>(ReportType.PDF).MakeDocument(db.Gebruikers);

            return File(s, "application/pdf", "result.pdf");
        }
    }
}
