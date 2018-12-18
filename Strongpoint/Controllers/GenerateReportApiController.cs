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
        [Route("totalrecordwithoutsearch")]
        public object GetRecord([FromBody]Faktura faktura)
        {
            (object objFakturaList, int totalRecord) = _reportRepository.GetReport(faktura);
            Faktura objFaktura = new Faktura() { TotalRows = totalRecord };
            return objFaktura;
        }
        [HttpPost]
        [Route("totalrecordbysearch")]
        public object GetRecordBySearch([FromBody]Faktura faktura)
        {
            (object objFakturaList, int totalRecord) = _reportRepository.GetReportBySearch(faktura);
            Faktura objFaktura = new Faktura() { TotalRows = totalRecord };
            return objFaktura;
        }
        [HttpPost]
        [Route("withoutsearch")]
        public object GetSearchWithOutSearch([FromBody] Faktura faktura)
        {
            (object objFakturaList, int totalRecord) =  _reportRepository.GetReport(faktura);
            return objFakturaList;
        }
        [HttpPost]
        [Route("bysearch")]
        public object GetSearchBySearch([FromBody]Faktura faktura)
        {
            (object objFakturaList, int totalRecord) = _reportRepository.GetReportBySearch(faktura);
            return objFakturaList;
        }
        
    }
}
