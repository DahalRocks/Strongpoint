using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Strongpoint.Models;

namespace Strongpoint.Controllers
{
    public class ExportToExcelController : Controller
    {
        public readonly IHostingEnvironment _hostingEnvironment;
        public ExportToExcelController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public FileResult Export(string hiddenHtml)
        {

            try
            {
                // Get an encoding for code page 1252 (Western Europe character set).
                Encoding cp1252 = Encoding.GetEncoding(1252);
                FileResult resultFile = File(cp1252.GetBytes(hiddenHtml), "application/vnd.ms-excel", "Table.xls");
                resultFile.FileDownloadName = "XmlReport.xls";
                return resultFile;
            }
            catch (Exception e)
            {
                LogError.LogErrorInstance.Log(e, _hostingEnvironment);
                throw;
            }
            
        }
    }
}