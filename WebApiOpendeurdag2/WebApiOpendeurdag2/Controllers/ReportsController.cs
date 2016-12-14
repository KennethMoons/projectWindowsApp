using iText.Layout;
using iText.Layout.Element;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Mvc;
using WebApiOpendeurdag2.Models;
using WebApiOpendeurdag2.Services;

namespace WebApiOpendeurdag2.Controllers
{
    public class ReportsController : ApiController
    {
        private WebApiOpendeurdag2Context db = new WebApiOpendeurdag2Context();

        [System.Web.Http.AllowAnonymous]
        public async Task<HttpResponseMessage> GetReports()
        {
            var service = ReportFactory.Create<Gebruiker, Table>(ReportType.PDF);
            service.strat = new GebruikerPdfStrat();
            MemoryStream s = service.MakeDocument(db.Gebruikers) as MemoryStream;
            // https://stackoverflow.com/questions/26038856/how-to-return-a-file-filecontentresult-in-asp-net-webapi
            var result = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ByteArrayContent(s.ToArray())
            };
            result.Content.Headers.ContentDisposition =
                new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment")
                {
                    FileName = "GebruikerReport.pdf"
                };
            result.Content.Headers.ContentType =
                new MediaTypeHeaderValue("application/octet-stream");

            return result;
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
