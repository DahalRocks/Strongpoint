using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Strongpoint.Controllers
{
    public class ExportToExcelController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public FileResult Export(string GridHtml)
        {
            
            // Get an encoding for code page 1252 (Western Europe character set).
            Encoding cp1252 = Encoding.GetEncoding(1252);

            FileResult resultFile= File(cp1252.GetBytes(GridHtml), "application/vnd.ms-excel", "Table.xls");
            resultFile.FileDownloadName = "XmlReport.xls";
            return resultFile;
        }
    }
}