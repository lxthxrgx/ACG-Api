using ACG_Class.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ACG_Class.Model.ModelMemory.Class;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ACG_Api.Controllers.Accounting
{

    [Route("api/Accounting/Analytics")]
    [ApiController]
    public class AnalyticsController : ControllerBase
    {   
        private readonly MemoryDb _mcontext;
        public AnalyticsController(DataBaseContext context, MemoryDb mcontext)
        {
            _mcontext = mcontext;
        }

        // GET: api/<AnalyticsController>
        [Route("Sublease")]
        [HttpGet]
        public async Task<IEnumerable<_4D_Memory>> GetSublease()
        {
            return await _mcontext.D4_Memory.ToListAsync();
        }

        [Route("Guard")]
        [HttpGet]
        public async Task<IEnumerable<_5D_Memory>> GetGuard()
        {
            return await _mcontext.D5_Memory.ToListAsync();
        }

        [Route("Counterparty")]
        [HttpGet]
        public async Task<IEnumerable<_1D_Memory>> GetCounterparty()
        {
            return await _mcontext.D1_Memory.ToListAsync();
        }
    }
}
