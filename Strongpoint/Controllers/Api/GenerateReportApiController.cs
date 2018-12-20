using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Strongpoint.Models;

namespace Strongpoint.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenerateReportApiController : ControllerBase
    {
        private readonly IReportRepository _reportRepository;
        private readonly IHostingEnvironment _hostingEnvironment;
        public GenerateReportApiController(IReportRepository reportRepository, IHostingEnvironment hostingEnvironment)
        {
            _reportRepository = reportRepository;
            _hostingEnvironment = hostingEnvironment;
        }
        [HttpPost]
        [Route("bysearch")]
        public async Task<object> GetSearch([FromBody]Faktura faktura)
        {
            //We can use it for paging in the future
            faktura.CurrentPage = 1;
            faktura.PageSize = 50;
            try
            {
               (object objFakturaList, int totalRecord) = await _reportRepository.GetReport(faktura);
               return objFakturaList;
            }
            catch (Exception e)
            {
                LogError.LogErrorInstance.Log(e, _hostingEnvironment);
                throw;
            }
            
        }
        
    }
}
