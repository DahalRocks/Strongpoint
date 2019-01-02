using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DomainModel;
using Interfaces;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<object> GetSearch([FromBody]Invoice invoice)
        {
            invoice.CurrentPage = 1;
            invoice.PageSize = 50;
            (IEnumerable<IInvoice> objFakturaList, int totalRecord) = await _reportRepository.GetReport(invoice);
            return objFakturaList;
        }
    }
}
