using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;

namespace WebApiOpendeurdag2.Services
{
    public class ExcelReportService<T> : ReportService<T, ExcelWorksheet> where T : class
    {
        public AddLineStrategy<T, ExcelWorksheet> strat { get; set; }

        public ExcelReportService(AddLineStrategy<T, ExcelWorksheet> str)
        {
            strat = str;
        }

        public ExcelReportService() : this(new DefaultExcelStrat())
        {
        }

        public Stream MakeDocument(IEnumerable<T> objects)
        {
            Stream result = new MemoryStream();
            using (ExcelPackage package = new ExcelPackage(result))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Lijst");
                strat.AddHeaderLine(worksheet);
                foreach (T obj in objects)
                {
                    strat.AddLine(obj, worksheet);
                }
                package.Save();
            }
            return result;
        }

        private class DefaultExcelStrat : AddLineStrategy<T, ExcelWorksheet>
        {
            private int row = 1;

            public void AddHeaderLine(ExcelWorksheet doc)
            {
                row++;
            }

            public void AddLine(T obj, ExcelWorksheet worksheet)
            {
                worksheet.Cells[row, 1].Value = "object toegevoegd";
                row++;
            }

            public int Length()
            {
                return 1;
            }
        }
    }
}