﻿using ACG_Class.Database;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ACG_Api.Groups
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupController : ControllerBase
    {
        private readonly DataBaseContext _context;

        public GroupController(DataBaseContext context)
        {
            _context = context;
        }

        // GET: api/<GroupController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<GroupController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<GroupController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<GroupController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<GroupController>/5
        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] int id)
        {
            var DataToDelete = _context.D2.Where(e => e.Id == id).SingleOrDefault();
            if (DataToDelete != null)
            {
                var a = _context.D2.Remove(DataToDelete);
                await _context.SaveChangesAsync();
                return StatusCode(200, new { succses = $"Успішно видалено запис" });
            }
            else
            {
                return StatusCode(400, "Data is not found");
            }
        }
    }
}
