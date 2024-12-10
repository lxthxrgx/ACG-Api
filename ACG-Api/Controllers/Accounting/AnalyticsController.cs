using ACG_Api.Database;
using ACG_Api.Model.Class;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ACG_Api.Controllers.Accounting
{

    [Route("api/Accounting/Analytics")]
    [ApiController]
    public class AnalyticsController : ControllerBase
    {   
        private readonly DataBaseContext _context;
        public AnalyticsController(DataBaseContext context)
        {
            _context = context;
        }

        // GET: api/<AnalyticsController>
        [Route("Counterparty")]
        [HttpGet]
        public async Task<IEnumerable<_4D>> GetCounterparty()
        {
            return await _context.D4.ToListAsync();
        }

        [Route("Guard")]
        [HttpGet]
        public async Task<IEnumerable<_1D>> GetGuard()
        {
            return await _context.D1.ToListAsync();
        }
    }
}
