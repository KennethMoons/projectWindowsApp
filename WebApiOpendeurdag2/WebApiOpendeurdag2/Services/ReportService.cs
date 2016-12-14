using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;

namespace WebApiOpendeurdag2.Services
{
    public interface ReportService<T> where T : class
    {
        Stream MakeDocument(DbSet<T> objects);
    }

    public enum ReportType {
        PDF, EXCEL
    }
}
