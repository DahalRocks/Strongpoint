using System.Text;
using Microsoft.AspNetCore.Mvc;

namespace Strongpoint.Controllers
{
    public class ExportToExcelController : Controller
    {
        [HttpPost]
        public FileResult Export(string hiddenHtml)
        {
            // Get an encoding for code page 1252 (Western Europe character set).
            Encoding cp1252 = Encoding.GetEncoding(1252);
            FileResult resultFile = File(cp1252.GetBytes(hiddenHtml), "application/vnd.ms-excel", "Table.xls");
            resultFile.FileDownloadName = "XmlReport.xls";
            return resultFile;
        }
    }
}