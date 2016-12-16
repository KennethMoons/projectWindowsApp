using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;

namespace WebApiOpendeurdag2.Services
{
    public interface ReportService<T, D> where T : class where D : class
    {
        AddLineStrategy<T, D> strat { get; set; }

        Stream MakeDocument(IEnumerable<T> objects);
    }

    public enum ReportType {
        PDF, EXCEL
    }

    public interface AddLineStrategy<T, D> where T : class where D : class
    {
        void AddLine(T obj, D doc);

        int Length();
    }
}
