using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiOpendeurdag2.Services
{
    public static class ReportFactory
    {
        internal static ReportService<T, D> Create<T, D>(ReportType type) where T : class where D : class
        {
            switch (type) {
                case ReportType.PDF: return new PdfReportService<T>() as ReportService<T, D>;
                case ReportType.EXCEL: return new ExcelReportService<T>() as ReportService<T, D>;
            }
            throw new NotImplementedException(type.ToString() + " heeft nog geen ReportService");
        }
    }
}
