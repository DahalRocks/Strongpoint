using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public GenerateReportApiController(IReportRepository reportRepository)
        {
            _reportRepository = reportRepository;
        }
        [HttpPost]
        [Route("bysearch")]
        public async Task<object> GetSearch([FromBody]Faktura faktura)
        {
            //We can use it for paging in the future
            faktura.CurrentPage = 1;
            faktura.PageSize = 50;

            (object objFakturaList, int totalRecord) = await _reportRepository.GetReport(faktura);
            return objFakturaList;
        }
        
    }
}
