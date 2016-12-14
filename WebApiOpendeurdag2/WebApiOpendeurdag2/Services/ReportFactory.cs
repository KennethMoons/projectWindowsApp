using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiOpendeurdag2.Services
{
    public static class ReportFactory
    {
        internal static ReportService<T> Create<T>(ReportType type) where T : class
        {
            switch (type) {
                case ReportType.PDF: return new PdfReportService<T>();
                case ReportType.EXCEL: break;
            }
            throw new NotImplementedException(type.ToString() + " heeft nog geen ReportService");
        }
    }
}
