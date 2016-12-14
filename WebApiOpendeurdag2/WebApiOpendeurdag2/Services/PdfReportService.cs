using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;

namespace WebApiOpendeurdag2.Services
{
    internal class PdfReportService<T> : ReportService<T, Table> where T : class
    {
        public AddLineStrategy<T, Table> strat { get; set; }

        public PdfReportService(AddLineStrategy<T, Table> str) {
            strat = str;
        }

        public PdfReportService() : this(new DefaultPdfStrat())
        {
        }

        public Stream MakeDocument(DbSet<T> objects)
        {
            Stream result = new MemoryStream();
            PdfDocument pdf = new PdfDocument(new PdfWriter(result));
            using (Document doc = new Document(pdf)) {
                doc.Add(new Paragraph("Dit is een rapport van alle studenten"));
                Table table = new Table(strat.Length());
                // TODO header line & layout
                foreach (T obj in objects)
                {
                    strat.AddLine(obj, table);
                }
                doc.Add(table);
            }
            
            return result;
        }

        private class DefaultPdfStrat : AddLineStrategy<T, Table>
        {
            public void AddLine(T obj, Table tab)
            {
                tab.AddCell("object added");
            }

            public int Length()
            {
                return 1;
            }
        }
    }
}
