using iText.Layout;
using iText.Layout.Element;
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
            var service = ReportFactory.Create<Gebruiker, Table>(ReportType.PDF);
            service.strat = new GebruikerPdfStrat();
            Stream s = service.MakeDocument(db.Gebruikers);

            return File(s, "application/pdf", "result.pdf");
        }

        class GebruikerPdfStrat : AddLineStrategy<Gebruiker, Table>
        {
            public void AddLine(Gebruiker obj, Table tab)
            {
                tab.AddCell(obj.GebruikerId.ToString());
                tab.AddCell(obj.Naam);
                tab.AddCell(obj.Email);
                tab.AddCell(obj.Telnr);
                tab.AddCell(obj.Postcode);
                tab.AddCell(obj.Gemeente);
                // TODO: Gebruiker moet List<VoorkeurCampus> en List<VoorkeurOpleiding> hebben
            }

            public int Length()
            {
                return 6;
            }
        }
    }
}
