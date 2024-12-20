using Microsoft.AspNetCore.Mvc;
using ACG_Class.Database;
using Microsoft.EntityFrameworkCore;
using ACG_Class.Model.Class;
using ACG_Class.Model.ModelMemory.Class;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ACG_Api.Controllers.Accounting
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubleaseController : ControllerBase
    {
        private readonly DataBaseContext _context;
        private readonly MemoryDb _mcontext;
        public SubleaseController(DataBaseContext context, MemoryDb mcontext)
        {
            _context = context;
            _mcontext = mcontext;
        }

        // GET: api/<SubleaseController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<_4D>>> Get()
        {
            var result = await _context.D4.ToListAsync();
            return Ok(result);
        }

        // POST api/<SubleaseController>
        [HttpPost("Add")]
        public async Task<IActionResult> Post([FromBody] _4D value)
        {

            if (value == null)
            {
                return StatusCode(400, new { server_error = "No Data" });
            }

            try
            {
                await _context.D4.AddAsync(value);
                await _context.SaveChangesAsync();

                _context.Database.GetDbConnection().Close();

                await _mcontext.D4_Memory.AddAsync(
                    new _4D_Memory {
                        Id = value.Id,
                        NumberGroup = value.NumberGroup,
                        NameGroup = value.NameGroup,
                        address = value.address,
                        DogovirSuborendu = value.DogovirSuborendu,
                        DateTime = value.DateTime,
                        EndAktDate = value.EndAktDate,
                        Suma = value.Suma,
                        Suma2 = value.Suma2,
                        AktDate = value.AktDate,
                        added = DateTime.Now,
                    }
                    );
                await _mcontext.SaveChangesAsync();

                var hasNullRecords = _mcontext.D4_Memory
                    .Where(x => x.Id == value.Id)
                    .Any(x => x.NumberGroup == null ||
                              x.NameGroup == null || x.address == null ||
                              x.DogovirSuborendu == null || x.DateTime == DateTime.MinValue ||
                              x.Suma == null || x.AktDate == null);

                if (hasNullRecords)
                {
                    return StatusCode(400, new { server_error = "There are records with null values in some fields.", data = value });
                }

                return Ok(value);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { server_error = $"Error: {ex.Message}" });
            }
        }

        // PUT api/<SubleaseController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<SubleaseController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
