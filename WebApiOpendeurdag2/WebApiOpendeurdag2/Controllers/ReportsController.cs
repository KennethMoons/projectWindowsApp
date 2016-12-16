using iText.Layout;
using iText.Layout.Element;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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

        private HttpResponseMessage MakeRequest(MemoryStream ms, String bestandsnaam) {
            // https://stackoverflow.com/questions/26038856/how-to-return-a-file-filecontentresult-in-asp-net-webapi
            var result = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ByteArrayContent(ms.ToArray())
            };
            result.Content.Headers.ContentDisposition =
                new ContentDispositionHeaderValue("attachment")
                {
                    FileName = bestandsnaam
                };
            result.Content.Headers.ContentType =
                new MediaTypeHeaderValue("application/octet-stream");
            return result;
        }

        [System.Web.Http.AllowAnonymous]
        public HttpResponseMessage GetReports() {
            return GetReport("");
        }

        [System.Web.Http.AllowAnonymous]
        public HttpResponseMessage GetReport(string id)
        {
            try
            {
                var s = (ReportType) Enum.Parse(typeof(ReportType), id.ToUpper());
                switch (s) {
                    case ReportType.PDF: return GetPdfReport();
                    case ReportType.EXCEL: return GetExcelReport();
                }
            }
            catch (ArgumentException) {
            }
            return GetPdfReport();
        }
        
        private HttpResponseMessage GetPdfReport()
        {
            var service = ReportFactory.Create<Gebruiker, Table>(ReportType.PDF);
            service.strat = new GebruikerPdfStrat();
            MemoryStream s = service.MakeDocument(db.Gebruikers) as MemoryStream;

            return MakeRequest(s, "GebruikerReport.pdf");
        }

        private HttpResponseMessage GetExcelReport()
        {
            var service = ReportFactory.Create<Gebruiker, ExcelWorksheet>(ReportType.EXCEL);
            service.strat = new GebruikerExcelStrat();
            MemoryStream s = service.MakeDocument(db.Gebruikers) as MemoryStream;

            return MakeRequest(s, "GebruikerReport.xlsx");
        }

        class GebruikerPdfStrat : AddLineStrategy<Gebruiker, Table>
        {
            public void AddLine(Gebruiker obj, Table tab)
            {
                tab.AddCell(obj.GebruikerId.ToString() + "");
                tab.AddCell(obj.Naam + "");
                tab.AddCell(obj.Email + "");
                tab.AddCell(obj.Telnr + "");
                tab.AddCell(obj.Postcode + "");
                tab.AddCell(obj.Gemeente + "");
                // TODO: Gebruiker moet List<VoorkeurCampus> en List<VoorkeurOpleiding> hebben
            }

            public int Length()
            {
                return 6;
            }
        }

        class GebruikerExcelStrat : AddLineStrategy<Gebruiker, ExcelWorksheet>
        {
            private int row = 1;

            public void AddLine(Gebruiker obj, ExcelWorksheet worksheet)
            {
                worksheet.Cells[row, 1].Value = obj.GebruikerId.ToString();
                worksheet.Cells[row, 2].Value = obj.Naam;
                worksheet.Cells[row, 3].Value = obj.Email;
                worksheet.Cells[row, 4].Value = obj.Telnr;
                worksheet.Cells[row, 5].Value = obj.Postcode;
                worksheet.Cells[row, 6].Value = obj.Gemeente;
                row++;
            }

            public int Length()
            {
                return 6;
            }
        }
    }
}
