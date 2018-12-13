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
        // GET: api/GenerateReportApi
        [HttpGet]
        public async Task<object> Get()
        {
            return await _reportRepository.GetReport();
        }

        // GET: api/GenerateReportApi/5
        [HttpPost]
        [Route("{byfakturanummer}")]
        public async Task<object> Get([FromBody]Faktura faktura)
        {
            return await _reportRepository.GetReportBySearch(faktura);
        }

        // POST: api/GenerateReportApi
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/GenerateReportApi/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
