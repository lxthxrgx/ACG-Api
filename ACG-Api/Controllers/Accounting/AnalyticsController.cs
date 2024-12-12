using ACG_Class.Database;
using ACG_Class.Model.Class;
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
        private readonly MemoryDb _mcontext;
        public AnalyticsController(DataBaseContext context, MemoryDb mcontext)
        {
            _context = context;
            _mcontext = mcontext;
        }

        // GET: api/<AnalyticsController>
        [Route("Sublease")]
        [HttpGet]
        public async Task<IEnumerable<_4D>> GetSublease()
        {
            return await _context.D4.ToListAsync();
        }

        [Route("AddToMemory")]
        [HttpPost]
        public async Task<IEnumerable<_4D>> GetSubleaseMemory()
        {
            var data = await _context.D4.ToListAsync();

            // Добавляем полученные данные в in-memory базу данных
            await _mcontext.D4_Memory.AddRangeAsync(data);

            // Сохраняем изменения в in-memory базе данных
            await _mcontext.SaveChangesAsync();

            // Возвращаем данные из in-memory базы данных
            return await _mcontext.D4_Memory.ToListAsync();
        }

        [Route("Guard")]
        [HttpGet]
        public async Task<IEnumerable<_5D>> GetGuard()
        {
            return await _context.D5.ToListAsync();
        }

        [Route("Counterparty")]
        [HttpGet]
        public async Task<IEnumerable<_1D>> GetCounterparty()
        {
            return await _context.D1.ToListAsync();
        }
    }
}
