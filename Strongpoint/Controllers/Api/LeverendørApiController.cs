using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Strongpoint.Models;
using Strongpoint.Models.Repository;

namespace Strongpoint.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeverendørApiController : ControllerBase
    {
        private readonly ILeverendørRepository _leverendørRepository;
        public readonly IHostingEnvironment _hostingEnvironment;
        public LeverendørApiController(ILeverendørRepository leverendørRepository,IHostingEnvironment hostingEnvironment)
        {
            _leverendørRepository = leverendørRepository;
            _hostingEnvironment = hostingEnvironment;
            
        }
        // GET: api/Leverendør
        [HttpGet]
        public async Task<object> GetLeverendør()
        {
            try
            {
                return await _leverendørRepository.GetLeverendør();
            }
            catch (Exception e)
            {
                LogError.LogErrorInstance.Log(e, _hostingEnvironment);
                throw;
            }
            
        }

        
    }
}