using System.Threading.Tasks;
using Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Strongpoint.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierApiController : ControllerBase
    {
        private readonly ISupplierRepository _supplierRepository;
        public SupplierApiController(ISupplierRepository supplierRepository)
        {
            _supplierRepository = supplierRepository;
        }
        // GET: api/SupplierApi
        [HttpGet]
        public async Task<object> GetSupplier()
        {
            return await _supplierRepository.GetSupplier();
        }
    }
}