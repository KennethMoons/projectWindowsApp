using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;

namespace WebApiOpendeurdag2.Services
{
    internal abstract class PdfReportService<T> : ReportService<T> where T : class
    {
        public Stream MakeDocument(DbSet<T> objects)
        {
            Stream result = new MemoryStream();
            PdfDocument pdf = new PdfDocument(new PdfWriter(result));
            using (Document doc = new Document(pdf)) {
                doc.Add(new Paragraph("test"));
            }
            
            return result;
        }
    }
}
